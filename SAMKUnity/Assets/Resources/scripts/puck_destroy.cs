using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puck_destroy : MonoBehaviour {

    private float puckFade;             //Alpha-value that gets 
    public CanvasManip CM;
    public target_random TR;
    private bool activeScore = true;
    public GameObject BowEffect;

    //bool fadeVolume = false;


    // Use this for initialization
    void Start () {
        CM = GetComponent<CanvasManip>();
        TR = GetComponent<target_random>();
        puckFade = 100;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }
	
	// Update is called once per frame
	void Update () {
        if (puckFade <= 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, puckFade);
            puckFade -= 0.01f;
            if (puckFade <= 0f)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            puckFade--;
        }
 

    }

    IEnumerator DestroyPuck()
    {
        yield return new WaitForSeconds(5f);
    }


    IEnumerator playGoalSaveSounds()
    {
        // Thud noise
        SoundManager.instance.PlaySingle(GameControl.instance.thudSoundAudio, 0.8f);
        yield return new WaitForSeconds(GameControl.instance.thudSoundAudio.length);
         
        // Crowd cheer       
        SoundManager.instance.FadeInCaller(GameControl.instance.crowdCheerAudio, 0.1f, 0.6f);
        SoundManager.instance.FadeOutCaller(GameControl.instance.crowdCheerAudio, 0.005f, 3.0f);

    }

    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (GameObject.FindGameObjectWithTag("targetti") != null)
        {
            if (coll.collider.tag == "limbsR" && activeScore && puckFade > 99.0f)
            {
                TR = GameObject.FindGameObjectWithTag("targetti").GetComponent<target_random>();
                CM = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasManip>();
                CM.scoreAmount += 1;

                //TR.isUpRep();


                // Only score as completed rep on upRep
                if (GameControl.instance.isUpRep)
                    CM.scoreRight += 1;
                else
                    Debug.Log("NO REP!");

                //Debug.Log("UpRep: " + GameControl.instance.isUpRepLeft);
                GameControl.instance.completedRepsLeft = CM.scoreRight;            // Note: Arms are mirrored

                StartCoroutine(playGoalSaveSounds());

                GameObject fx = Instantiate(BowEffect) as GameObject;
                fx.transform.position = transform.position;
                activeScore = false;
                

                TR.pointColor = new Color32(0x01, 0x75, 0xC8, 0xFF);
                GameObject point = Instantiate(TR.puckpoint) as GameObject;    //Adds an indicator object to score meter
                point.transform.position = new Vector2(TR.meter.transform.position.x + 0f, TR.meter.transform.position.y + 6f);   //Gives the position to indicator object
                point.GetComponent<SpriteRenderer>().color = TR.pointColor;//0175C8
            }
          

            if (coll.collider.tag == "limbsL" && activeScore && puckFade > 99.0f)
            {
                TR = GameObject.FindGameObjectWithTag("targetti").GetComponent<target_random>();
                CM = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasManip>();
                CM.scoreAmount += 1;

                //TR.isUpRep();
                
                // Only score as completed rep on upRep
                if (GameControl.instance.isUpRep)
                    CM.scoreLeft += 1;
                else
                    Debug.Log("NO REP!");

                GameControl.instance.completedRepsRight = CM.scoreLeft;

                StartCoroutine(playGoalSaveSounds());

                GameObject fx = Instantiate(BowEffect) as GameObject;
                fx.transform.position = transform.position;
                activeScore = false;
                               

                TR.pointColor = new Color32(0xFB, 0xBB, 0x01, 0xFF);
                GameObject point = Instantiate(TR.puckpoint) as GameObject;    //Adds an indicator object to score meter
                point.transform.position = new Vector2(TR.meter.transform.position.x + 0f, TR.meter.transform.position.y + 6f);   //Gives the position to indicator object
                point.GetComponent<SpriteRenderer>().color = TR.pointColor;//FBBB01
            }
        }
        

        //Debug.Log("ScoreL: " + GameControl.instance.completedRepsLeft);
        //Debug.Log("ScoreR: " + GameControl.instance.completedRepsRight);

    }

}

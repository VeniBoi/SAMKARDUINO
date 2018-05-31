using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puck_destroy : MonoBehaviour {

    private float puckFade;             //Alpha-value that gets 
    public CanvasManip CM;
    private bool activeScore = true;

    // Use this for initialization
    void Start () {
        CM = GetComponent<CanvasManip>();
        puckFade = 100;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(puckFade);
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.tag == "limbs" && activeScore && puckFade > 99)
        {
            //GetComponent<CanvasManip>().scoreAmount += 1;
                Debug.Log("ajshdsajkmhdjksahdkjsa");
                CM = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasManip>();
                CM.scoreAmount += 1;
            activeScore = false;
        }
    }
}

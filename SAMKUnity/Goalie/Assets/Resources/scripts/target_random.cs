using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_random : MonoBehaviour
{

    private int timer = 0, score;
    private float timeUntilHit;
    public GameObject puckker, arcRotator;
    public CanvasManip CM;
    // Use this for initialization
    void Start()
    {
        replace();
        GetComponent<CircleCollider2D>().enabled = true;
    }

    // Update is called once per frame

    void Update()
    {
        if (timer == 1)     
        {
            //GameObject shot = Instantiate(puckker) as GameObject;
            puckker.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), -7, 1);
            puckker.GetComponent<puck_fly>().journeyLength = Vector3.Distance(transform.position, puckker.transform.position);
            puckker.SetActive(true);
            timer = 0;
        }

        else
        {
            --timer;                                    //Decrease timer every frame
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "ice_puck")
        {
            replace();
            //shoot();
        }

        if (collider.tag == "limbs")                    //When limb hits the target
        {
            timer = 10;                                //Leaving a little buffer before the hit
            //score++;                                    //Scoring shouldn't probably be done here
            //CM.ScoreValue.text = score.ToString();      //Convert score to UI.text
        }

        else
        {
            replace();
        }
    }

    private void replace()
    {
        timer = (System.Convert.ToInt32(CM.MaxTimeToTargetSlider.value)*60);                        //Convert seconds to frames [not the best solution]
        //transform.position = new Vector2(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
        arcRotator.transform.rotation = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.forward);
    }
}

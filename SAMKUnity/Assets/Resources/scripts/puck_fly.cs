using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puck_fly : MonoBehaviour
{
    public CanvasManip CM;
    public target_random TR;
    public GameObject target;
    //public GameObject target2;
    public GameObject used_puck;
    public float journeyLength, journeyLength2;
    public Vector3 pos, pos2;
    public int score = 0;
    public Text ScoreText;
    public GameObject findingPoint;

 

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 1);
        pos = target.GetComponent<Transform>().position;
        journeyLength = Vector3.Distance(pos, transform.position);
        GetComponent<BoxCollider2D>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
 
        pos = target.GetComponent<Transform>().position;
        float distance = Vector3.Distance(pos, transform.position);
        if (distance >= 0.3f)
        {
            
            transform.localScale = new Vector3(0.05f + 0.75f * (distance / journeyLength), 0.05f + 0.75f * (distance / journeyLength), 1);
            transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
        }

        else
        {
            GameObject shot = Instantiate(used_puck) as GameObject;
            shot.transform.position = transform.position;
            gameObject.SetActive(false);
            //Debug.Log(score);
        }

    }

    /*void OnColliderStay(Collider2D collider)
    {
        if (collider.tag == "limbsR")
        {
            score++;
            TR.pointColor = Color.blue;
            GameObject point = Instantiate(TR.puckpoint) as GameObject;    //Adds an indicator object to score meter
            point.transform.position = new Vector2(TR.meter.transform.position.x + 0f, TR.meter.transform.position.y + 6f);   //Gives the position to indicator object
        }
        if (collider.tag == "limbsL")
        {
            score++;
            TR.pointColor = Color.green;
            GameObject point = Instantiate(TR.puckpoint) as GameObject;    //Adds an indicator object to score meter
            point.transform.position = new Vector2(TR.meter.transform.position.x + 0f, TR.meter.transform.position.y + 6f);   //Gives the position to indicator object
        }
        Debug.Log(score);
        ScoreText.text = score.ToString();
        GameObject shot = Instantiate(used_puck) as GameObject;
        shot.transform.position = transform.position;
        gameObject.SetActive(false);
    }*/
}

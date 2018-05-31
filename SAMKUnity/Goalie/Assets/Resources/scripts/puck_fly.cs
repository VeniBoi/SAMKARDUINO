using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puck_fly : MonoBehaviour
{
    public CanvasManip CM;
    public GameObject target;
    public GameObject used_puck;
    public float journeyLength;
    public Vector3 pos;
    public int score = 0;
    public Text ScoreText;


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
            //GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            GameObject shot = Instantiate(used_puck) as GameObject;
            shot.transform.position = transform.position;
            gameObject.SetActive(false);
            Debug.Log(score);
        }
    }

    void OnColliderStay(Collider2D collider)
    {
        if (collider.tag == "limbs")
        {
            //++score;
            score++;
            /*Debug.Log(score);
            ScoreText.text = score.ToString();
            GameObject shot = Instantiate(used_puck) as GameObject;
            shot.transform.position = transform.position;
            gameObject.SetActive(false);*/
            //replace();
        }
        Debug.Log(score);
        ScoreText.text = score.ToString();
        GameObject shot = Instantiate(used_puck) as GameObject;
        shot.transform.position = transform.position;
        gameObject.SetActive(false);
    }

}

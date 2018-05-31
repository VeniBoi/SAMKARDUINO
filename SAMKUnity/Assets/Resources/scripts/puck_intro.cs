using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puck_intro : MonoBehaviour {

    public float puckSpeed = 0.2f;
    public GameObject puck;
    public GameObject explosion;
    //public GameObject star;
    //public GameObject goalieHead;

    public TrailRenderer trail;



    // Use this for initialization
    void Start ()
    {
        //puck.transform.position = new Vector2(-20.0f, 1.82f);

        
        trail.sortingLayerName = "Background";
        trail.sortingOrder = 2;

    }
	
	// Update is called once per frame
	void Update () {

        float xPos = puck.transform.position.x;
        float yPos = puck.transform.position.y;

        puck.transform.position = new Vector2(xPos+puckSpeed, yPos);
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Animate explosion
        GameObject shot = Instantiate(explosion) as GameObject;
        shot.SetActive(true);
        shot.transform.position = new Vector3(2.69f, 1.43f,0.02f);
        //gameObject.SetActive(false);

        //GameObject starShot = Instantiate(star) as GameObject;
        //starShot.SetActive(true);
        //starShot.transform.position = new Vector3(5.0f, 3.78f, 0.02f);




        // Animate goalie head
        //Vector3 currentHeadPos = goalieHead.transform.position;
        //Vector3 newGoaliePos1 = new Vector3(0.0f, 0.0f, currentHeadPos.z * 10.0f);
        //Vector3 newGoaliePos2 = new Vector3(0.0f, 0.0f, currentHeadPos.z * -10.0f);

        //goalieHead.transform.Rotate(newGoaliePos1);
        //goalieHead.transform.Rotate(newGoaliePos2);
        //goalieHead.transform.Rotate(currentHeadPos.x, currentHeadPos.y, currentHeadPos.z - 10.0f);
        //goalieHead.transform.Rotate(currentHeadPos.x, currentHeadPos.y, currentHeadPos.z - 10.0f);
        //goalieHead.transform.Rotate(currentHeadPos.x, currentHeadPos.y, currentHeadPos.z + 10.0f);

        /// Animate goalie head
        IntroSceneControl.introInstance.setShake(0.2f, 0.1f);


        // Reset puck position
        puck.transform.position = new Vector2(-30.9f,1.37f);

    }
}

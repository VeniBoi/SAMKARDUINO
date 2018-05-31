using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_random : MonoBehaviour
{

    private int timer = 0, score;
    public int cycle = 0;
    private float timeUntilHit;
    public GameObject puckker, arcRotator, puckpoint, meter, pause;
    public CanvasManip CM;
    public float RepsInSet;
    public float[] RepsList = { -10, -110, -10, -90, -10, 10, 110, 10, 90, 10 };
    public float tolerance = 15; //K
    public int shotCount = 0;
    public GraphRender GR;
    public Color pointColor = Color.black;
    public bool isLeftHand;

   
    bool hasPuckCollided = false;   // To prevent multiple collision events
    bool isPuck2Active = false;

    void Start()
    {
        //replace();
        GetComponent<CircleCollider2D>().enabled = true;

        // Test if puck2 is active from settings
        if (transform.tag == "targetti2")
        {
            //Debug.Log("Puck2 active");
            isPuck2Active = true;
        }
    }

    // Update is called once per frame

    void Update()
    {
        // Override presets
        float minPuckValue = 20.0f;
        RepsList[0] = -minPuckValue;
        RepsList[2] = -minPuckValue;
        RepsList[4] = -minPuckValue;
        RepsList[5] = minPuckValue;
        RepsList[7] = minPuckValue;
        RepsList[9] = minPuckValue;

        //The maximum value, even if the slider is set to a higher value
        if ((CM.ROMLSlider.value * 5.0f) > 165.0f)
        {
            RepsList[1] = -165;
            RepsList[3] = -165;
            RepsList[6] = 165;
            RepsList[8] = 165;
        }
        else
        {
            RepsList[1] = -CM.ROMLSlider.value * 5.0f;                 //Slider value for left hand, negative needed for left
            RepsList[3] = -CM.ROMLSlider.value * 5.0f;
            RepsList[6] = CM.ROMRSlider.value * 5.0f;                  //Slider value for right hand
            RepsList[8] = CM.ROMRSlider.value * 5.0f;
        }

        // Set target to max values when mapping ROM (i.e. player's maxROM from slider = 180 degrees and "Map to full ROM?" selected in settings)
        if (PlayerPrefs.HasKey("MapToFullROM") && PlayerPrefs.GetInt("MapToFullROM") == 1)
        {
            RepsList[1] = -165.0f;                 //Slider value for left hand, negative needed for left
            RepsList[3] = -165.0f;
            RepsList[6] = 165.0f;                  //Slider value for right hand
            RepsList[8] = 165.0f;

        }


            /*
            if (isLeftHand == false)
            {
                for (int i=0; i<RepsList.Length; i++)
                {
                    RepsList[i] = -1 * RepsList[i];
                }
            }
            */

            pause.transform.position = new Vector2(meter.transform.position.x, -5f + RepsInSet * 0.31f);
        RepsInSet = CM.RepsSlider.value;


        if (timer > 1141)                                   //If selected value is over 19 seconds
        {
            timer = 1200;                                   //Keep it until manually increased
        }

        if (CM.WeighingSlider.value == -1 && cycle > 4)     //If left weighing is selected
        {
            cycle = 1;
        }
        if (CM.WeighingSlider.value == 1 && cycle < 5)      //If right weighing is selected and cycle is under
        {
            cycle = 5;
        }
        if (CM.WeighingSlider.value == 1 && cycle > 9)      //If right weighing is selected and cycle goes over
        {
            cycle = 6;
        }
        if (CM.WeighingSlider.value == 2 && cycle > 9)
        {
            cycle = 1;
        }

        // Shoot puck at target
        if (timer == 1)     
        {
            SoundManager.instance.PlaySingle(GameControl.instance.whooshAudio, 1.0f);

            //Debug.Log("NEW PUCK");
            puckker.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), -7, 1);
            puckker.GetComponent<puck_fly>().journeyLength = Vector3.Distance(transform.position, puckker.transform.position);
            puckker.SetActive(true);
            timer = 0;

            hasPuckCollided = false;

        }

        else
        {
            --timer;                                    //Decrease timer every frame
        }


        // Prevents puck shot timer losing synchronisation (aligns timers)
        if (transform.tag == "targetti")
        {
            GameControl.instance.puckShotTimer = timer;
        }
        if (transform.tag == "targetti2")
        {
            timer = GameControl.instance.puckShotTimer;
        }

    }

    

    void OnTriggerEnter2D(Collider2D collider)
    {
        

        if (collider.tag == "ice_puck")
        {
            //Debug.Log("ice_puck");
            replace();
        }

        if (collider.tag == "limbsL" | collider.tag == "limbsR")                    //When limb hits the target
        {
            //Debug.Log("limb_puck");
            isUpRep();

            //timer = 10;                                //Leaving a little buffer before the hit

            // Timed puck
            if (CM.MaxTimeToTargetSlider.value < 20.0f)
            {
                
            }
            // No time (infinite tutorial level)
            else
            {
                timer = 10;
            }
        }

        else
        {
            if (hasPuckCollided == false)
            {
                CM.shotsOnGoal++;
                //Debug.Log("SHOT: " + CM.shotsOnGoal.ToString());

                //Debug.Log("other_puck");
                //shotCount++;
                isUpRep();

                hasPuckCollided = true;
                replace();

            }
                        
            //GR.ProgressData[0, shotCount] = CM.scoreAmount;
            //GR.GreateData();
            //Debug.Log(shotCount);
        }


       

    }
   
    
    public void isUpRep()
    {
        // isUpRepLeft?
        if (cycle == 1 || cycle == 3 || cycle == 6 || cycle == 8)
        {
            GameControl.instance.isUpRep = false;
        }
        else
        {
            GameControl.instance.isUpRep = true;
        }
        //Debug.Log("Cycle: " + cycle + "   isUpRep: " + GameControl.instance.isUpRep);

        
    }

    
    
    //Place new target
    public void replace()
    {
        timer = (System.Convert.ToInt32(CM.MaxTimeToTargetSlider.value)*60);                        //Convert seconds to frames [not the best solution]

        // test for upRep
        //isUpRep();


        // LEFT HAND ONLY (at the moment shooting both hands)
        if (CM.WeighingSlider.value == -1)
        {
            if (2 % RepsList.Length == 1)       //If RepsList value is odd, adds random element to a target position
            {
                arcRotator.transform.rotation = Quaternion.AngleAxis(RepsList[cycle % RepsList.Length] - Random.Range(-tolerance, tolerance), Vector3.forward);
                
            }

            else
            {
                arcRotator.transform.rotation = Quaternion.AngleAxis(RepsList[cycle % RepsList.Length], Vector3.forward);   //Static value for even value
                
            }

            //Debug.Log("WhatRep: " + GameControl.instance.isUpRepLeft);
            //Debug.Log("ANGLE: " + RepsList[cycle % RepsList.Length].ToString() + "  CYCLE: " + cycle.ToString());

            /*
            GameObject point = Instantiate(puckpoint) as GameObject;    //Adds an indicator object to score meter
            point.transform.position = new Vector2(meter.transform.position.x + 0f, meter.transform.position.y + 6f);   //Gives the position to indicator object

            if (cycle % 5 == 0)             //Every fifth indicator object is different color
            {
                point.GetComponent<SpriteRenderer>().color = Color.black;
            }

            else
            {
                point.GetComponent<SpriteRenderer>().color = Color.yellow;      //Default color for indicator objects
            }
            */
            cycle++;

        }

        // BOTH HANDS (one at a time)
        else if (CM.WeighingSlider.value == 1)
        {
            if (2 % RepsList.Length == 1)       //If RepsList value is odd, adds random element to a target position
            {
                arcRotator.transform.rotation = Quaternion.AngleAxis(RepsList[cycle % RepsList.Length] - Random.Range(-tolerance, tolerance), Vector3.forward);
            }

            else
            {
                arcRotator.transform.rotation = Quaternion.AngleAxis(RepsList[cycle % RepsList.Length], Vector3.forward);   //Static value for even value
            }

            /*GameObject point = Instantiate(puckpoint) as GameObject;    //Adds an indicator object to score meter
            point.transform.position = new Vector2(meter.transform.position.x + 0f, meter.transform.position.y + 6f);   //Gives the position to indicator object

            if (cycle % 5 == 0)             //Every fifth indicator object is different color
            {
                point.GetComponent<SpriteRenderer>().color = Color.black;
            }

            else
            {
                point.GetComponent<SpriteRenderer>().color = Color.yellow;      //Default color for indicator objects
            }*/
            cycle++;
        }

        // BOTH HANDS (same time)
        else if (CM.WeighingSlider.value == 2)
        {
            
            if (2 % RepsList.Length == 1)       //If RepsList value is odd, adds random element to a target position
            {
                //Debug.Log("BOTH HANDS");
                if (isPuck2Active == true && transform.tag == "targetti")
                    arcRotator.transform.rotation = Quaternion.AngleAxis(-1 * (RepsList[cycle % RepsList.Length] - Random.Range(-tolerance, tolerance)), Vector3.forward);
                else
                    arcRotator.transform.rotation = Quaternion.AngleAxis(RepsList[cycle % RepsList.Length] - Random.Range(-tolerance, tolerance), Vector3.forward);

                
            }

            else
            {
                //float num = RepsList[cycle % RepsList.Length] - Random.Range(-tolerance, tolerance);
                //Debug.Log("Range: " + num);

                if (isLeftHand == true)
                    arcRotator.transform.rotation = Quaternion.AngleAxis(RepsList[cycle % RepsList.Length], Vector3.forward);   //Static value for even value
                else
                    arcRotator.transform.rotation = Quaternion.AngleAxis(-1 * RepsList[cycle % RepsList.Length], Vector3.forward);
            }
            // Prevent repeating taget at end of each cycle
            if (cycle == 4 || cycle == 9)
                cycle++;

            cycle++;
        }

        // RIGHT HAND ONLY
        else
        {
            if (2 % RepsList.Length == 1)       //If RepsList value is odd, adds random element to a target position
            {
                arcRotator.transform.rotation = Quaternion.AngleAxis(RepsList[cycle % RepsList.Length] - Random.Range(-tolerance, tolerance), Vector3.forward);
            }

            else
            {
                arcRotator.transform.rotation = Quaternion.AngleAxis(RepsList[cycle % RepsList.Length], Vector3.forward);   //Static value for even value
            }

            /*GameObject point = Instantiate(puckpoint) as GameObject;    //Adds an indicator object to score meter
            point.transform.position = new Vector2(meter.transform.position.x + 0f, meter.transform.position.y + 6f);   //Gives the position to indicator object

            if (cycle % 5 == 0)             //Every fifth indicator object is different color
            {
                point.GetComponent<SpriteRenderer>().color = Color.black;
            }

            else
            {
                point.GetComponent<SpriteRenderer>().color = Color.yellow;      //Default color for indicator objects
            }*/
            cycle++;
        }

        //hasPuckCollided = false;
        //Debug.Log(cycle);
    }
}

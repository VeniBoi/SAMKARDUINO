using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class CanvasManip : MonoBehaviour {

    public BTLEConnect6 B6;
    public target_random TR, TR2;
    public Font myFont;
    public GameObject ShoulderRDemo, BreakPanel, EndPanel, Target, Target2, Point;
    public Quaternion ShoulderRDemoLiike1, ShoulderRDemoLiikeMiddle, ShoulderRDemoLiike2, startPos;
    public int scoreAmount, scoreRight, scoreLeft;
    public Text myText, ROMRTitle, ROMLAmount, ROMRAmount, RepsAmount, SetsAmount, RecoveryTimeAmount, WeighingAmount, MaxTimeToTargetAmount, ScoreValue, BreakScoreText, BreakTimeText, EndPanelText, GameInfoText;
    public Slider ROMLSlider, ROMRSlider, RepsSlider, SetsSlider, RecoveryTimeSlider, WeighingSlider, MaxTimeToTargetSlider;
    public Toggle SoundToggle, MapToFullROM;
    private float breakTimeAmount, setAmount;
    private bool continued = false;

    public GameObject number1;
    public GameObject number2;
    public GameObject number3;
    public GameObject goLabel;

    public int is2PuckShooter; // 1=true

    public Text goalsSavedText, shotsOnGoalText, savePercentageText;
    public Text goalsSavedEndText, shotsOnGoalEndText, savePercentageEndText;
    public int shotsOnGoal = 0;
    public int totalSaves = 0;
    float savePercentage = 0.0f;

  
    // Use this for initialization
    void Start()
    {
        SoundManager.instance.UnmuteBackgroundSounds();

        ShoulderRDemoLiike1 = new Quaternion(0.0f, 0.0f, 0.6f, 0.8f);
        ShoulderRDemoLiikeMiddle = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        ShoulderRDemoLiike2= new Quaternion(0.0f, 0.0f, -0.4f, 0.9f);
        startPos = ShoulderRDemo.transform.rotation;

        // Set Slider values
        if (PlayerPrefs.HasKey("ROMLSlider"))
            ROMLSlider.value = PlayerPrefs.GetFloat("ROMLSlider");
        else
            ROMLSlider.value = 90.0f / 5.0f;

        if (PlayerPrefs.HasKey("ROMRSlider"))
            ROMRSlider.value = PlayerPrefs.GetFloat("ROMRSlider");
        else
            ROMRSlider.value = 90.0f / 5.0f;

        if (PlayerPrefs.HasKey("RepsSlider"))
            RepsSlider.value = PlayerPrefs.GetFloat("RepsSlider");
        else
            RepsSlider.value = 10.0f;

        if (PlayerPrefs.HasKey("SetsSlider"))
            SetsSlider.value = PlayerPrefs.GetFloat("SetsSlider");
        else
            SetsSlider.value = 2.0f;

        if (PlayerPrefs.HasKey("RecoveryTimeSlider"))
            RecoveryTimeSlider.value = PlayerPrefs.GetFloat("RecoveryTimeSlider");
        else
            RecoveryTimeSlider.value = 10.0f;

        if (PlayerPrefs.HasKey("WeighingSlider"))
            WeighingSlider.value = PlayerPrefs.GetFloat("WeighingSlider");
        else
            WeighingSlider.value = 0.0f;

        if (PlayerPrefs.HasKey("MaxTimeToTargetSlider"))
            MaxTimeToTargetSlider.value = PlayerPrefs.GetFloat("MaxTimeToTargetSlider");
        else
            MaxTimeToTargetSlider.value = 1.0f;

        if (PlayerPrefs.HasKey("SoundToggleValue"))
        {
            if (PlayerPrefs.GetInt("SoundToggleValue") == 1)
                SoundToggle.isOn = true;
            else
                SoundToggle.isOn = false;
        }
        else
            SoundToggle.isOn = true;

        if (PlayerPrefs.HasKey("MapToFullROM"))
        {
            if (PlayerPrefs.GetInt("MapToFullROM") == 1)
                MapToFullROM.isOn = true;
            else
                MapToFullROM.isOn = false;
        }
        else
            MapToFullROM.isOn = false;

        



    }

    // Update is called once per frame
    void Update()
    {
        // Sound
        SoundManager.instance.ToggleSoundOnorOff();

        ScoreValue.text = "Score: " + scoreAmount.ToString();
        ROMLAmount.text = (ROMLSlider.value * 5.0f).ToString() + "°";
        ROMRAmount.text = (ROMRSlider.value * 5.0f).ToString() + "°";
        //RepsAmount.text = (int)(RepsSlider.value / 3) + " full reps + " + (RepsSlider.value % 3);
        RepsAmount.text = (int)(RepsSlider.value / 1) + " puck saves (i.e. reps)";
        SetsAmount.text = SetsSlider.value.ToString();
        RecoveryTimeAmount.text = RecoveryTimeSlider.value.ToString() + " seconds";
        WeighingAmount.text = WeighingSlider.value.ToString();


        // Save Slider values
        PlayerPrefs.SetFloat("ROMLSlider", ROMLSlider.value);
        PlayerPrefs.SetFloat("ROMRSlider", ROMRSlider.value);
        PlayerPrefs.SetFloat("RepsSlider", RepsSlider.value);
        PlayerPrefs.SetFloat("SetsSlider", SetsSlider.value);
        PlayerPrefs.SetFloat("RecoveryTimeSlider", RecoveryTimeSlider.value);
        PlayerPrefs.SetFloat("WeighingSlider", WeighingSlider.value);
        PlayerPrefs.SetFloat("MaxTimeToTargetSlider", MaxTimeToTargetSlider.value);

        if (SoundToggle.isOn == true)
            PlayerPrefs.SetInt("SoundToggleValue", 1);
        else
            PlayerPrefs.SetInt("SoundToggleValue", 0);

        if (MapToFullROM.isOn == true)
            PlayerPrefs.SetInt("MapToFullROM", 1);
        else
            PlayerPrefs.SetInt("MapToFullROM", 0);








        if (MaxTimeToTargetSlider.value < 20.0f)
        {
            MaxTimeToTargetAmount.fontSize = 16;
            MaxTimeToTargetAmount.text = MaxTimeToTargetSlider.value.ToString();
        }
        else
        {
            MaxTimeToTargetAmount.fontSize = 24;
            MaxTimeToTargetAmount.text = "∞";
        }

        // End panel appears
        if ((scoreAmount) >= RepsSlider.value && setAmount == (SetsSlider.value-1))
        {
            StopLoggingWorkoutTime();

            SoundManager.instance.PlaySingle(GameControl.instance.buzzerAudio, 0.8f);

            EndPanel.SetActive(true);
            EndPanelText.text = "Congratulations!\nMatch practice ís complete for today!";

            // Update statistics
            totalSaves = totalSaves + scoreAmount;
            goalsSavedEndText.text = totalSaves.ToString();
            shotsOnGoalEndText.text = shotsOnGoal.ToString();
            savePercentage = 100.0f * (float)totalSaves / (float)shotsOnGoal;
            savePercentageEndText.text = savePercentage.ToString("F0") + " %";

            Target.SetActive(false);

            if (WeighingSlider.value == 2)
            {
                Target2.SetActive(false);
            }

                /*
                if (PlayerPrefs.GetInt("2PuckShooter") == 1)
                    Target2.SetActive(false);
                */

            scoreAmount = 0;
            setAmount = 0;
        }

        // Break panel appears if reps are max and it wasn't the last set
        if ((scoreAmount) >= RepsSlider.value && setAmount != SetsSlider.value)
        {
            StopLoggingWorkoutTime();
            StartLoggingRestTime();

            SoundManager.instance.PlaySingle(GameControl.instance.buzzerAudio, 0.8f);

            breakTimeAmount = RecoveryTimeSlider.value;
            setAmount++;
            BreakPanel.SetActive(true);

            Target.SetActive(false);
            if (WeighingSlider.value == 2)
            {
                Target2.SetActive(false);
            }

                /*
                if (PlayerPrefs.GetInt("2PuckShooter") == 1)
                    Target2.SetActive(false);
                */

            continued = false;
            StartCoroutine(BreakTimer());
            scoreAmount = 0;
        }

        if (WeighingSlider.value == -1)
            WeighingAmount.text = "Left arm ONLY";
        if (WeighingSlider.value == 1)
            WeighingAmount.text = "Right arm ONLY";
        if (WeighingSlider.value == 0)
            WeighingAmount.text = "Both arms (1 arm at a time)";
        if (WeighingSlider.value == 2)
        {
            WeighingAmount.text = "Both arms (at same time)";

            /*
            ROMRSlider.interactable = false;
            ROMRAmount.text = "";

            Color titlecolor = ROMRTitle.color;
            titlecolor.a = 0.20f;
            ROMRTitle.color = titlecolor;
            */
        }

            



        //GameControl.instance.completedRepsLeft = scoreRight;            // Note: Arms are mirrored
        //GameControl.instance.completedRepsRight = scoreLeft;

    }

    //Continues the game (new set) and destroys the point markers
    public void ToContinue()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("point");
        foreach(GameObject go in gos)
        { Destroy(go); }
        continued = true;

        Target.SetActive(true);
        TR.cycle = 0;
        //TR.replace();

        if (WeighingSlider.value == 2)
        {
            Target2.SetActive(true);
            TR2.cycle = 0;
            //TR2.replace();
        }

        // Align cycles for new set
        if (GameControl.instance.completedSets > 1)
        {
            TR.cycle = TR2.cycle = 1;
        }
           

    }

    //Shows the break panel between sets
    public IEnumerator BreakTimer()
    {
        /*
        TR.cycle = 1;
        if (WeighingSlider.value == 2)
        {
            TR2.cycle = 1;
        }
        */

        // Update statistics
        totalSaves = totalSaves + scoreAmount;
        goalsSavedText.text = totalSaves.ToString();
        shotsOnGoalText.text = shotsOnGoal.ToString();
        savePercentage = 100.0f * (float)totalSaves / (float)shotsOnGoal;
        savePercentageText.text = savePercentage.ToString("F0") + " %";


        while (breakTimeAmount <= RecoveryTimeSlider.value && breakTimeAmount >= 0)
        {
            //BreakScoreText.text = "Well done! You saved " + RepsSlider.value + " pucks! \n\nNext round begins in " + breakTimeAmount + " seconds!";
            BreakScoreText.text = "Next period begins in " + breakTimeAmount + " seconds!";
            breakTimeAmount--;
            yield return new WaitForSecondsRealtime(1f);
        }
        BreakPanel.SetActive(false);

        if (continued == false)
            ContinueWorkout();

        StopCoroutine(BreakTimer());
    }

    public void ContinueWorkout()
    {
        if (WeighingSlider.value == 1)
            TR.cycle = 5;
        else
            TR.cycle = 0;

        TR.replace();
        Target.SetActive(true);

        if (WeighingSlider.value == 2)     // both arms at same time selected
        {
            
            TR2.cycle = 0;
            TR2.replace();
            Target2.SetActive(true);
        }
        

        StopLoggingRestTime();
        StartLoggingWorkoutTime();
        GameControl.instance.completedSets++;

        ToContinue();

        /*
        if (continued == false)
        {
            ToContinue();
        }
        */
    }


    //Function to start the "start game"-function
    public void ToStartTheGame()
    {
        StartCoroutine(StartTheGame());
    }

    //The "start game"-function
    public IEnumerator StartTheGame()
    {
        continued = false;
        scoreAmount = 0;                                    //Set the score to zero
        ToContinue();                                       //Makes sure there are no point markers on the screen
        //GameInfoText.text = "Get into starting position...";
        //StartCoroutine(B6.LockQuaternion());
        //GameInfoText.text = "Ready?";                       //Countdown start

        yield return new WaitForSeconds(1.5f);                            
        //GameInfoText.text = "3";
        GameObject shot3 = Instantiate(number3) as GameObject;
        shot3.SetActive(true);
        SoundManager.instance.PlaySingle(GameControl.instance.threeAudio, 0.8f);

        yield return new WaitForSecondsRealtime(1.5f);
        //GameInfoText.text = "2";
        Destroy(shot3);
        GameObject shot2 = Instantiate(number2) as GameObject;
        shot2.SetActive(true);
        SoundManager.instance.PlaySingle(GameControl.instance.twoAudio, 0.8f);

        yield return new WaitForSecondsRealtime(1.5f);
        //GameInfoText.text = "1";
        Destroy(shot2);
        GameObject shot = Instantiate(number1) as GameObject;
        shot.SetActive(true);
        SoundManager.instance.PlaySingle(GameControl.instance.oneAudio, 0.8f);

        yield return new WaitForSecondsRealtime(1.5f);
        //GameInfoText.text = "Go!";
        Destroy(shot);
        GameObject goText = Instantiate(goLabel) as GameObject;
        goText.SetActive(true);
        SoundManager.instance.PlaySingle(GameControl.instance.buzzerAudio, 0.8f);

        yield return new WaitForSeconds(1f);
        Destroy(goText);

        yield return new WaitForSeconds(2f);

        //ToContinue();

        // Reset timer if countdown (new workout)
        GameControl.instance.totalTimeOfWorkout = 0.0f;
        StopLoggingRestTime();
        GameControl.instance.totalTimeAtRest = 0.0f;        
        StartLoggingWorkoutTime();
        


        GameInfoText.text = "";                 //Countdown end
        if (WeighingSlider.value == -1)         //if left weighing is selected
        {
            TR.cycle = 0;
            TR.replace();
      
        }
        else if (WeighingSlider.value == 1)     //if right weighing is selected
        {
            TR.cycle = 5;
            TR.replace();
        }

        else if (WeighingSlider.value == 2)     // both arms at same time selected
        {
            // Shoot both pucks
            TR.cycle = 0;
            TR.replace();

            TR2.cycle = 0;
            TR2.replace();
        }

        else                                    //if neutral weighing is selected (both arms, one at a time)
        {
            TR.replace();
        }

        StopCoroutine(StartTheGame());
    }


    public void StartLoggingWorkoutTime()
    {
        if (GameControl.instance.startWorkoutTimer == true)
        {
            GameControl.instance.workoutTimer = Time.time;
            GameControl.instance.startWorkoutTimer = false;
            //Debug.Log("Start Workout Timer. Time = " + GameControl.instance.totalTimeOfWorkout.ToString());
        }
    }

    public void StopLoggingWorkoutTime()
    {
        GameControl.instance.totalTimeOfWorkout = GameControl.instance.totalTimeOfWorkout + (Time.time - GameControl.instance.workoutTimer);
        GameControl.instance.startWorkoutTimer = true;
        //Debug.Log("Stop Workout Timer. Time = " + GameControl.instance.totalTimeOfWorkout.ToString());
    }

    public void StartLoggingRestTime()
    {
        if (GameControl.instance.startRestTimer == true)
        {
            GameControl.instance.restTimer = Time.time;
            GameControl.instance.startRestTimer = false;
            //Debug.Log("Start Rest Timer. Time = " + GameControl.instance.totalTimeAtRest.ToString());
        }
    }

    public void StopLoggingRestTime()
    {
        if (GameControl.instance.startRestTimer == false)
        {
            GameControl.instance.totalTimeAtRest = GameControl.instance.totalTimeAtRest + (Time.time - GameControl.instance.restTimer);
            GameControl.instance.startRestTimer = true;
            //Debug.Log("Stop Rest Timer. Time = " + GameControl.instance.totalTimeAtRest.ToString());
        }
          
    }



    public void ResetButtonClicked()
    {
        SceneManager.LoadScene("IntroScene");
    }
}

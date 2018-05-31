using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// For data serialisation (saving + loading method data)
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameControl : MonoBehaviour {


    // Instance for GameControl
    public static GameControl instance;         //A reference to our game control script so we can access it statically


    // Variables for data saving
    public int completedRepsLeft = 0;           // from puck_destroy -> Saved in CM
    public int completedRepsRight = 0;          // from puck_destroy -> Saved in CM
    public int completedSets = 1;               // Saved in CM
    public float totalTimeAtRest = 0.0f;        // Saved in CM
    public float totalTimeOfWorkout = 0.0f;     // Saved in CM
    public float maxRomAchievedLeft = 0.0f;     // Saved in BTLEConnect6
    public float maxRomAchievedRight = 0.0f;    // Saved in BTLEConnect6


    public float workoutTimer = 0.0f;           
    public bool startWorkoutTimer = true;       

    public float restTimer = 0.0f;
    public bool startRestTimer = true;

    public int puckShotTimer = 0;
    public bool isUpRep = false;        

    // Audio
    public AudioClip thudSoundAudio;
    public AudioClip crowdCheerAudio;
    public AudioClip buzzerAudio;
    public AudioClip threeAudio;
    public AudioClip twoAudio;
    public AudioClip oneAudio;
    public AudioClip whooshAudio;




    void Awake()
    {
        //If we don't currently have a game control
        if (instance == null)
        {
            //...set this one to be it...
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGameButtonPressed()
    {
        SoundManager.instance.MuteBackgroundSounds();
        SaveDataControl.instance.Save();
        SceneManager.LoadScene("ResultsScene");
    }
}

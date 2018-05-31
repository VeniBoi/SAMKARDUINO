using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For data serialisation (saving + loading method data)
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveDataControl : MonoBehaviour {

    // Instance for GameControl
    public static SaveDataControl instance;         //A reference to our game control script so we can access it statically.


    // Loaded Data
    public string userName;
    public DateTime currentLoadedDateOfWorkout;
    public int currentLoadedRepsLeft;
    public int currentLoadedRepsRight;
    public int currentLoadedSets;
    public float currentLoadedRestTimeTotal;
    public float currentLoadedWorkoutTimeTotal;
    public float currentLoadedRomLeft;
    public float currentLoadedRomRight;



    void Awake()
    {
        //If we don't currently have a game control
        if (instance == null)
        {
            //...set this one to be it...
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }



    public void Save()
    {
        //Debug.Log("SAVE TIME: " + userName);


        // Set-up file for saving
        BinaryFormatter bf = new BinaryFormatter();
        
        // Test if username exists
        if (String.IsNullOrEmpty(SaveDataControl.instance.userName))
        {
            userName = "Unknown Client";
        }
        else
        {
            userName = SaveDataControl.instance.userName;
        }

        // File format (reverse ordered to help with ordering)
        string fileName = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        
        
        // Create directory for userName
        if (!Directory.Exists(Application.persistentDataPath + "/" + userName))
        {
            Debug.Log("Create Dir");
            Directory.CreateDirectory(Application.persistentDataPath + "/" + userName);
        }

        // Create file
        FileStream file = File.Create(Application.persistentDataPath + "/" + userName + "/" + fileName + ".dat");


        // Data to save
        CurrentWorkoutData data = new CurrentWorkoutData();

        data.dateOfWorkout = System.DateTime.Now;
        data.repsCompletedLeft = GameControl.instance.completedRepsLeft;
        data.repsCompletedRight = GameControl.instance.completedRepsRight;
        data.setsCompleted = GameControl.instance.completedSets;
        data.restTimeTotal = GameControl.instance.totalTimeAtRest;
        data.workoutTimeTotal = GameControl.instance.totalTimeOfWorkout;
        data.maxROMAchievedLeft = GameControl.instance.maxRomAchievedLeft;
        data.maxROMAchievedRight = GameControl.instance.maxRomAchievedRight;
        //data.maxROMAchievedLeft = 92.9f;
        //data.maxROMAchievedRight = 154.3f;

        // Save data
        bf.Serialize(file, data);
        file.Close();
    }



    public void Load(string workoutDateName)
    {
        if (String.IsNullOrEmpty(SaveDataControl.instance.userName))
        {
            userName = "Unknown Client";
        }
        else
        {
            userName = SaveDataControl.instance.userName;
        }
        

        // Passed file name
        string fileName = workoutDateName;

        if (File.Exists(Application.persistentDataPath + "/" + userName + "/" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + userName + "/" + fileName, FileMode.Open);

            CurrentWorkoutData data = (CurrentWorkoutData)bf.Deserialize(file);
            
            file.Close();

            // Can now access data elements
            currentLoadedDateOfWorkout = data.dateOfWorkout;
            currentLoadedRepsLeft = data.repsCompletedLeft;
            currentLoadedRepsRight = data.repsCompletedRight;
            currentLoadedSets = data.setsCompleted;
            currentLoadedRestTimeTotal = data.restTimeTotal;
            currentLoadedWorkoutTimeTotal = data.workoutTimeTotal;
            currentLoadedRomLeft = data.maxROMAchievedLeft;
            currentLoadedRomRight = data.maxROMAchievedRight;
            

        }
    }





// Use this for initialization
void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


// Need [Serializable] to format data for saving
[Serializable]
public class CurrentWorkoutData
{
    public DateTime dateOfWorkout;

    public int repsCompletedLeft;
    public int repsCompletedRight;

    public int setsCompleted;

    public float restTimeTotal;
    public float workoutTimeTotal;

    public float maxROMAchievedLeft;
    public float maxROMAchievedRight;

}

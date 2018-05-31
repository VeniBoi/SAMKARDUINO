using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;   // needed for using Lists

// For data serialisation (saving + loading method data)
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadData : MonoBehaviour {

    public Text scoreText;
    public Text titleText;
    

    public Dropdown workoutHistoryDropdown;
    CurrentWorkoutData data;

    //public int[] datesOfWorkout = {1,2,3};
    public string listOfFiles;
    //public string[] fileNames;
    
    List<Dropdown.OptionData> dropdownOptions;


    void OnEnable()
    {
        // Add listener for dropdown
        workoutHistoryDropdown.onValueChanged.AddListener(delegate { onWorkoutHistorChange(); });


        // While working on result scene
        //SaveDataControl.instance.userName = "Firstname Secondname";
        if (String.IsNullOrEmpty(SaveDataControl.instance.userName))
        {
            SaveDataControl.instance.userName = "Unknown Client";
        }
        else
        {
            //SaveDataControl.instance.userName = SaveDataControl.instance.userName;
        }

        //Debug.Log(Application.persistentDataPath);
        string myPath = Application.persistentDataPath + "/" + SaveDataControl.instance.userName;
        listOfFiles = myPath + "\n\n";

        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            listOfFiles += "\n" + f.Name.ToString();

            //fileNames[i] = f.Name.ToString();
            //i++;

            workoutHistoryDropdown.options.Add(new Dropdown.OptionData(f.Name.ToString()));
        }

        scoreText.text = listOfFiles;


        // Set results to latest value
        dropdownOptions = workoutHistoryDropdown.GetComponent<Dropdown>().options;

        int numberOfResults = dropdownOptions.Count;
        workoutHistoryDropdown.value = numberOfResults - 1;

    }


    // Use this for initialization
    void Start ()
    {
        

        titleText.text = "WORKOUT RESULTS:\t" + SaveDataControl.instance.userName;

        // Displays results if only one data entry
        onWorkoutHistorChange();

    }
	
    public void closeButtonPressed()
    {
        SceneManager.LoadScene("StartScene");
    }

	// Update is called once per frame
	void Update () {
		
	}

    // Called when load button pressed
    /*
    public void LoadDataFromGame()
    {
        //Load();
        //GameControl.instance.Load();
        SaveDataControl.instance.Load("playerInfo");
        
        //data = GameControl.loadedData;

        scoreText.text = "Workout Date:\t" + SaveDataControl.instance.currentLoadedDateOfWorkout + 
            "\nReps:\t\t\t\t" + SaveDataControl.instance.currentLoadedReps + 
            "\nSets:\t\t\t\t" + SaveDataControl.instance.currentLoadedSets + 
            "\nMax ROM:\t\t" + SaveDataControl.instance.currentLoadedRom + "  degrees" +
            "\nRest Time:\t\t" + SaveDataControl.instance.currentLoadedRestTimeTotal + "  seconds";
    }
    */


    public void onWorkoutHistorChange()
    {
        
        //List<Dropdown.OptionData> dropdownOptions = workoutHistoryDropdown.GetComponent<Dropdown>().options;
        string selectedWorkout = dropdownOptions[workoutHistoryDropdown.value].text;
        //Debug.Log(selectedWorkout);

        SaveDataControl.instance.Load(selectedWorkout);

        
        scoreText.text = "Workout Date:\t\t\t\t" + SaveDataControl.instance.currentLoadedDateOfWorkout.ToString("yyyy-MM-dd (hh:mm.ss)") +
            "\nTotal Reps (Left):\t\t\t\t" + SaveDataControl.instance.currentLoadedRepsLeft +
            "\nTotal Reps (Right):\t\t\t" + SaveDataControl.instance.currentLoadedRepsRight +
            "\nSets Completed:\t\t\t\t" + SaveDataControl.instance.currentLoadedSets +
            "\nMax ROM (Left):\t\t\t\t" + SaveDataControl.instance.currentLoadedRomLeft.ToString("F0") + "  degrees" +
            "\nMax ROM (Right):\t\t\t" + SaveDataControl.instance.currentLoadedRomRight.ToString("F0") + "  degrees" +
            "\nTotal Workout Time:\t\t" + SaveDataControl.instance.currentLoadedWorkoutTimeTotal.ToString("F0") + "  seconds" +
            "\nTotal Rest Time:\t\t\t\t" + SaveDataControl.instance.currentLoadedRestTimeTotal.ToString("F0") + "  seconds";

        
    }


    /*
    public void Load()
    {
        string fileName = "/playerInfo.dat";
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);

            CurrentWorkoutData data = (CurrentWorkoutData)bf.Deserialize(file);

            file.Close();

            // can now access data (example below)
            DateTime dateOfWorkout = data.dateOfWorkout;
            int reps = data.repsCompleted;
            int sets = data.setsCompleted;
            float rom = data.maxROMAchieved;

            //loadedData = data;
            scoreText.text = "Workout Date:\t" + dateOfWorkout + "\nReps:\t\t\t\t" + reps + "\nSets:\t\t\t\t" + sets + "\nMax ROM:\t\t" + rom;

        }


    }
    */
}


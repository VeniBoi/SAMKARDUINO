using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class StartSceneControl : MonoBehaviour {

    public GameObject clientSettingsPanel;
    public InputField newClientInputField;
    public Dropdown savedUsernamesDropdown;

    public GameObject warningPanel;
    public Text warningText;

    public Text currentUsername;
    List<Dropdown.OptionData> dropdownOptions;

    // Use this for initialization
    void Start ()
    {
        updateUsernameText();

        // Autorotate settings (landscape only)
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
         
    }


    // Update is called once per frame
    void Update()
    {
        updateUsernameText();
    }

    

    void updateUsernameText()
    {
        if (PlayerPrefs.HasKey("savedUsername"))
        {
            // there is a saved username, load that one
            currentUsername.text = "Client ID:  " + PlayerPrefs.GetString("savedUsername");
            SaveDataControl.instance.userName = PlayerPrefs.GetString("savedUsername");
        }
        else
        {
            // no saved user, load unknown user
            currentUsername.text = "Client ID:  Unknown User";
            SaveDataControl.instance.userName = "Unknown User";

            // Create directory if required
            if (!Directory.Exists(Application.persistentDataPath + "/" + SaveDataControl.instance.userName))
                Directory.CreateDirectory(Application.persistentDataPath + "/" + SaveDataControl.instance.userName);
        }
    }


    void OnEnable()
    {
        // Add listener for dropdown
        savedUsernamesDropdown.onValueChanged.AddListener(delegate { onUsernameChange(); });

        // Display folders
        string[] dir = Directory.GetDirectories(Application.persistentDataPath);

        
        foreach (string d in dir)
        {
            //Debug.Log(Path.GetFileName((d)));
            if (Path.GetFileName(d) != "Unity")
                savedUsernamesDropdown.options.Add(new Dropdown.OptionData(Path.GetFileName(d)));
               
        }

    }

    void onUsernameChange()
    {
        string selectedUsername = dropdownOptions[savedUsernamesDropdown.value].text;

        SaveDataControl.instance.userName = selectedUsername;
        PlayerPrefs.SetString("savedUsername", selectedUsername);
        PlayerPrefs.SetInt("Username Index", savedUsernamesDropdown.value);
        
    }


    public void startButtonPressed()
    {
        SceneManager.LoadScene("Hockey");
    }

    public void displayClientSettingsPanel()
    {
        dropdownOptions = savedUsernamesDropdown.GetComponent<Dropdown>().options;
        clientSettingsPanel.SetActive(true);

        // update dropdown to show selection
        int i = 0;
        string[] dir = Directory.GetDirectories(Application.persistentDataPath);
        foreach (string d in dir)
        {
            //Debug.Log(Path.GetFileName((d)));

            if (Path.GetFileName(d) != "Unity")
            {
                if (Path.GetFileName(d) == SaveDataControl.instance.userName)
                {
                    savedUsernamesDropdown.value = i;
                }
                else
                {
                    i++;
                }

                //savedUsernamesDropdown.options.Add(new Dropdown.OptionData(Path.GetFileName(d)));



            } 

        }

    }

    public void closeClientSettingsButtonPressed()
    {
        clientSettingsPanel.SetActive(false);
    }





    public void saveNewClientButtonPressed()
    {

        if (string.IsNullOrEmpty(newClientInputField.text))
        {
            //Debug.Log("NO TEXT ENTERED!");

            warningText.text = "Please enter a client ID or name.";
            warningPanel.SetActive(true);
        }
        else
        {
            // Save new user and create directory 
            SaveDataControl.instance.userName = newClientInputField.text;
            PlayerPrefs.SetString("savedUsername", newClientInputField.text);

            // Create directory if required
            if (!Directory.Exists(Application.persistentDataPath + "/" + SaveDataControl.instance.userName))
                Directory.CreateDirectory(Application.persistentDataPath + "/" + SaveDataControl.instance.userName);


            // Update text display
            currentUsername.text = "Client ID:" + PlayerPrefs.GetString("savedUsername");

            // Reload scene (also updates dropdown)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);



            
        }


        




        //PlayerPrefs.SetInt("Username Index", savedUsernamesDropdown.value);
        //updateDropdownForUsername();
        //Debug.Log(SaveDataControl.instance.userName);
    }

    public void LoadClientWorkoutDataButtonPressed()
    {
        //SceneManager.LoadScene("ResultsScene");

        string myPath = Application.persistentDataPath + "/" + SaveDataControl.instance.userName;
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles("*.*");


        if (info.Length > 0)
        {
            SceneManager.LoadScene("ResultsScene");
                       
        }
        else
        {
            //Debug.Log("NO workout history!");

            warningText.text = "This client does not have any workout data recorded yet.";
            warningPanel.SetActive(true);
        }

    }


    public void closeWarningButtonPressed()
    {
        warningPanel.SetActive(false);
    }


}

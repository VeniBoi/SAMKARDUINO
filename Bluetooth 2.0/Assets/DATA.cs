using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class DATA : MonoBehaviour {

	static public string nimi, readPath;
	public InputField input;
	public GameObject finalPanel, playPanel, restartButton;

	[SerializeField]
	private GameObject buttonTemplate;

	public void createText()
	{
		
		//Path of the file
		//Tähän henkilön nimi, ja sen perään .txt
		string path = Application.dataPath + "/datakansio/" + input.text + ".txt";	
		
		//Content of file
		string content = kameraScript.totalPoints.ToString() + "," + System.DateTime.Now + "\n";

		//Add some text to the file
		File.AppendAllText(path, content);
		readPath = path;
		

		kameraScript.totalPoints = 0;
		input.text = "";
		finalPanel.SetActive(false);
		playPanel.SetActive(true);
		restartButton.SetActive(true);
	}


	// Use this for initialization
	void Start()
	{

		//1---------------Haetaan tiedostot nimella------------------------------//
		string myPath = Application.dataPath + "/datakansio/";
		DirectoryInfo dir = new DirectoryInfo(myPath);
		FileInfo[] info = dir.GetFiles("*.*");
		foreach (FileInfo f in info)
		{

			string z = f.ToString();
			//Debug.Log(Path.GetFileName(z));
			string fileName = Path.GetFileName(z); // juho.txt || juho.txt.meta
			string[] nameSplitted = fileName.Split('.'); // ["juho", "txt"] || ["juho", "txt", "meta"]
			string ending = nameSplitted[nameSplitted.Length - 1]; // 2 - 1 == 1 || 3 - 1 == 2

			if (ending.Equals("meta")) {
				continue;
			}
			// lisäää nimi listaan asd

			Debug.Log(nameSplitted[0]);

			GameObject button = Instantiate(buttonTemplate) as GameObject;
			button.SetActive(true);

			button.GetComponent<buttonListButton>().SetText(nameSplitted[0]);

			button.transform.SetParent(buttonTemplate.transform.parent, false);


		}

		
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}

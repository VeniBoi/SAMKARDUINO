using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class DATA : MonoBehaviour {

	static public string nimi;
	public InputField input;

	public void createText()
	{
		
		//Path of the file
		//Tähän henkilön nimi, ja sen perään .txt
		string path = Application.dataPath + "/" +  "datakansio/" + input.text + ".txt";	
		
		//Content of file
		string content = kameraScript.totalPoints.ToString() + "," + System.DateTime.Now + "\n";

		//Add some text to the file
		File.AppendAllText(path, content);

		
		kameraScript.totalPoints = 0;
		input.text = "";
	}
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

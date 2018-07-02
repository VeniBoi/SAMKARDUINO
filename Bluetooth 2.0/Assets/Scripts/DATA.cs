﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;

public class DATA : MonoBehaviour {

	static public string nimi, readPath;
	public InputField input;
	public GameObject finalPanel, playPanel, restartButton, datapanel, datanappi, backNappi;

	[SerializeField]
	private GameObject buttonTemplate;

	public void backButton()
	{
		datanappi.SetActive(true);
		backNappi.SetActive(false);
		restartButton.SetActive(true);
		playPanel.SetActive(true);
		datapanel.SetActive(false);
	}

	public void createText()
	{

		
		DirectoryInfo dirInf = new DirectoryInfo(Application.persistentDataPath + "/" + "datakansio");

		if (!dirInf.Exists)
		{
			Debug.Log("Creating subdirectory");
			dirInf.Create();
		}

		
		

		//Path of the file
		//Tähän henkilön nimi, ja sen perään .txt
		string path = Application.persistentDataPath + "/datakansio/" + input.text + ".csv";

		if (!File.Exists(path))
		{
			File.WriteAllText(path, "Score,Date" + "\n");

		}

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
		Debug.Log(path);
		datanappi.SetActive(true);
	}

	public void dataSivu()
	{
		//1---------------Haetaan tiedostot nimella------------------------------//
		string myPath = Application.persistentDataPath + "/datakansio/";
		DirectoryInfo dir = new DirectoryInfo(myPath);
		FileInfo[] info = dir.GetFiles("*.*");
		foreach (FileInfo f in info)
		{

			string z = f.ToString();
			//Debug.Log(Path.GetFileName(z));
			string fileName = Path.GetFileName(z); // juho.txt || juho.txt.meta
			string[] nameSplitted = fileName.Split('.'); // ["juho", "txt"] || ["juho", "txt", "meta"]
			string ending = nameSplitted[nameSplitted.Length - 1]; // 2 - 1 == 1 || 3 - 1 == 2

			if (ending.Equals("meta"))
			{
				continue;
			}
			// lisäää nimi listaan asd

			Debug.Log(nameSplitted[0]);

			GameObject button = Instantiate(buttonTemplate) as GameObject;
			button.SetActive(true);

			button.GetComponent<buttonListButton>().SetText(nameSplitted[0]);

			button.transform.SetParent(buttonTemplate.transform.parent, false);

			datapanel.SetActive(true);
			playPanel.SetActive(false);
			datanappi.SetActive(false);
			backNappi.SetActive(true);
			restartButton.SetActive(false);
			

		}
	}
	// Use this for initialization
	void Start()
	{

		

		
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}

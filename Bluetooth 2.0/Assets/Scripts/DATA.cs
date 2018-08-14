using System.Collections;
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
	public GameObject finalPanel, playPanel, restartButton, datapanel, datanappi, backNappi, areyousurepanel;
	public Text deletedText;

	[SerializeField]
	private GameObject buttonTemplate;


	public void deleteFilesYES()
	{
		string myPath = Application.persistentDataPath + "/datakansio/";
		DirectoryInfo dir = new DirectoryInfo(myPath);
		FileInfo[] info = dir.GetFiles("*.*");
		foreach (FileInfo f in info)
		{
			string z = f.ToString();
			string fileName = Path.GetFileName(z);
			File.Delete(Application.persistentDataPath + "/datakansio/" + fileName);

		}
		StartCoroutine(deleteText());

	}

	public void deleteFilesNO()
	{
		areyousurepanel.SetActive(false);
	}

	public void deletePanel()
	{
		areyousurepanel.SetActive(true);
	}

	public void backButton()
	{


		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("nimiNappi");

		for (var i = 0; i < gameObjects.Length; i++)
		{
			if(gameObjects[i].activeInHierarchy)
			{
				Destroy(gameObjects[i]);
			}

			
		}

		datanappi.SetActive(true);
		backNappi.SetActive(false);
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
		string path = Application.persistentDataPath + "/datakansio/" + input.text + ".csv";

		if (!File.Exists(path))
		{
			File.WriteAllText(path, "Average_Time, Best_Time, Date" + "\n");

		}

		//Content of file
		string content = kavelyTestiScript.testValuesAdded.ToString("F2") + "," + kavelyTestiScript.testValuesBest.ToString("F2") + "," + System.DateTime.Now + "\n";

		//Add some text to the file
		File.AppendAllText(path, content);
		readPath = path;
		

		kameraScript.totalPoints = 0;
		input.text = "";
		finalPanel.SetActive(false);
		playPanel.SetActive(true);
		Debug.Log(path);
		datanappi.SetActive(true);
	}

	public void dataSivu()
	{

		datapanel.SetActive(true);
		playPanel.SetActive(false);
		datanappi.SetActive(false);
		backNappi.SetActive(true);
		restartButton.SetActive(false);
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

		}
	}
	// Use this for initialization
	void Start()
	{

		

		
		
	}
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator deleteText()
	{
		deletedText.text = "All Files Deleted!";
		yield return new WaitForSeconds(2.0f);
		areyousurepanel.SetActive(false);
		deletedText.text = "Are you sure you want to delete all files?";
	}
}

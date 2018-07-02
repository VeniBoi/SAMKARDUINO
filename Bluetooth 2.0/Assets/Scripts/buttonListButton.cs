using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class buttonListButton : MonoBehaviour {

	
	public Text myText, emailnimi;
	public static string nimi;


	public void SetText(string textString)
	{
		myText.text = textString;
		nimi = textString;
	}

	public void onClick()
	{
		//2----------------Avataan tiedosto henkilon nimella, joka on haettu alempana------------------------//
		nimi = myText.text;
		string path = Application.persistentDataPath + "/datakansio/" + nimi + ".csv";
		string[] lines = File.ReadAllLines(path);
		emailnimi.text = "File chosen: " + nimi;
		mailScript.nameChosen = true;

		foreach (string pisteet in lines)
		{
			string[] x = pisteet.Split(',');
			Debug.Log(x[0]);
		}
	}

	private void Start()
	{
		this.enabled = true;
	}

}

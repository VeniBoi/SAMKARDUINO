using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class buttonListButton : MonoBehaviour {


	public Text myText;
	string nimi;


	public void SetText(string textString)
	{
		myText.text = textString;
		nimi = textString;
	}

	public void onClick()
	{
		//2----------------Avataan tiedosto henkilon nimella, joka on haettu alempana------------------------//

		string path = Application.dataPath + "/datakansio/" + nimi + ".txt";
		string[] lines = File.ReadAllLines(path);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {

	public InputField inputfield;
	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

		if (Input.GetKeyDown("p"))
		{
			kameraScript.totalPoints = kameraScript.totalPoints + 10;
		}

		if(Input.GetKeyDown("a")){

			Debug.Log(kameraScript.totalPoints);
		}

	}

	public void AsetaTulos()
	{

		Highscores.AddNewHighscore(inputfield.text, kameraScript.totalPoints);

	}
}

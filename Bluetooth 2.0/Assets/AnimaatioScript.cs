using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaatioScript : MonoBehaviour {


	static public bool animaatio1;
	static public bool animaatio2;

	static public bool animaatio3;
	static public bool animaatio4;

	static public bool animaatio5;
	static public bool animaatio6;

	static public bool animaatio7;
	static public bool animaatio8;

	// Use this for initialization
	void Start () {
		animaatio1 = false;
		animaatio2 = true;

		animaatio3 = false;
		animaatio4 = true;

		animaatio5 = false;
		animaatio6 = true;

		animaatio7 = false;
		animaatio8 = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (animaatio1 == true && animaatio2 == true)
		{
			this.GetComponent<Animator>().Play("Lasku", -1, 0.0f);
			Debug.Log("Lasku animaatio alkaa");
			animaatio2 = false;
		}

		if (animaatio3 == true && animaatio4 == true)
		{
			this.GetComponent<Animator>().Play("Liito", -1, 0.0f);
			Debug.Log("Liito animaatio alkaa");
			animaatio4 = false;
		}

		if (animaatio5 == true && animaatio6 == true)
		{
			this.GetComponent<Animator>().Play("Telemark", -1, 0.0f);
			Debug.Log("Liito animaatio alkaa");
			animaatio6 = false;
		}

		if (animaatio7 == true && animaatio8 == true)
		{
			this.GetComponent<Animator>().Play("Pys_ytys", -1, 0.0f);
			Debug.Log("Nyt pysähdytään POIKITTAIN");
			animaatio8 = false;
		}
	}
}

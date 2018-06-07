using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaatioScript : MonoBehaviour {


	static public bool animaatio1;
	static public bool animaatio2;

	static public bool animaatio3;
	static public bool animaatio4;

	// Use this for initialization
	void Start () {
		animaatio1 = false;
		animaatio2 = true;

		animaatio3 = false;
		animaatio4 = true;
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
	}

	public void Lasku()
	{
		
		Debug.Log("Lasku animaatio alkaa");
	}

	public void Liito()
	{
		this.GetComponent<Animator>().Play("Liito", -1, 0.0f);
		Debug.Log("Liito animaatio alkaa");
	}

	public void Telemark()
	{
		this.GetComponent<Animator>().Play("Telemark", -1, 0.0f);
		Debug.Log("Telemark animaatio alkaa");
	}
}

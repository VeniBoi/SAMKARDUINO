﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuutioScript7 : MonoBehaviour {


	public int T;
	public int arvo;
	public Color startColor;
	public Color endColor;

	// Use this for initialization
	void Start () {
		//T = GameObject.Find("GameManager").GetComponent<ArduinoScript>().S0;

		
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<Renderer>().material.color = new Color32(T, 152, 0, 255);
		T = GameObject.Find("GameManager").GetComponent<ArduinoScript>().S7;
		arvo = T;
		if (T > 0)
		{
			//GetComponent<Renderer>().material.color = new Color32(T, 255 - T, 0, 255);
		}

		transform.position = new Vector3(-35.2f, -arvo/15, 27.7f);
	}
}
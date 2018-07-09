﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SittingPressureScript6 : MonoBehaviour
{

	int moveValue;
	Vector3 startposition;
	public Text text;
	// Use this for initialization
	void Start()
	{
		startposition = this.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		moveValue = BasicDemo.S6;

		if (moveValue > 0)
		{
			transform.position = new Vector3(0, -(startposition.y + moveValue) / 10, 0);
		}
		else
		{
			this.transform.position = startposition;
		}

		text.text = "Koordinaatti 6: " + transform.position.y;
	}
}

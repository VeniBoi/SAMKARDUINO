﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vari3 : MonoBehaviour
{
	public Color32 color;



	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{



		this.GetComponent<Image>().color = new Color32((byte)BasicDemo.mappedValue3, (byte)(255 - BasicDemo.mappedValue3), 0, 255);
	}
}
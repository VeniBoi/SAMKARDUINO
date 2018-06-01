using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vari6 : MonoBehaviour {
	public Color32 color;



	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{



		Renderer rend = GetComponent<Renderer>();



		color = new Color32((byte)BasicDemo.mappedValue6, (byte)(255 - BasicDemo.mappedValue6), 0, 255);
		rend.material.SetColor("_Color", color);
	}
}
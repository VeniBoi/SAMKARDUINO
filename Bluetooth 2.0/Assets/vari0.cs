using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vari0 : MonoBehaviour {

	public Color32 color;
	public Image image;



	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{



		//Renderer rend = GetComponent<Renderer>();


		this.GetComponent<Image>().color = new Color32((byte)BasicDemo.mappedValue0, (byte)(255 - BasicDemo.mappedValue0), 0, 255);
		//color = new Color32((byte)BasicDemo.mappedValue0, (byte)(255 - BasicDemo.mappedValue0), 0, 255);
		//rend.material.SetColor("_Color", color);
	}
}


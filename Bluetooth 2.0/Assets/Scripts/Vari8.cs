using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vari8 : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{



		this.GetComponent<Image>().color = new Color32((byte)BasicDemo.mappedValue8, (byte)(255 - BasicDemo.mappedValue8), 0, 255);
	}
}

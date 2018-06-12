using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class väriMuutos : MonoBehaviour {

	public Color32 color;

	public GameObject kuutio0;
	public GameObject kuutio1;
	public GameObject kuutio2;
	public GameObject kuutio3;
	public GameObject kuutio4;
	public GameObject kuutio5;
	public GameObject kuutio6;
	public GameObject kuutio7;
	public GameObject kuutio8;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



		Renderer rend = GetComponent<Renderer>();

		

		color = new Color32(255, (byte)BasicDemo.mappedValue0, 128, 255);
		rend.material.SetColor("_Color", color);
	}
}

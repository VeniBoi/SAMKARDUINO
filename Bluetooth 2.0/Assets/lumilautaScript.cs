using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lumilautaScript : MonoBehaviour {

	static public float pisteetLumilauta;
	public Text pisteetLumilautaText;
	public GameObject standupPanel;


	// Use this for initialization
	void Start()
	{
		pisteetLumilauta = 0;
		pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F2");

	}


		// Update is called once per frame
		void Update ()
	{
		pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F2");

		if (kameraScript.seuraavatasoPainettu == true && BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			standupPanel.SetActive(false);
		}
	}
}

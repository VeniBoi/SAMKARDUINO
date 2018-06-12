using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lumilautaScript : MonoBehaviour {

	static public float pisteetLumilauta;
	public Text pisteetLumilautaText;
	public GameObject standupPanel;
	public Rigidbody rb;


	// Use this for initialization
	void Start()
	{
		pisteetLumilauta = 0;
		pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F2");
		rb.GetComponent<Rigidbody>();
		GetComponent<Rigidbody>().isKinematic = true;
	}


		// Update is called once per frame
		void Update ()
	{
		pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F2");

		if(kameraScript.seuraavatasoPainettu == true)
		{
			GetComponent<Rigidbody>().isKinematic = false;
		}

		if (kameraScript.seuraavatasoPainettu == true && BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			standupPanel.SetActive(false);
		}

		//LIIKUTUS OIKEALLE! ----------------------VVVVV----------------------

		if (BasicDemo.S7 < 50 && BasicDemo.S1 < 50 && BasicDemo.S8 < 50)
		{
			rb.AddForce(Vector3.right * 15);
		}

		//LIIKUTUS VASEMMALLE! ----------------------VVVVV----------------------

		if (BasicDemo.S6 < 50 && BasicDemo.S3 < 50 && BasicDemo.S5 < 50)
		{
			rb.AddForce(-Vector3.right * 15);
		}

	}
}

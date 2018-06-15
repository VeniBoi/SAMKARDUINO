﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lumilautaScript : MonoBehaviour {

	static public float pisteetLumilauta;
	public Text pisteetLumilautaText;
	public Text totalPoints;
	public GameObject standupPanel;
	public GameObject lumisotaPanel;
	public GameObject lumilautaPisteetPaneeli;
	public Rigidbody rb;
	public float coordinateF;
	public bool pelikaynnissa;

	//public Quaternion vasen = Quaternion.Euler(20.85f, 0, 20);
	//public Quaternion oikea = Quaternion.Euler(20.85f, 0, -20);
	//public Quaternion ylös = Quaternion.Euler(20.85f, 0, 0);

	public bool lumilautaloppu;
	//bool randomBool;


	// Use this for initialization
	void Start()
	{
		pelikaynnissa = false;
		lumisotaPanel.SetActive(false);
		lumilautaloppu = true;
		coordinateF = rb.position.z;
		pisteetLumilauta = 0;
		//pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F2");
		rb.GetComponent<Rigidbody>();
		GetComponent<Rigidbody>().isKinematic = true;
	}


	// Update is called once per frame
	void Update()
	{

		


		pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F0");
		

		if (kameraScript.seuraavatasoPainettu == true)
		{

			
		}


		if (kameraScript.seuraavatasoPainettu == true && BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			standupPanel.SetActive(false);
			GetComponent<Rigidbody>().isKinematic = false;
			pelikaynnissa = true;
		}

		if (pelikaynnissa == true && Input.GetKey("a"))//(BasicDemo.S3 < 80)
		{
			//rb.AddForce(-Vector3.right * 30 * Time.deltaTime);
			transform.position += new Vector3(-0.5f, 0, 0);
			transform.rotation = Quaternion.Euler(20.85f, 0, 20);
		}
		else
		{
			transform.rotation = Quaternion.Euler(20.85f, 0, 0);
		}

		if (pelikaynnissa == true &&Input.GetKey("d"))//(BasicDemo.S1 < 80)
		{
			//rb.AddForce(Vector3.right * 30 * Time.deltaTime);
			transform.position += new Vector3(0.5f, 0, 0);
			transform.rotation = Quaternion.Euler(20.85f, 0, -20);
		}
		
		
		/*
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
		*/	
	}

	

	void OnTriggerExit(Collider other)
	{

		if (other.gameObject.CompareTag("lumilautaLoppuTrigger"))
		{
			Debug.Log("osumaa satan");
			pelikaynnissa = false;
			lumilautaloppu = false;
			lumilautaPisteetPaneeli.SetActive(false);
			StartCoroutine(seuraavataso());
			Debug.Log(pelikaynnissa);
			//randomBool = false;
		}

	
	}

	IEnumerator seuraavataso()
	{
		yield return new WaitForSeconds(2f);
		lumisotaPanel.SetActive(true);
		Debug.Log(pisteetLumilauta);
		totalPoints.text = "Total Points: " + (kameraScript.totalPoints + pisteetLumilauta).ToString("F2");
		kameraScript.totalPoints = kameraScript.totalPoints + pisteetLumilauta;
	}
}

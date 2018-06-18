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
	public Vector3 paikka;



	//public Quaternion vasen = Quaternion.Euler(20.85f, 0, 20);
	//public Quaternion oikea = Quaternion.Euler(20.85f, 0, -20);
	//public Quaternion ylös = Quaternion.Euler(20.85f, 0, 0);

	public bool lumilautaloppu;
	public bool kaantoBool;
	//bool randomBool;

	public void Restart()
	{
		GetComponent<Rigidbody>().isKinematic = true;
		//transform.eulerAngles = new Vector3(16.548f, 0f, 0f);
		transform.position = paikka;
		pisteetLumilauta = 0;
		pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F0");
		rb.constraints = RigidbodyConstraints.None;
		rb.constraints = RigidbodyConstraints.FreezePositionX;
		rb.constraints = RigidbodyConstraints.FreezeRotationX;
		rb.constraints = RigidbodyConstraints.FreezeRotationY;
		pelikaynnissa = false;
		lumilautaloppu = true;
		kameraScript.seuraavatasoPainettu = false;


	}

	// Use this for initialization
	void Start()
	{
		paikka = this.transform.position;
		kaantoBool = false;
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

		if (BasicDemo.S3 < 80) //(pelikaynnissa == true && Input.GetKey("a")) 
		{
			//rb.AddForce(-Vector3.right * 30 * Time.deltaTime);
			//transform.position += new Vector3(-0.4f, 0, 0);
			transform.rotation = Quaternion.Euler(20.85f, 0, 20);

			int erotusVasen = BasicDemo.S1 - BasicDemo.S3;
			transform.position += new Vector3(-erotusVasen / 100f, 0, 0);
		}
		else
		{
			transform.rotation = Quaternion.Euler(20.85f, 0, 0);
		}

		if (BasicDemo.S1 < 80) //(pelikaynnissa == true &&Input.GetKey("d"))
		{
			//rb.AddForce(Vector3.right * 30 * Time.deltaTime);
			//transform.position += new Vector3(0.4f, 0, 0);
			transform.rotation = Quaternion.Euler(20.85f, 0, -20);

			int erotusOikea = BasicDemo.S3 - BasicDemo.S1;
			transform.position += new Vector3(erotusOikea / 100f, 0, 0);
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

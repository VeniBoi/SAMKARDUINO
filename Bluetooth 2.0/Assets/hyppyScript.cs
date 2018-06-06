using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hyppyScript : MonoBehaviour
{

	public float thrust;
	public Rigidbody rb;
	public float coordinateF;
	public GameObject paneeli;
	public float hyppyPisteet;
	public Text hyppyPisteetText;
	public GameObject restartButton;

	static public bool pelaajaValmis;
	public bool onHypätty;
	public bool onLaskeuduttu;
	static public bool restartBool;


	// Use this for initialization
	void Start()
	{
		restartButton.SetActive(false);
		hyppyPisteetText.enabled = false;
		onLaskeuduttu = true;
		hyppyPisteet = 0;
		onHypätty = false;
		pelaajaValmis = false;
		rb.GetComponent<Rigidbody>();
		GetComponent<Rigidbody>().isKinematic = true;       //Pysäyttää kuution(pelaajan)
	}

	// Update is called once per frame
	void Update()
	{
		thrust = rb.position.z - coordinateF;

		if (kameraScript.playPainettu == true && kameraScript.pelaajaPaikalla == true && BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			GetComponent<Rigidbody>().isKinematic = false;
			paneeli.SetActive(false);
			hyppyPisteetText.enabled = true;
			restartBool = true;
			restartButton.SetActive(true);

		}

		

		if (BasicDemo.S0 < 20 && BasicDemo.S1 < 20 && BasicDemo.S2 < 20 && BasicDemo.S3 < 20 && BasicDemo.S4 < 20 && BasicDemo.S5 < 20 && BasicDemo.S6 < 20 && BasicDemo.S7 < 20 && BasicDemo.S8 < 20 && onHypätty == false)
		{

			rb.AddForce(Vector3.forward * thrust * 17);
			rb.AddForce(Vector3.up * thrust * 17);
			Debug.Log("Työnnön voima: " + thrust);
			onHypätty = true;

		}

		if(onLaskeuduttu == false)
		{
			hyppyPisteet = rb.position.z - coordinateF;
			hyppyPisteetText.text = "Pisteet: " + hyppyPisteet.ToString("F2");
			//paneeli.SetActive(true);
			restartBool = false;
		}

		


	}

	public void Restart()
	{
		GetComponent<Rigidbody>().isKinematic = true;
		transform.eulerAngles = new Vector3(0, 90, 0);
		this.transform.position = new Vector3(-167.7526f, 80.16851f, 40f);
		paneeli.SetActive(true);
		restartButton.SetActive(false);
		hyppyPisteet = 0;
		hyppyPisteetText.text = "Pisteet: " + hyppyPisteet.ToString("F2");

	}
	

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("hyppyalue"))
		{
			//Laske koordinaatin mukaan addforce voima, jolla "hypätään".
			coordinateF = rb.position.z;
			Debug.Log("Ensimmäinen koordinaatti: " + coordinateF);
			onLaskeuduttu = false;
			onHypätty = false;
		}

		if (other.gameObject.CompareTag("laskualue"))
		{
			Debug.Log("On laskeuduttu!");
			onLaskeuduttu = true;
			
		}

		if (other.gameObject.CompareTag("hyppyalueloppu"))
		{
			Debug.Log("Enää ei voi hypätä.");
			onLaskeuduttu = true;
		}
	}
}

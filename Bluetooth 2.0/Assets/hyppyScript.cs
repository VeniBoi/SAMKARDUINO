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
	public bool hyppyalueLoppu;
	public bool animaatioLasku;
	public bool hyppyAlueella;


	// Use this for initialization
	void Start()
	{
		hyppyAlueella = false;
		animaatioLasku = false;
		hyppyalueLoppu = true;
		restartButton.SetActive(false);
		hyppyPisteetText.enabled = false;
		onLaskeuduttu = true;
		hyppyPisteet = 0;
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
			animaatioLasku = true;
			AnimaatioScript.animaatio1 = true;
			//GameObject.Find("Tapio").GetComponent<AnimaatioScript>().Lasku();

		}

		

		if(BasicDemo.S0 < 30 && BasicDemo.S1 < 30 && BasicDemo.S2 < 30 && BasicDemo.S3 < 30 && BasicDemo.S4 < 30 && BasicDemo.S5 < 30 && BasicDemo.S6 < 30 && BasicDemo.S7 < 30 && BasicDemo.S8 < 30 && hyppyAlueella == true)//(onHypätty == false && Input.GetKeyDown(KeyCode.Space))// || 
		{
			
				//rb.AddForce(Vector3.forward * thrust * 2);
				rb.AddForce(Vector3.up * thrust * 13);
				Debug.Log("Työnnön voima: " + thrust);
				onHypätty = true;
				hyppyAlueella = false;
				AnimaatioScript.animaatio3 = true;

		}
		

		if(onLaskeuduttu == false)
		{
			hyppyPisteet = rb.position.z - coordinateF;
			hyppyPisteetText.text = "Pisteet: " + hyppyPisteet.ToString("F2");
			//paneeli.SetActive(true);
			restartBool = false;
		}

		/*if(Input.GetKeyDown(KeyCode.Space) && hyppyalueLoppu == false && onHypätty == false)
		{
			rb.AddForce(Vector3.forward * thrust * 17);
			rb.AddForce(Vector3.up * thrust * 17);
			Debug.Log("Työnnön voima: " + thrust);
			Debug.Log(hyppyalueLoppu);
			Debug.Log("Toimiiko?");
		}
		*/

	}

	public void Restart()
	{
		GetComponent<Rigidbody>().isKinematic = true;
		transform.eulerAngles = new Vector3(0, 90, 0);
		this.transform.position = new Vector3(-167.7526f, 112.129f, -27.568f);
		paneeli.SetActive(true);
		restartButton.SetActive(false);
		hyppyPisteet = 0;
		hyppyPisteetText.text = "Pisteet: " + hyppyPisteet.ToString("F2");
		AnimaatioScript.animaatio1 = false;
		AnimaatioScript.animaatio2 = true;

	}
	

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("hyppyalue"))
		{
			//Laske koordinaatin mukaan addforce voima, jolla "hypätään".
			coordinateF = rb.position.z;
			Debug.Log("Ensimmäinen koordinaatti: " + coordinateF);
			onLaskeuduttu = false;
			hyppyalueLoppu = false;
			hyppyAlueella = true;
		}

		if (other.gameObject.CompareTag("laskualue"))
		{
			Debug.Log("On laskeuduttu!");
			onLaskeuduttu = true;
			
		}

		if (other.gameObject.CompareTag("hyppyalueloppu"))
		{
			Debug.Log("On hypätty");
			onHypätty = true;
		}
	}
}

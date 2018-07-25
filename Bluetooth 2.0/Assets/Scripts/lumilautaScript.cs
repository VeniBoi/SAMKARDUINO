using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lumilautaScript : MonoBehaviour {

	static public int pisteetLumilauta;
	public Text pisteetLumilautaText;
	public Text totalPoints;
	public GameObject standupPanel;
	public GameObject lumisotaPanel;
	public GameObject lumilautaPisteetPaneeli, Skip2;
	public Rigidbody rb;
	public float coordinateF;
	public bool pelikaynnissa;
	public bool staticLiikutus;
	public bool staticLiikutus2;
	public Vector3 paikka;

	public float vastaVoima;

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
		if(LanguageScript.Lang == 1)
		{
			pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F0");
		}
		else
		{
			pisteetLumilautaText.text = "Score: " + pisteetLumilauta.ToString("F0");
		}
		
		rb.constraints = RigidbodyConstraints.None;
		rb.constraints = RigidbodyConstraints.FreezePositionX;
		rb.constraints = RigidbodyConstraints.FreezeRotationX;
		rb.constraints = RigidbodyConstraints.FreezeRotationY;
		pelikaynnissa = false;
		lumilautaloppu = true;
		kameraScript.seuraavatasoPainettu = false;


	}

	public void speedChange(float newSpeed)
	{
		vastaVoima = newSpeed;
	}

	// Use this for initialization
	void Start()
	{
		vastaVoima = 0.3f;
		paikka = this.transform.position;
		kaantoBool = false;
		pelikaynnissa = false;
		lumisotaPanel.SetActive(false);
		lumilautaloppu = true;
		coordinateF = rb.position.z;
		pisteetLumilauta = 0;
		//pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F2");
		rb.GetComponent<Rigidbody>();
		GetComponent<Rigidbody>().isKinematic = true; // -----------------------------------------------------------------------------
		staticLiikutus = false;
		staticLiikutus2 = true;
	}


	// Update is called once per frame
	void Update()
	{

		

		if(LanguageScript.Lang == 1)
		{
			pisteetLumilautaText.text = "Pisteet: " + pisteetLumilauta.ToString("F0");
		}
		else
		{
			pisteetLumilautaText.text = "Score: " + pisteetLumilauta.ToString("F0");
		}
		
		
		if (staticLiikutus)
		{
			rb.AddForce(-transform.forward * vastaVoima, ForceMode.Force);

			if (staticLiikutus2)
			{
				rb.AddForce(transform.forward * 1500 * Time.deltaTime, ForceMode.Impulse);
				staticLiikutus2 = false;
			}
		}
		


		if (kameraScript.seuraavatasoPainettu == true && BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			standupPanel.SetActive(false);
			GetComponent<Rigidbody>().isKinematic = false;//----------------------------------------------------------------------------
			pelikaynnissa = true;
			staticLiikutus = true;
			
		}

		if (BasicDemo.S3 < 80 && pelikaynnissa == true) /*(pelikaynnissa == true && Input.GetKey("a"))*/ 
		{
			
			//transform.position += new Vector3(-0.4f, 0, 0);
			transform.rotation = Quaternion.Euler(20.85f, 0, 20);

			int erotusVasen = BasicDemo.S1 - BasicDemo.S3;
			transform.position += new Vector3(-erotusVasen / 100f, 0, 0);
			//rb.AddForce(-Vector3.right * -erotusVasen / 10f * Time.deltaTime);
		}
		else
		{
			transform.rotation = Quaternion.Euler(20.85f, 0, 0);
		}

		if (BasicDemo.S1 < 80 && pelikaynnissa == true) /*(pelikaynnissa == true &&Input.GetKey("d"))*/
		{
			//rb.AddForce(Vector3.right * 30 * Time.deltaTime);
			//transform.position += new Vector3(0.4f, 0, 0);
			transform.rotation = Quaternion.Euler(20.85f, 0, -20);

			int erotusOikea = BasicDemo.S3 - BasicDemo.S1;
			transform.position += new Vector3(erotusOikea / 100f, 0, 0);
			//rb.AddForce(Vector3.right * erotusOikea / 10f * Time.deltaTime);
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
	
	public void tasoSkip()
	{
		kameraScript.seuraavatasoPainettu = false;
		pelikaynnissa = false;
		lumilautaloppu = false;
		lumilautaPisteetPaneeli.SetActive(false);
		lumisotaPanel.SetActive(true);
		Debug.Log(pisteetLumilauta);
		if (LanguageScript.Lang == 1)
		{
			totalPoints.text = "Kokonaispisteet: " + (kameraScript.totalPoints + pisteetLumilauta).ToString("F0");
		}
		else
		{
			totalPoints.text = "Total Points: " + (kameraScript.totalPoints + pisteetLumilauta).ToString("F0");
		}

		kameraScript.totalPoints = kameraScript.totalPoints + pisteetLumilauta;
	}

	void OnTriggerExit(Collider other)
	{

		if (other.gameObject.CompareTag("lumilautaLoppuTrigger"))
		{
			Skip2.SetActive(false);
			pelikaynnissa = false;
			lumilautaloppu = false;
			lumilautaPisteetPaneeli.SetActive(false);
			StartCoroutine(seuraavataso());
			Debug.Log(pelikaynnissa);
			//randomBool = false;
			
		}

	
	}

	public IEnumerator seuraavataso()
	{
		yield return new WaitForSeconds(1f);
		lumisotaPanel.SetActive(true);
		Debug.Log(pisteetLumilauta);
		if(LanguageScript.Lang == 1)
		{
			totalPoints.text = "Kokonaispisteet: " + (kameraScript.totalPoints + pisteetLumilauta).ToString("F0");
		}
		else
		{
			totalPoints.text = "Total Points: " + (kameraScript.totalPoints + pisteetLumilauta).ToString("F0");
		}
		
		kameraScript.totalPoints = kameraScript.totalPoints + pisteetLumilauta;
	}
}

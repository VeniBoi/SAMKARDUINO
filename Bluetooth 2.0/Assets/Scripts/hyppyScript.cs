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
	static public float hyppyPisteet;
	public Text hyppyPisteetText;
	public GameObject restartButton;
	public GameObject partikkelit;
	public GameObject seuraavaTasoPanel;
	public GameObject panel2;
	public Text totalpoints;


	static public bool pelaajaValmis;
	public bool onHypätty;
	public bool onLaskeuduttu;
	static public bool restartBool;
	public bool hyppyalueLoppu;
	public bool animaatioLasku;
	public bool hyppyAlueella;
	public bool kääntöKohtaYlitetty;
	public bool animaatioBool;
	

	public Transform player;

	public Vector3 velocity;
	public Vector3 targetVelocity;

	public Vector3 paikka;
	public Quaternion paikka2;

	public float hidastus;

	// Use this for initialization
	void Start()
	{
		targetVelocity = new Vector3(0.0f, 0.0f, 0.0f);
		paikka = this.transform.position;
		paikka2 = this.transform.rotation;
		animaatioBool = true;
		kääntöKohtaYlitetty = false;
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
		hidastus = 0.15f;
	}

	// Update is called once per frame
	void Update()
	{
		
		RaycastHit hit;

		Ray downray = new Ray(transform.position, -Vector3.up);
		Debug.DrawRay(transform.position, -Vector3.up * 20, Color.red);

		if (Physics.Raycast(downray, out hit, 10) && kääntöKohtaYlitetty == true && animaatioBool == true)
		{
			Debug.Log("Nyt lähtee laskeutumisanimaatio käyntiin");
			AnimaatioScript.animaatio5 = true;
			animaatioBool = false;
		}




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



		if (Input.GetKeyDown("space") && hyppyAlueella == true) /*if(BasicDemo.S0 < 30 && BasicDemo.S1 < 30 && BasicDemo.S2 < 30 && BasicDemo.S3 < 30 && BasicDemo.S4 < 30 && BasicDemo.S5 < 30 && BasicDemo.S6 < 30 && BasicDemo.S7 < 30 && BasicDemo.S8 < 30 && hyppyAlueella == true)*/ 
		{

			//rb.AddForce(Vector3.forward * thrust * 2);
				Time.timeScale = 1f;
				rb.AddForce(Vector3.up * thrust * 15);
				Debug.Log("Työnnön voima: " + thrust);
				onHypätty = true;
				AnimaatioScript.animaatio3 = true;
				rb.constraints = RigidbodyConstraints.FreezeRotationZ;
				hyppyAlueella = false;


		}
		

		if(onLaskeuduttu == false)
		{
			hyppyPisteet = rb.position.z - coordinateF;
			hyppyPisteetText.text = "Score: " + hyppyPisteet.ToString("F0");
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
		kameraScript.playPainettu = false;
		GetComponent<Rigidbody>().isKinematic = true;
		rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		
		transform.position = paikka;
		transform.rotation = paikka2;
		//transform.eulerAngles = new Vector3(0, 89, 0);
		//this.transform.position = new Vector3(-167.7526f, 112.129f, -27.568f);
		paneeli.SetActive(true);
		restartButton.SetActive(false);
		hyppyPisteet = 0;
		hyppyPisteetText.text = "Pisteet: " + hyppyPisteet.ToString("F0");
		AnimaatioScript.animaatio1 = false;
		AnimaatioScript.animaatio2 = true;
		AnimaatioScript.animaatio3 = false;
		AnimaatioScript.animaatio4 = true;
		AnimaatioScript.animaatio5 = false;
		AnimaatioScript.animaatio6 = true;
		AnimaatioScript.animaatio7 = false;
		AnimaatioScript.animaatio8 = true;
		kääntöKohtaYlitetty = false;
		animaatioBool = true;
		GameObject.Find("lasku").GetComponent<materiaaliScript>().materiaaliVaihtoTakas();

	}

	
	

	IEnumerator partikkelitNum()
	{
		yield return new WaitForSeconds(0.3f);
		partikkelit.SetActive(true);
		yield return new WaitForSeconds(2.3f);
		partikkelit.SetActive(false);
	}

	IEnumerator seuraavataso()
	{
		yield return new WaitForSeconds(2.5f);
		seuraavaTasoPanel.SetActive(true);
		panel2.SetActive(false);
		//_------------------------------------------------------------------------------------------------------------------------------
	}

	public void adjustTime(float uusihidastus)
	{
		hidastus = uusihidastus;
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("hyppyalue"))
		{
			//Laske koordinaatin mukaan addforce voima, jolla "hypätään".
			Time.timeScale = hidastus;
			coordinateF = rb.position.z;
			onLaskeuduttu = false;
			hyppyalueLoppu = false;
			hyppyAlueella = true;
			Debug.Log("Hidastus nopeus: " + hidastus);
			Debug.Log(Time.timeScale);
		}

		if (other.gameObject.CompareTag("laskualue"))
		{
			Debug.Log("On laskeuduttu!");
			onLaskeuduttu = true;
			//rb.constraints = RigidbodyConstraints.None;
			rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX; 
		}

		if (other.gameObject.CompareTag("hyppyalueloppu"))
		{
			//GetComponent<Rigidbody>().AddTorque(-transform.forward * 1f * 1f);
			Debug.Log("On hypätty");
			onHypätty = true;
			Time.timeScale = 1f;
			
		}

		if (other.gameObject.CompareTag("rotateCollider"))
		{
			Debug.Log("Kääntyminen alkaa");
			rb.constraints = RigidbodyConstraints.FreezeRotationY;
			kääntöKohtaYlitetty = true;
		}

		if (other.gameObject.CompareTag("stoppiTrigger"))
		{
			Debug.Log("Pysähdytään!");
			AnimaatioScript.animaatio7 = true;
			

			rb.AddForce(-Vector3.forward * 5 * 5);
			//rb.constraints = RigidbodyConstraints.FreezeRotationZ;
			rb.constraints = RigidbodyConstraints.FreezePositionX;
			GameObject.Find("lasku").GetComponent<materiaaliScript>().materiaaliVaihto();
			StartCoroutine(partikkelitNum());

		}

		if (other.gameObject.CompareTag("stoppiTriggerForce"))
		{
			StartCoroutine(seuraavataso());
			kameraScript.totalPoints = kameraScript.totalPoints + hyppyPisteet;
			totalpoints.text = "Total Points: " + kameraScript.totalPoints.ToString("F0");
			Debug.Log(kameraScript.totalPoints);
			rb.AddForce(-Vector3.forward * 30 * 30);
			
		}

		if (other.gameObject.CompareTag("stoppiTriggerForce1"))
		{
			
			rb.AddForce(-Vector3.forward * 10 * 10);
		}

		
	}
}

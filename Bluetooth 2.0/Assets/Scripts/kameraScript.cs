using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kameraScript : MonoBehaviour
{
	
	public float transitionDuration = 2.5f;
	public Transform target;
	public GameObject laskuri;
	public GameObject playPaneeli;
	public GameObject restartButton;
	public GameObject seuraavatasoPaneeli;
	public GameObject lumilautaPisteet;
	public GameObject menuNappi;
	public GameObject searchMenu;


	public Transform player;
	public Transform playerLumi;
	public Vector3 offset;
	public Vector3 stoppiOffset;

	static public bool playPainettu;
	public bool kameraKohdalla;
	static public bool pelaajaPaikalla;
	static public bool lumilautaKohdalla;
	static public bool seuraavatasoPainettu = false;

	


	// Use this for initialization
	void Start()
	{
		//Paneeli2.GetComponent<Image>().enabled =  false;
		lumilautaKohdalla = false;
		kameraKohdalla = false;
		playPainettu = false;
		laskuri.SetActive(false);
		
	}

	// Update is called once per frame
	void Update()
	{

		if (kameraKohdalla == true)
		{
			transform.position = player.position + offset;
		}

		if (lumilautaKohdalla == true)
		{
			Quaternion.Euler(15, 0, 0);
			transform.position = playerLumi.position + offset;
		}

		/*if (BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			hyppyScript.pelaajaValmis = true;
			//Paneeli2.SetActive(false);
		}

		*/
		/*if (hyppyScript.pelaajaValmis == true)
		{
			
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
		}
		*/
	}

	public void sammutus()
	{
		Application.Quit();
	}


	public void liikutus()
	{
		//Paneeli2.GetComponent<Image>().enabled = false;
		hyppyScript.restartBool = true;
		playPainettu = true;
		
		if (playPainettu ==  true)
		{
			StartCoroutine(Transition());
			pelaajaPaikalla = true;
		}
		
	}

	public void paneelit()
	{
		playPaneeli.SetActive(false);
		restartButton.SetActive(true);
		menuNappi.SetActive(true);
	}


	public void kameraKaannos()
	{
		StartCoroutine(TransitionLaskeuduttu());
	}
	
	public void lumilautaTasoon()
	{
		seuraavatasoPaneeli.SetActive(false);
		lumilautaKohdalla = true;
		kameraKohdalla = false;
		seuraavatasoPainettu = true;
		lumilautaPisteet.SetActive(true);
	}

	public void takaisinMenuun()
	{
		transform.position = new Vector3(990, 817, 35);
		menuNappi.SetActive(true);
		playPaneeli.SetActive(true);
		searchMenu.SetActive(true);
	}

	IEnumerator Transition()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);


			transform.position = Vector3.Lerp(startingPos, target.position, t);

			kameraKohdalla = true;
			
			
			yield return 0;
		}
	}

	IEnumerator TransitionLaskeuduttu()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		Quaternion startingRot = transform.rotation;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);

			transform.position = player.position + offset;

			transform.position = Vector3.Lerp(startingPos, player.position + stoppiOffset, t);
			transform.rotation = Quaternion.Lerp(startingRot, player.rotation , 0);
			

			yield return 0;
		}
	}

	IEnumerator valmis()
	{

		yield return new WaitForSeconds(0.7f);

		laskuri.SetActive(true);
		
		pelaajaScript.peliAlkanut = true;

	}

	IEnumerator valmisAnalyysi()
	{
		yield return new WaitForSeconds(0.7f);
		
		

	}
}

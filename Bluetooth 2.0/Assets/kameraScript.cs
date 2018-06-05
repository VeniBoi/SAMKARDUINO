using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kameraScript : MonoBehaviour
{
	
	public float transitionDuration = 2.5f;
	public Transform target;
	public Transform targetAnalyysi;
	public Transform targetBack;
	public GameObject canvas;
	public GameObject laskuri;
	public GameObject Paneeli;
	public GameObject pelaaja;
	public GameObject Paneeli2;


	public Transform player;
	public Vector3 offset;

	public bool playPainettu;
	public bool kameraKohdalla;

	


	// Use this for initialization
	void Start()
	{
		Paneeli2.SetActive(false);
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

		if (BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			hyppyScript.pelaajaValmis = true;
		}

			if (hyppyScript.pelaajaValmis == true)
		{
			Paneeli2.SetActive(false);
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	public void sammutus()
	{
		Application.Quit();
	}


	public void liikutus()
	{
		playPainettu = true;
		Paneeli.SetActive(false);
		if (playPainettu ==  true)
		{
			StartCoroutine(Transition());

		}
	}


	/*public void liikutusAnalyysi()
	{

		if (BasicDemo.onLiitytty ==  true)
		{
			canvas.SetActive(false);
			input.SetActive(false);
			backbtn.SetActive(true);
			connectFirst.SetActive(false);
			StartCoroutine(TransitionAnalyysi());
		}
		else
		{
			connectFirst.SetActive(true);
		}
	}

	*/

	IEnumerator Transition()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);


			transform.position = Vector3.Lerp(startingPos, target.position, t);

			kameraKohdalla = true;
			Paneeli2.SetActive(true);
			
			yield return 0;
		}
	}

	/*IEnumerator TransitionAnalyysi()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);


			transform.position = Vector3.Lerp(startingPos, targetAnalyysi.position, t);



			yield return 0;
		}
	}

	*/


	IEnumerator TransitionBack()
	{

		float t = 0.0f;
		Vector3 startingPos = transform.position;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);


			transform.position = Vector3.Lerp(startingPos, targetBack.position, t);



			yield return 0;
		}

		
		canvas.SetActive(true);
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

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
	public GameObject aika;
	public GameObject restart;
	public GameObject input;
	public GameObject backbtn;
	public GameObject connectFirst;
	
	


	// Use this for initialization
	void Start()
	{
		laskuri.SetActive(false);
		aika.SetActive(false);
		restart.SetActive(false);
		backbtn.SetActive(false);
		connectFirst.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void sammutus()
	{
		Application.Quit();
	}

	public void Back()
	{
		
		laskuri.SetActive(false);
		aika.SetActive(false);
		backbtn.SetActive(false);
		restart.SetActive(false);
		StartCoroutine(TransitionBack());
		pelaajaScript.palkki1.GetComponent<poleColor>().varitakas();
		pelaajaScript.palkki2.GetComponent<poleColor>().varitakas();
		pelaajaScript.palkki3.GetComponent<poleColor>().varitakas();
		pelaajaScript.palkki4.GetComponent<poleColor>().varitakas();

	}

	public void liikutus()
	{
		if (BasicDemo.onLiitytty == true) {
			canvas.SetActive(false);
			StartCoroutine(valmis());
			backbtn.SetActive(true);
			StartCoroutine(Transition());
			pelaajaScript.osumat = 0;
			pelaajaScript.gameTimer = 0;
			connectFirst.SetActive(false);
		}
		else
		{
			connectFirst.SetActive(true);
		}
	}


	public void liikutusAnalyysi()
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

	IEnumerator Transition()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);


			transform.position = Vector3.Lerp(startingPos, target.position, t);


			
			yield return 0;
		}
	}

	IEnumerator TransitionAnalyysi()
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
		aika.SetActive(true);
		laskuri.SetActive(true);
		restart.SetActive(true);
		pelaajaScript.peliAlkanut = true;

	}

	IEnumerator valmisAnalyysi()
	{
		yield return new WaitForSeconds(0.7f);
		
		

	}
}

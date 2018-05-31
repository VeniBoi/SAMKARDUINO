using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kameraScript : MonoBehaviour
{
	
	public float transitionDuration = 2.5f;
	public Transform target;
	public GameObject canvas;
	public GameObject laskuri;
	public GameObject aika;


	// Use this for initialization
	void Start()
	{
		laskuri.SetActive(false);
		aika.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void liikutus()
	{
		canvas.SetActive(false);
		

		StartCoroutine(valmis());
		StartCoroutine(Transition());
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

	IEnumerator valmis()
	{
		yield return new WaitForSeconds(0.7f);
		aika.SetActive(true);
		laskuri.SetActive(true);
		pelaajaScript.peliAlkanut = true;

	}
}

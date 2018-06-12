using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageChangeScript : MonoBehaviour {

	public bool connectImage1;
	public bool connectImage2;
	public Sprite kuva1;
	public Sprite kuva2;
	Image m_Image;


	// Use this for initialization
	void Start () {

		InvokeRepeating("kuvanVaihto", 0.1f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		/*
		if(connectImage1 == true)
		{
			//
			StartCoroutine(kuva1());
		}

		if (connectImage2 == true)
		{
			StartCoroutine(kuva2());
		}
		*/
	}

	void kuvanVaihto()
	{
		StartCoroutine(kuva134());
	}

	IEnumerator kuva134()
	{
		GetComponent<Image>().sprite = kuva1;
		yield return new  WaitForSeconds(1f);
		GetComponent<Image>().sprite = kuva2;

	}

	

}

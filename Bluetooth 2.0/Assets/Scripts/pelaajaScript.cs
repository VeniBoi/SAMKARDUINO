using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pelaajaScript : MonoBehaviour {

	public Rigidbody rb;
	public GameObject restartNappi;
	static public GameObject palkki1;
	static public GameObject palkki2;
	static public GameObject palkki3;
	static public GameObject palkki4;
	public Text laskuri;
	public Text palkkiLaskuri;
	

	static public float gameTimer = 0f;
	static public int osumat;
	public int moveSpeed;
	public int speed;

	
	static public bool peliAlkanut = false;
	

	// Use this for initialization
	void Start () {
		osumat = 0;
		palkkiLaskuri.text = "Palkkeihin osuttu: " + osumat;
		peliAlkanut = false;
		speed = 5;
	}
	
	// Update is called once per frame
	void Update () {

		
		


		if (osumat < 4 && peliAlkanut)
		{
			gameTimer += Time.deltaTime;
			//int seconds = (int)(gameTimer % 60);
		}
		laskuri.text = "aika: " + gameTimer.ToString("f0") + "s";

		//Vasemmalle
		if (BasicDemo.S3 <= 85 && BasicDemo.S5 <= 85 && peliAlkanut)						
		{
			transform.Translate(-Vector3.right * Time.deltaTime * speed);
		}

		//Oikealle
		if (BasicDemo.S8 <= 85 && BasicDemo.S1 <= 85 && peliAlkanut)
		{
			transform.Translate(Vector3.right * Time.deltaTime * speed);
		}

		//Eteenpäin
		if (BasicDemo.S8 <= 60 && BasicDemo.S4 <= 60 && BasicDemo.S5 <= 60 && peliAlkanut)
		{
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}

		//Taaksepäin
		if (BasicDemo.S6 <= 80 && BasicDemo.S7 <= 80 && peliAlkanut)
		{
			transform.Translate(-Vector3.forward * Time.deltaTime * speed);
		}
		


		/*
		if (BasicDemo.S3 <= 20 && peliAlkanut) 
		{
			rb.AddForce(-700 * Time.deltaTime, 0, 0);
		}

		if (BasicDemo.S1 <= 20 && peliAlkanut)
		{
			rb.AddForce(700 * Time.deltaTime, 0, 0);
		}

		if (BasicDemo.S4 <= 20 && peliAlkanut)
		{
			rb.AddForce(0, 0, 700 * Time.deltaTime);
		}

		if (BasicDemo.S6 <= 20 && BasicDemo.S7 <= 20 && peliAlkanut)
		{
			rb.AddForce(0, 0, -700 * Time.deltaTime);
		}
		*/

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("pole"))
		{
			osumat++;
			setText();
		}
	}

	public void setText()
	{
		palkkiLaskuri.text = "Palkkeihin osuttu: " + osumat;
	}

	public void restart()
	{
		gameTimer = 0;
		osumat = 0;
		this.transform.position = new Vector3(-19.5f, 0.0f, 0.0f);
		StartCoroutine(odotus());
		Time.timeScale = 1f;
	}
	
	IEnumerator odotus()
	{
		Time.timeScale = 0f;
		yield return new WaitForSeconds(1);
		
	}
}

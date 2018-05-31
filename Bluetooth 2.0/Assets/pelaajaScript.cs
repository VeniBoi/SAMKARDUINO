using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pelaajaScript : MonoBehaviour {

	public GameObject palkki1;
	public GameObject palkki2;
	public GameObject palkki3;
	public GameObject palkki4;
	public Text laskuri;
	public Text palkkiLaskuri;

	public float gameTimer = 0f;
	public int osumat;

	static public bool peliAlkanut = false;
	

	// Use this for initialization
	void Start () {
		osumat = 0;
		palkkiLaskuri.text = "Palkkeihin osuttu: " + osumat;
	}
	
	// Update is called once per frame
	void Update () {
		if (osumat < 4 && peliAlkanut)
		{
			gameTimer += Time.deltaTime;
			int seconds = (int)(gameTimer % 60);
		}
		laskuri.text = "aika: " + gameTimer.ToString("f0") + "s";


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

	
}

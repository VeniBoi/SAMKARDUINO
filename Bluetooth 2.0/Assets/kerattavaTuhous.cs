using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kerattavaTuhous : MonoBehaviour {

	// Use this for initialization
	void Start () {
		lumilautaScript.pisteetLumilauta = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("pelaaja2"))
		{
			lumilautaScript.pisteetLumilauta = lumilautaScript.pisteetLumilauta + 50f;
			Debug.Log("No meneekö se rikki?");
			Destroy(this.gameObject);
		}

		
	}
}

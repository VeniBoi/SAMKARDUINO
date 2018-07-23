using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipScript : MonoBehaviour {

	public GameObject makihyppaaja, lumilautailija, skipNappi1, skipNappi2;

	public void Skippaa1()
	{
		kameraScript.pelaajaPaikalla = false;
		makihyppaaja.GetComponent<Rigidbody>().isKinematic = true;
		makihyppaaja.GetComponent<hyppyScript>().panel2.SetActive(false);
		this.GetComponent<kameraScript>().lumilautaTasoon();
		skipNappi1.SetActive(false);
		skipNappi2.SetActive(true);
	}

	public void Skippaa2()
	{
		lumilautailija.GetComponent<lumilautaScript>().tasoSkip();
		this.GetComponent<kameraScript>().lumisotaTasoon();
		lumilautailija.GetComponent<Rigidbody>().isKinematic = true;
		skipNappi2.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

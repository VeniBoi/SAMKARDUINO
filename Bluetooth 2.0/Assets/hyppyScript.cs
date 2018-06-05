using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyppyScript : MonoBehaviour
{

	public float thrust;
	public Rigidbody rb;
	public float coordinateF;

	static public bool pelaajaValmis;


	// Use this for initialization
	void Start()
	{

		pelaajaValmis = false;
		rb.GetComponent<Rigidbody>();
		GetComponent<Rigidbody>().isKinematic = true;       //Pysäyttää kuution(pelaajan)
	}

	// Update is called once per frame
	void Update()
	{
		thrust = rb.position.z - coordinateF;

		if (Input.GetKeyDown(KeyCode.Space))
		{

			rb.AddForce(Vector3.forward * thrust * 15);
			rb.AddForce(Vector3.up * thrust * 15);
			Debug.Log("Työnnön voima: " + thrust);
		}




	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("hyppyalue"))
		{
			//Laske koordinaatin mukaan addforce voima, jolla "hypätään".
			coordinateF = rb.position.z;
			Debug.Log("Ensimmäinen koordinaatti: " + coordinateF);
			
			



		}
	}
}

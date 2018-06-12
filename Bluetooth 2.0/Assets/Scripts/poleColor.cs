using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poleColor : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void varitakas()
	{
		gameObject.GetComponent<Renderer>().material.color = Color.green;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		}
	}
}

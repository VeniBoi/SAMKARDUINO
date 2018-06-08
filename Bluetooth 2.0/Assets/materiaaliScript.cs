using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materiaaliScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void materiaaliVaihto()
	{
		this.GetComponent<Collider>().material.dynamicFriction = 0.5f;
	}

	public void materiaaliVaihtoTakas()
	{
		this.GetComponent<Collider>().material.dynamicFriction = 0f;
	}
}

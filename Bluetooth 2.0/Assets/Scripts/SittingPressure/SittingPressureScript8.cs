using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SittingPressureScript8 : MonoBehaviour
{

	int moveValue;
	Vector3 startposition;

	public Text text;
	// Use this for initialization
	void Start()
	{
		startposition = this.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		moveValue = BasicDemo.S8;

		if (Input.GetKeyDown("d"))
		{
			transform.position = new Vector3(transform.position.x, 5, transform.position.z);
		}
		else if (Input.GetKeyDown("f"))
		{
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}

		text.text = "Koordinaatti 8: " + transform.position.y;
	}
}
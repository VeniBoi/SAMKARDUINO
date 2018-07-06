using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingPressureScript1 : MonoBehaviour
{

	float moveValue;
	Vector3 startposition;

	// Use this for initialization
	void Start()
	{
		startposition = this.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		moveValue = BasicDemo.S1;

		if (moveValue > 0)
		{
			transform.position = new Vector3(0, -moveValue / 32, 0);
		}
		else
		{
			this.transform.position = startposition;
		}
	}
}

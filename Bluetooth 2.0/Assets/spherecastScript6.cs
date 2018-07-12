using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spherecastScript6 : MonoBehaviour
{

	public List<GameObject> cubeobjects = new List<GameObject>();


	private void Awake()
	{
		cubesList(transform.position, 4f);
	}

	void Update()
	{
		transform.position = new Vector3(transform.position.x, -BasicDemo.S6 / 60, transform.position.z);
		CheckSurrounding();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, transform.localScale * 7.5f);
	}

	private void cubesList(Vector3 center, float radius)
	{
		Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale * 4, Quaternion.identity);

		foreach (Collider objekti in hitColliders)
		{
			float distance;
			distance = (transform.position - objekti.transform.position).sqrMagnitude;
			cubeobjects.Add(objekti.gameObject);
			objekti.GetComponent<distanceVariableScript>().distance = distance;
		}
	}

	void CheckSurrounding()
	{
		foreach (GameObject objekti in cubeobjects)
		{
			if (objekti.GetComponent<distanceVariableScript>().distance < 2f)
			{
				float yArvo = transform.position.y / objekti.GetComponent<distanceVariableScript>().distance;
				float yourFloat = Mathf.Round(yArvo * 100f) / 100f;
				objekti.transform.position = new Vector3(objekti.transform.position.x, yourFloat * 2f, objekti.transform.position.z);
			}
			else if (objekti.GetComponent<distanceVariableScript>().distance > 2f && objekti.GetComponent<distanceVariableScript>().distance < 4f)
			{
				float yArvo = transform.position.y / objekti.GetComponent<distanceVariableScript>().distance;
				float yourFloat = Mathf.Round(yArvo * 100f) / 100f;
				objekti.transform.position = new Vector3(objekti.transform.position.x, yourFloat * 4f, objekti.transform.position.z);
			}
			else
			{
				float yArvo = transform.position.y / objekti.GetComponent<distanceVariableScript>().distance;
				float yourFloat = Mathf.Round(yArvo * 100f) / 100f;
				objekti.transform.position = new Vector3(objekti.transform.position.x, yourFloat * 10, objekti.transform.position.z);
			}
		}
	}
}

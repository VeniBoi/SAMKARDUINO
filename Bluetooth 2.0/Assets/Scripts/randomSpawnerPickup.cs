using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawnerPickup : MonoBehaviour
{
	public GameObject[] spawnees;
	public Transform spawnPos;
	int randomInt;

	// Use this for initialization
	void Start()
	{
		randomSpawner();


	}
	void Update()
	{


	}

	void randomSpawner()                                        //Ottaa random objektin sille asetetusta listasta
	{                                                           // Objektin voi asettaa listaan unityn editorista (public).
		randomInt = Random.Range(0, spawnees.Length);
		Instantiate(spawnees[randomInt], spawnPos.position, Quaternion.Euler(0, 0, 197f));
	}



}
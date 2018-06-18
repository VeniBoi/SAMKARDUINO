using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lumisotaScript : MonoBehaviour {

	

	// Use this for initialization
	void Start () {
		aikatuhousVoid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void tuhousScript()
	{
		GameObject.Find("Main Camera").GetComponent<kameraScript>().maalitauluunOsuttu();
		Debug.Log("Rikki män!");
		Destroy(this.gameObject);

	}

	public void aikatuhousVoid()
	{
		StartCoroutine(aikatuhous());
	}

	IEnumerator aikatuhous()
	{
		yield return new WaitForSeconds(3);
		Destroy(this.gameObject);
	}
}

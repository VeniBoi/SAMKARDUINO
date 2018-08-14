using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaatteidenVaihto : MonoBehaviour {

	public Material[] materiaalit;
	public string[] animaatio;
	Vector3 startPosition, upPosition;
	bool vaihto;
	private float y;

	Animator yourAnimation;

	// Use this for initialization
	void Start () {
		
		this.GetComponent<MeshRenderer>().material = materiaalit[Random.Range(0, materiaalit.Length)];
		yourAnimation = GetComponent<Animator>();
		yourAnimation.Play("Armature|BOIGA");
		
		//this.GetComponent<Animator>().Play(Random.Range(0, animaatio.Length), -1, 0.0f);
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
	
	/*IEnumerator floatingUp()
	{
		transform.position.y += 0.5 * Time.deltaTime;
	}
	*/
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spherecastScript : MonoBehaviour
{

	public LayerMask m_LayerMask;


	// Use this for initialization
	void Start()
	{
		//transform.position = new Vector3(transform.position.x, -7f, transform.position.z);
		
	}

	// Update is called once per frame
	void Update()
	{


		if (Input.GetKeyDown("r"))
		{
			Debug.Log("DABBABABBABABABAB");
			ExplosionDamage(transform.position, 3f);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, 3f);
	}

	void ExplosionDamage(Vector3 center, float radius)
	{
		Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, radius);

		
		foreach (Collider objekti in hitColliders)
		{
			Debug.Log(objekti.name);
			float distance;
			
			distance = (this.transform.position - objekti.transform.position).sqrMagnitude;
			
			objekti.transform.position = new Vector3(objekti.transform.position.x, (transform.position.y / distance), objekti.transform.position.z);
			

		}


		/*int i = 0;
		while (i < hitColliders.Length)
		{
			Debug.Log("Hit : " + hitColliders[i].name);
			hitColliders[i].GetComponent<Transform>().position = new Vector3(hitColliders[i].transform.position.x, -5, hitColliders[i].transform.position.z);
			i++;
		}
		*/
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	public float force;
	void OnCollisionEnter2D(Collision2D collision)
	{
		var contactObject = collision.gameObject;
		var direction = (contactObject.transform.position - transform.position).normalized;
		contactObject.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	public float force;
	public Animator anim;


	void OnCollisionEnter2D(Collision2D collision)
	{
		anim.SetTrigger("Hit");
		var contactObject = collision.gameObject;
		var direction = (contactObject.transform.position - transform.position).normalized;
		contactObject.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
	}
}

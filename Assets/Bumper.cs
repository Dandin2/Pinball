using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	public float force;
	public Animator anim;
	public Transform directionalTransform;

	void OnCollisionEnter2D(Collision2D collision)
	{
		anim.SetTrigger("Hit");
		Vector3 direction = Vector3.zero;
		if (directionalTransform != null)
		{
			direction = (transform.position - directionalTransform.position).normalized;
		}
		else
		{
			direction = (collision.gameObject.transform.position - transform.position).normalized;
		}
		collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
	}
}

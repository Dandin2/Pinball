using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	public float minForce;
	public float maxForce;
	public Animator anim;
	public Transform directionalTransform;
	public int PointsToAdd = 0;

	void OnCollisionEnter2D(Collision2D collision)
	{
		anim.SetTrigger("Hit");
		Vector3 direction;
		if (directionalTransform != null)
		{
			direction = (transform.position - directionalTransform.position).normalized;
		}
		else
		{
			direction = (collision.gameObject.transform.position - transform.position).normalized;
		}
		var force = Random.Range(minForce, maxForce);
		collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
		GameManager.Instance.UpdatePoints(PointsToAdd);
	}
}

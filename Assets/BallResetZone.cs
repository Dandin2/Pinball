using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetZone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		var ball = collision.gameObject.GetComponent<Ball>();
		if(ball != null)
		{
			ball.ResetPosition();
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	Rigidbody2D rb => GetComponent<Rigidbody2D>();

	private Vector2 startingPosition;
	private AudioSource audioSource;

	private void Start()
	{
		Debug.Log(transform.position);
		startingPosition = transform.position;
		audioSource = GetComponent<AudioSource>();
	}

	internal void ResetPosition()
	{
		transform.position = startingPosition;
		rb.velocity = Vector3.zero;
		GameManager.Instance.UseLife();
	}

	private void Update()
	{
		if (!audioSource.isPlaying && rb.velocity.magnitude > 0f)
		{
			audioSource.Play();
		}
		else if(audioSource.isPlaying && rb.velocity.magnitude == 0f)
		{
			audioSource.Stop();
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			ResetPosition();
		}
		if (rb.velocity.y > 0)
			rb.gravityScale = 50;
		else
			rb.gravityScale = 100;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		StartCoroutine(DelayThenSpin());
	}

	private IEnumerator DelayThenSpin()
	{
		yield return new WaitForSeconds(0.2f);
		GetComponent<Rigidbody2D>().angularVelocity = 15f;
		yield break;
	}
}

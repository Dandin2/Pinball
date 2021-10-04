using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	Rigidbody2D rb => GetComponent<Rigidbody2D>();

	public List<GameObject> disableOnResetPositionGOs;

	private Vector2 startingPosition;
	private AudioSource audioSource;

	private void Start()
	{
		startingPosition = transform.position;
		audioSource = GetComponent<AudioSource>();
	}

	internal void ResetPosition()
	{
		transform.position = startingPosition;
		rb.velocity = Vector3.zero;
		GameManager.Instance.UseLife();
		foreach (GameObject go in disableOnResetPositionGOs)
			go.SetActive(false);
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

	public void Suspend()
    {
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

	public void UnSuspend()
	{
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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

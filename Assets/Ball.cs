using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	Rigidbody2D rb => GetComponent<Rigidbody2D>();
	public float upGravityScale;
	public float velocityLight;

	public List<GameObject> disableOnResetPositionGOs;

	private Vector2 startingPosition;
	private AudioSource audioSource;
	private float maxGravityScale;

	private void Start()
	{
		startingPosition = transform.position;
		audioSource = GetComponent<AudioSource>();
		maxGravityScale = rb.gravityScale;
	}

	internal void ResetPosition()
	{
		transform.position = startingPosition;
		rb.velocity = Vector3.zero;
		GameManager.Instance.UseLife();
		foreach (GameObject go in disableOnResetPositionGOs)
			go.SetActive(false);
		GameManager.Instance.Border.SetObjectiveText("Press and hold space bar to launch.", false);
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

		if (rb.velocity.y > velocityLight)
			rb.gravityScale = upGravityScale;
		else
			rb.gravityScale = maxGravityScale;
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

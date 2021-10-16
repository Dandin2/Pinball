using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public KeyCode TiltLeft;
    public KeyCode TiltRight;
	public float LeftAngle;
	public float RightAngle;
	public List<Sprite> ringSprites;

	private State ringState = State.Center;
	private Rigidbody2D _rb;
	private float rotationSpeed = 5f;

	private int index = 0;
	private enum State
	{
		Left = 0,
		Center = 1,
		Right = 2
	}

	public void GoToNextSprite()
    {
		if (index < ringSprites.Count)
		{
			GetComponent<SpriteRenderer>().sprite = ringSprites[index];
			index++;
		}
	}

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(ControlsManager.Instance.LeftTilt))
		{
			if (ringState != State.Left)
				ringState--;
		}
		else if (Input.GetKeyDown(ControlsManager.Instance.RightTilt))
		{
			if (ringState != State.Right)
				ringState++;
		}
	}

	private void FixedUpdate()
	{
		if(ringState == State.Left && transform.rotation.eulerAngles.z != LeftAngle)
		{
			var rotation = Mathf.Lerp(_rb.rotation, LeftAngle, rotationSpeed * Time.fixedDeltaTime);
			if (LeftAngle - rotation < 1)
			{
				rotation = LeftAngle;
			}
			_rb.MoveRotation(rotation);
		}
		else if(ringState == State.Center && transform.rotation.eulerAngles.z != 0)
		{
			var rotation = Mathf.Lerp(_rb.rotation, 0, rotationSpeed * Time.fixedDeltaTime);
			if (rotation < 1 && rotation > -1)
			{
				rotation = 0;
			}
			_rb.MoveRotation(rotation);
		}
		else if(ringState == State.Right && transform.rotation.eulerAngles.z != RightAngle)
		{
			var rotation = Mathf.Lerp(_rb.rotation, RightAngle, rotationSpeed * Time.fixedDeltaTime);
			if(RightAngle - rotation > -1)
			{
				rotation = RightAngle;
			}
			_rb.MoveRotation(rotation);
		}
	}
}

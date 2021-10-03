using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    public KeyCode PlungerActivationKey;
    public Rigidbody2D PlungerRigidBody;

    private float startPosition;
    private RectTransform t;
    private AudioSource audioSource;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPosition = PlungerRigidBody.transform.position.y;
        t = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(PlungerActivationKey))
		{
            AddPower();
            started = true;
        }
        else if(started)
        {
            started = false;
            ReleasePlunger();
		}
    }

	private void ReleasePlunger()
	{
        audioSource.Play();
        PlungerRigidBody.MovePosition(new Vector2(PlungerRigidBody.transform.position.x, startPosition));
    }

	private void AddPower()
    {
        if (PlungerRigidBody.transform.position.y - 1 > gameObject.transform.position.y - (t.rect.height / 2))
        {
            PlungerRigidBody.transform.position = new Vector2(PlungerRigidBody.transform.position.x, PlungerRigidBody.transform.position.y - 1);
        }
    }
}

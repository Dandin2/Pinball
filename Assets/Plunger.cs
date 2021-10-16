using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    //public KeyCode PlungerActivationKey;
    public Rigidbody2D PlungerRigidBody;
    public bool IsAutoPlunger;
    public GameObject AutoPlungerBlocker;
    public SpriteRenderer Replenisher;

    private float startPosition;
    private RectTransform t;
    private AudioSource audioSource;
    private bool started = false;
    private bool autoStart = false;
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
        if ((!IsAutoPlunger && Input.GetKey(ControlsManager.Instance.Launcher)) || (IsAutoPlunger && autoStart))
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
        if(!IsAutoPlunger)
            GameManager.Instance.Border.SetObjectiveText("Hit the targets along the wall to get objectives.", false);
    }

	private void AddPower()
    {
        if (PlungerRigidBody.transform.position.y - 1 > gameObject.transform.position.y - (t.rect.height / 2))
        {
            PlungerRigidBody.transform.position = new Vector2(PlungerRigidBody.transform.position.x, PlungerRigidBody.transform.position.y - 1);
        }
    }

	private void OnTriggerEnter2D(Collider2D collider)
	{
        var ball = collider.GetComponent<Ball>();
		if(IsAutoPlunger && ball != null)
		{
            autoStart = true;
            Invoke("AutoReleasePlunger", 1f);
		}
	}

    private void AutoReleasePlunger()
	{
        autoStart = false;
        Invoke("CloseAutoPlunger", .5f);
    }
    private void CloseAutoPlunger()
    {
        Replenisher.color = new Color(96 / 255, 96 / 255, 96 / 255);
        AutoPlungerBlocker.gameObject.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Button;

public class Trigger : MonoBehaviour
{
    public Color defaultColor;
    public Color activatedColor;
    public int PointValue = 150;

    public Quest myQuest;
    public List<Trigger> fellowFriends;

    public bool isTriggered = false;
    public bool hasIndividualTrigger = false;
    public ButtonClickedEvent OnTriggerEvent;
    private Action triggerAction;
    private AudioSource audioSource;
    private Collider2D collider;

    private Animator animator;

    private bool canTrigger = true;

	private void Awake()
	{
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

    }

	public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O) && myQuest == Quest.SocialArea)
        //{
        //    if (hasIndividualTrigger)
        //        OnTriggerEvent.Invoke();
        //    else
        //        Activate();
        //}
        //else if (Input.GetKeyDown(KeyCode.I) && myQuest == Quest.LivingQuarters)
        //{
        //    if (hasIndividualTrigger)
        //        OnTriggerEvent.Invoke();
        //    else
        //        Activate();
        //}
        //else if (Input.GetKeyDown(KeyCode.U) && myQuest == Quest.DiningHall)
        //{
        //    if (hasIndividualTrigger)
        //        OnTriggerEvent.Invoke();
        //    else
        //        Activate();
        //}
    }

    public void SetTriggerAction(Action onTrigger)
    {
        audioSource = GetComponent<AudioSource>();
        triggerAction = onTrigger;
    }

    public void Deactivate()
    {
        isTriggered = false;
        //GetComponent<SpriteRenderer>().color = isTriggered ? activatedColor : defaultColor;
        animator.SetBool("isOn", isTriggered);
        triggerAction?.Invoke();
    }

    public void TurnOff()
    {
        isTriggered = false;
        //GetComponent<SpriteRenderer>().color = isTriggered ? activatedColor : defaultColor;
        animator.SetBool("isActive", false);
        animator.SetBool("isOn", false);
        canTrigger = false;
        collider.enabled = false;
    }

    //If you know what I mean winkyface
    public void TurnOn()
    {
        animator.SetBool("isActive", true);
        canTrigger = true;
        collider.enabled = true;
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canTrigger)
        {
            if (collision.gameObject.name == "Ball")
            {
                audioSource.Play();
                GameManager.Instance.UpdatePoints(PointValue);
                if (hasIndividualTrigger)
                    OnTriggerEvent.Invoke();
                else
                    Activate();
            }
        }
    }

    public void Activate()
    {
        bool initialActivation = !isTriggered;

        isTriggered = true;
        //GetComponent<SpriteRenderer>().color = isTriggered ? activatedColor : defaultColor;
        animator.SetBool("isOn", isTriggered);
        if (initialActivation)
            triggerAction?.Invoke();
    }
}

public enum Quest
{
    DiningHall,
    LivingQuarters,
    SocialArea,
    None
}
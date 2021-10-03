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

    public Quest myQuest;
    public List<Trigger> fellowFriends;

    public bool isTriggered = false;
    public bool hasIndividualTrigger = false;
    public ButtonClickedEvent OnTriggerEvent;
    private Action triggerAction;

    public void SetTriggerAction(Action onTrigger)
    {
        triggerAction = onTrigger;
    }

    public void Deactivate()
    {
        isTriggered = false;
        GetComponent<SpriteRenderer>().color = isTriggered ? activatedColor : defaultColor;
        triggerAction?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            if (hasIndividualTrigger)
                OnTriggerEvent.Invoke();
            else
                Activate();
        }
    }

    public void Activate()
    {
        isTriggered = !isTriggered;
        GetComponent<SpriteRenderer>().color = isTriggered ? activatedColor : defaultColor;
        triggerAction?.Invoke();
    }
}

public enum Quest
{
    DiningHall,
    LivingQuarters,
    SocialArea
}
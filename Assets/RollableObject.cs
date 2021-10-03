using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Button;

public class RollableObject : MonoBehaviour
{
    public ActivationRequirement thingThatHappens;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Ball")
        {
            if (thingThatHappens.condition == InteractCondition.EnterTop && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
                thingThatHappens.action?.Invoke();
            else if (thingThatHappens.condition == InteractCondition.EnterBottom && collision.GetComponent<Rigidbody2D>().velocity.y > 0)
                thingThatHappens.action?.Invoke();
            else if (thingThatHappens.condition == InteractCondition.EnterLeft && collision.GetComponent<Rigidbody2D>().velocity.x > 0)
                thingThatHappens.action?.Invoke();
            else if (thingThatHappens.condition == InteractCondition.EnterRight && collision.GetComponent<Rigidbody2D>().velocity.x < 0)
                thingThatHappens.action?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            if (thingThatHappens.condition == InteractCondition.ExitTop && collision.GetComponent<Rigidbody2D>().velocity.y > 0)
                thingThatHappens.action?.Invoke();
            else if (thingThatHappens.condition == InteractCondition.ExitBottom && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
                thingThatHappens.action?.Invoke();
            else if (thingThatHappens.condition == InteractCondition.ExitLeft && collision.GetComponent<Rigidbody2D>().velocity.x < 0)
                thingThatHappens.action?.Invoke();
            else if (thingThatHappens.condition == InteractCondition.ExitRight && collision.GetComponent<Rigidbody2D>().velocity.x > 0)
                thingThatHappens.action?.Invoke();
        }
    }
}

[Serializable]
public class ActivationRequirement
{
    public InteractCondition condition;
    public ButtonClickedEvent action;
}

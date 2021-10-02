using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Color defaultColor;
    public Color activatedColor;

    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            Activate();
        }
    }

    public void Activate()
    {
        isTriggered = !isTriggered;
        GetComponent<SpriteRenderer>().color = isTriggered ? activatedColor : defaultColor;
    }
}

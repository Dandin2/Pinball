using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerEditor : MonoBehaviour
{
    public List<ConditionalEnable> conditions;
    public bool enableDisableHitboxes = true;
    public bool enableDisableGameObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            ConditionalEnable ce = null;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (Math.Abs(rb.velocity.x) > Math.Abs(rb.velocity.y)) //collision.transform.position.x > transform.position.x)
            {
                if (rb.velocity.x > 0)
                    ce = conditions.Where(x => x.condition == InteractCondition.EnterLeft).FirstOrDefault();
                else
                    ce = conditions.Where(x => x.condition == InteractCondition.EnterRight).FirstOrDefault();
            }
            else
            {
                if (rb.velocity.y > 0)
                    ce = conditions.Where(x => x.condition == InteractCondition.EnterBottom).FirstOrDefault();
                else
                    ce = conditions.Where(x => x.condition == InteractCondition.EnterTop).FirstOrDefault();
            }

            if (ce != null)
            {
                if (enableDisableHitboxes)
                {
                    foreach (GameObject go in ce.enableHitboxes)
                        go.GetComponent<PolygonCollider2D>().enabled = true;
                    foreach (GameObject go in ce.disableHitboxes)
                        go.GetComponent<PolygonCollider2D>().enabled = false;
                }
                else if (enableDisableGameObjects)
                {
                    foreach (GameObject go in ce.enableHitboxes)
                        go.SetActive(true);
                    foreach (GameObject go in ce.disableHitboxes)
                        go.SetActive(false);
                }

                if (ce.bridge.sprite != null)
                    ce.bridge.sprite.sortingOrder = ce.bridge.orderInLayer;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Ball")
        {
            ConditionalEnable ce = null;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (Math.Abs(rb.velocity.x) > Math.Abs(rb.velocity.y)) //collision.transform.position.x > transform.position.x)
            {
                if (rb.velocity.x > 0)
                    ce = conditions.Where(x => x.condition == InteractCondition.ExitRight).FirstOrDefault();
                else
                    ce = conditions.Where(x => x.condition == InteractCondition.ExitLeft).FirstOrDefault();
            }
            else
            {
                if (rb.velocity.y > 0)
                    ce = conditions.Where(x => x.condition == InteractCondition.ExitTop).FirstOrDefault();
                else
                    ce = conditions.Where(x => x.condition == InteractCondition.ExitBottom).FirstOrDefault();
            }

            if(ce != null)
            {
                if (enableDisableHitboxes)
                {
                    foreach (GameObject go in ce.enableHitboxes)
                        go.GetComponent<PolygonCollider2D>().enabled = true;
                    foreach (GameObject go in ce.disableHitboxes)
                        go.GetComponent<PolygonCollider2D>().enabled = false;
                }
                else if (enableDisableGameObjects)
                {
                    foreach (GameObject go in ce.enableHitboxes)
                        go.SetActive(true);
                    foreach (GameObject go in ce.disableHitboxes)
                        go.SetActive(false);
                }

                if (ce.bridge.sprite != null)
                    ce.bridge.sprite.sortingOrder = ce.bridge.orderInLayer;
            }
        }
    }
}

[Serializable]
public class ConditionalEnable
{
    public InteractCondition condition;
    public List<GameObject> disableHitboxes;
    public List<GameObject> enableHitboxes;

    public SpriteChanger bridge;
}

[Serializable]
public class SpriteChanger
{
    public SpriteRenderer sprite;
    public int orderInLayer;
}

public enum InteractCondition
{
    EnterTop,
    EnterBottom,
    EnterLeft,
    EnterRight,
    ExitTop,
    ExitBottom,
    ExitLeft,
    ExitRight
}

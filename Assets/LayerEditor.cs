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

            List<ConditionalEnable> ce = new List<ConditionalEnable>();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            //if (collision.transform.position.x > transform.position.x)
            //    ce.Add(conditions.Where(x => x.condition == InteractCondition.EnterRight).FirstOrDefault());
            //else if (collision.transform.position.x < transform.position.x)
            //    ce.Add(conditions.Where(x => x.condition == InteractCondition.EnterLeft).FirstOrDefault());

            //if (collision.transform.position.y > transform.position.y)
            //    ce.Add(conditions.Where(x => x.condition == InteractCondition.EnterTop).FirstOrDefault());
            //else if (collision.transform.position.y < transform.position.y)
            //    ce.Add(conditions.Where(x => x.condition == InteractCondition.EnterBottom).FirstOrDefault());


            if (rb.velocity.x > 0)
            {
                ConditionalEnable x = conditions.Where(x => x.condition == InteractCondition.EnterLeft).FirstOrDefault();
                if (x != null)
                    ce.Add(x);
            }
            else
            {
                ConditionalEnable x = conditions.Where(x => x.condition == InteractCondition.EnterRight).FirstOrDefault();
                if (x != null)
                    ce.Add(x);
            }

            if (rb.velocity.y > 0)
            {
                ConditionalEnable x = conditions.Where(x => x.condition == InteractCondition.EnterBottom).FirstOrDefault();
                if (x != null)
                    ce.Add(x);
            }
            else
            {
                ConditionalEnable x = conditions.Where(x => x.condition == InteractCondition.EnterTop).FirstOrDefault();
                if (x != null)
                    ce.Add(x);
            }

            if (ce.Count > 0)
            {
                if (enableDisableHitboxes)
                {
                    foreach (GameObject go in ce.SelectMany(x => x.enableHitboxes))
                        go.GetComponent<PolygonCollider2D>().enabled = true;
                    foreach (GameObject go in ce.SelectMany(x => x.disableHitboxes))
                        go.GetComponent<PolygonCollider2D>().enabled = false;
                }
                else if (enableDisableGameObjects)
                {
                    foreach (GameObject go in ce.SelectMany(x => x.enableHitboxes))
                        go.SetActive(true);
                    foreach (GameObject go in ce.SelectMany(x => x.disableHitboxes))
                        go.SetActive(false);
                }

                foreach (ConditionalEnable test in ce)
                    if (test.bridge.sprite != null)
                        test.bridge.sprite.sortingOrder = test.bridge.orderInLayer;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            List<ConditionalEnable> ce = new List<ConditionalEnable>();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb.velocity.x > 0)
            {
                ConditionalEnable x = conditions.Where(x => x.condition == InteractCondition.ExitRight).FirstOrDefault();
                if (x != null)
                    ce.Add(x);
            }
            else
            {
                ConditionalEnable x = conditions.Where(x => x.condition == InteractCondition.ExitLeft).FirstOrDefault();
                if (x != null)
                    ce.Add(x);
            }

            if (rb.velocity.y > 0)
            {
                ConditionalEnable x = conditions.Where(x => x.condition == InteractCondition.ExitTop).FirstOrDefault();
                if (x != null)
                    ce.Add(x);
            }
            else
            {
                ConditionalEnable x = conditions.Where(x => x.condition == InteractCondition.ExitBottom).FirstOrDefault();
                if (x != null)
                    ce.Add(x);
            }

            if (ce.Count > 0)
            {
                if (enableDisableHitboxes)
                {
                    foreach (GameObject go in ce.SelectMany(x => x.enableHitboxes))
                        go.GetComponent<PolygonCollider2D>().enabled = true;
                    foreach (GameObject go in ce.SelectMany(x => x.disableHitboxes))
                        go.GetComponent<PolygonCollider2D>().enabled = false;
                }
                else if (enableDisableGameObjects)
                {
                    foreach (GameObject go in ce.SelectMany(x => x.enableHitboxes))
                        go.SetActive(true);
                    foreach (GameObject go in ce.SelectMany(x => x.disableHitboxes))
                        go.SetActive(false);
                }

                foreach (ConditionalEnable test in ce)
                    if (test.bridge.sprite != null)
                        test.bridge.sprite.sortingOrder = test.bridge.orderInLayer;
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerEditor : MonoBehaviour
{
    public List<ConditionalEnable> conditions;

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
                foreach (GameObject go in ce.enableHitboxes)
                    go.GetComponent<PolygonCollider2D>().enabled = true;
                foreach (GameObject go in ce.disableHitboxes)
                    go.GetComponent<PolygonCollider2D>().enabled = false;
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
                foreach (GameObject go in ce.enableHitboxes)
                    go.GetComponent<PolygonCollider2D>().enabled = true;
                foreach (GameObject go in ce.disableHitboxes)
                    go.GetComponent<PolygonCollider2D>().enabled = false;
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

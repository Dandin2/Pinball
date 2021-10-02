using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerEditor : MonoBehaviour
{
    public List<LayerChangeInfo> ConditionalChanges;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            LayerChangeInfo lci = null;
            //Check the speed of the ball,  Assume that whichever direction is "fastest" is the direction you exited out of.  IDK why
            if (Math.Abs(collision.GetComponent<Rigidbody2D>().velocity.x) > Math.Abs(collision.GetComponent<Rigidbody2D>().velocity.y))//collision.transform.position.x > transform.position.x)
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.x > 0)
                    lci = ConditionalChanges.Where(x => x.exitPosition == PositionTriggerType.Right).FirstOrDefault();
                if (collision.GetComponent<Rigidbody2D>().velocity.x < 0)
                    lci = ConditionalChanges.Where(x => x.exitPosition == PositionTriggerType.Left).FirstOrDefault();
            }
            else
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.y > 0)
                    lci = ConditionalChanges.Where(x => x.exitPosition == PositionTriggerType.Above).FirstOrDefault();
                if (collision.GetComponent<Rigidbody2D>().velocity.y < 0)
                    lci = ConditionalChanges.Where(x => x.exitPosition == PositionTriggerType.Below).FirstOrDefault();
            }

            if (lci != null)
            {
                foreach (GameObject go in lci.objectsToDisableCollision)
                    go.GetComponent<PolygonCollider2D>().enabled = false;
                foreach (GameObject go in lci.objectsToEnableCollision)
                    go.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }
    }
}

[Serializable]
public class LayerChangeInfo
{
    public PositionTriggerType exitPosition;
    public List<GameObject> objectsToDisableCollision;
    public List<GameObject> objectsToEnableCollision;
}

public enum PositionTriggerType
{
    ExitAbove,
    ExitBelow,
    ExitLeft,
    ExitRight,
    EnterAbove,
    EnterBelow,
    EnterLeft,
    EnterRight
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerEditor : MonoBehaviour
{
    public List<GameObject> aboveGameObjects;
    public List<GameObject> belowGameObjects;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Ball")
        {
            if(collision.transform.position.x > transform.position.x)
            {

            }
        }
    }
}

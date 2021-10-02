using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 85000));
        }
    }
}

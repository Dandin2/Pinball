using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position = new Vector3(-250, -701);
            rb.velocity = Vector3.zero;
        }
        if (rb.velocity.y > 0)
            rb.gravityScale = 50;
        else
            rb.gravityScale = 100;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DelayThenSpin());
    }

    private IEnumerator DelayThenSpin()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Rigidbody2D>().angularVelocity = 15f;
        yield break;
    }
}

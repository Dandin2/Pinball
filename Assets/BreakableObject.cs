using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject postBreakGO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Break();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Break();
    }

    private void Break()
    {
        GetComponent<Animator>().SetBool("Broke", true);
    }

    public void PostBreak()
    {
        if (postBreakGO != null)
        {
            GameObject go = Instantiate(postBreakGO);
            go.transform.position = transform.position;
            go.transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
        }
        else
            GetComponent<PolygonCollider2D>().enabled = false;
    }
}

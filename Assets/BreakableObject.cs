using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject postBreakGO;
    public Action breakAction;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Break();
        if (Input.GetKeyDown(KeyCode.Z))
            BreakInstantly();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Break();
    }

    private void Break()
    {
        GetComponent<Animator>().SetBool("Broke", true);
    }

    public void BreakInstantly()
    {
        GetComponent<Animator>().SetBool("BreakInstantly", true);
        GetComponent<Animator>().SetBool("Unbreak", false);
    }


    public void UnBreak()
    {
        GetComponent<Animator>().SetBool("Broke", false);
        GetComponent<Animator>().SetBool("BreakInstantly", false);
        GetComponent<Animator>().SetBool("Unbreak", true);
        GetComponent<PolygonCollider2D>().enabled = true;
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
        else if(GetComponent<Animator>().GetBool("Broke") || GetComponent<Animator>().GetBool("BreakInstantly"))
            GetComponent<PolygonCollider2D>().enabled = false;
        else if(GetComponent<Animator>().GetBool("Unbreak"))
            GetComponent<PolygonCollider2D>().enabled = true;

        GetComponent<Animator>().SetBool("Broke", false);
        GetComponent<Animator>().SetBool("BreakInstantly", false);
        GetComponent<Animator>().SetBool("Unbreak", false);
        breakAction?.Invoke();
    }

    public void SetBreakAction(Action onBreak)
    {

    }
}

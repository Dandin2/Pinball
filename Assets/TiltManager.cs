using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TiltManager : MonoBehaviour
{
    //public KeyCode LeftTiltKey;
    //public KeyCode RightTiltKey;
    public float bumpDistance;
    public float bumpTime;
    public GameObject Screen;

    private Coroutine bumpCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (bumpCoroutine == null)
        {
            if (Input.GetKeyDown(ControlsManager.Instance.LeftTilt))
            {
                bumpCoroutine = StartCoroutine(BumpTableLeft());
            }
            else if (Input.GetKeyDown(ControlsManager.Instance.RightTilt))
            {
                bumpCoroutine = StartCoroutine(BumpTableRight());
            }
        }
    }

	private IEnumerator BumpTableLeft()
    {
        Screen.transform.position = new Vector2(Screen.transform.position.x - bumpDistance, Screen.transform.position.y);
        
        yield return new WaitForSeconds(bumpTime);

        Screen.transform.position = new Vector2(Screen.transform.position.x + bumpDistance, Screen.transform.position.y);
        bumpCoroutine = null;
    }

    private IEnumerator BumpTableRight()
    {
        Screen.transform.position = new Vector2(Screen.transform.position.x + bumpDistance, Screen.transform.position.y);

        yield return new WaitForSeconds(bumpTime);

        Screen.transform.position = new Vector2(Screen.transform.position.x - bumpDistance, Screen.transform.position.y);
        bumpCoroutine = null;
    }

}

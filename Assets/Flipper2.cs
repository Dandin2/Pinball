using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper2 : MonoBehaviour
{

    public float flipRotation = 60;
    public Rigidbody2D rb;
    public GameObject spriteHolder;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            StartCoroutine(FlipFlippers());
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopAllCoroutines();
            StartCoroutine(ReleaseFlippers());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.SetRotation(60f);
            spriteHolder.transform.rotation = Quaternion.Euler(0, 0, 60);
        }
    }


    private IEnumerator FlipFlippers()
    {
        float totalTime = 0.1f;
        float currentTime = totalTime * transform.rotation.z / flipRotation;

        while (currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            rb.MoveRotation(flipRotation * currentTime / totalTime);
            spriteHolder.transform.rotation = Quaternion.Euler(0, 0, flipRotation * currentTime / totalTime);
            yield return new WaitForEndOfFrame();
        }
        rb.SetRotation(flipRotation);
        spriteHolder.transform.rotation = Quaternion.Euler(0, 0, flipRotation);

        yield break;
    }

    private IEnumerator ReleaseFlippers()
    {
        float totalTime = 0.1f;
        float currentTime = totalTime - (totalTime * transform.rotation.eulerAngles.z / flipRotation);

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            rb.MoveRotation(flipRotation * currentTime / totalTime);
            spriteHolder.transform.rotation = Quaternion.Euler(0, 0, flipRotation * currentTime / totalTime);
            yield return new WaitForEndOfFrame();
        }
        rb.SetRotation(0);
        spriteHolder.transform.rotation = Quaternion.Euler(0, 0, 0);

        yield break;
    }
}

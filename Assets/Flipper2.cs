using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper2 : MonoBehaviour
{

    public float flipRotation;
    public Rigidbody2D rb;
    public GameObject spriteHolder;
    public KeyCode activationKey;

    private float endRotation;
    private Coroutine activatingRoutine;
    private Coroutine deactivatingRoutine;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        endRotation = rb.transform.rotation.eulerAngles.z;
	}
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            if (activatingRoutine != null)
            {
                StopCoroutine(activatingRoutine);
            }
            audioSource.Play();
            activatingRoutine = StartCoroutine(FlipFlippers());
        }
        else if (Input.GetKeyUp(activationKey))
        {
            if (deactivatingRoutine != null)
            {
                StopCoroutine(deactivatingRoutine);
            }
            deactivatingRoutine = StartCoroutine(ReleaseFlippers());
        }

    }

	private IEnumerator FlipFlippers()
    {
        float totalTime = 0.08f;
        float currentTime = totalTime * transform.rotation.z / (endRotation + flipRotation);

        while (currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            //rb.MoveRotation(flipRotation * currentTime / totalTime);
            transform.rotation = Quaternion.Euler(0, 0, (endRotation + flipRotation) * currentTime / totalTime);
            yield return new WaitForEndOfFrame();
        }
        //rb.SetRotation(endRotation + flipRotation);
        transform.rotation = Quaternion.Euler(0, 0, endRotation + flipRotation);

        yield break;
    }

    private IEnumerator ReleaseFlippers()
    {
        float totalTime = 0.08f;
        float currentTime = totalTime - (totalTime * transform.rotation.eulerAngles.z / endRotation);

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            //rb.MoveRotation(endRotation * currentTime / totalTime);
            transform.rotation = Quaternion.Euler(0, 0, endRotation * currentTime / totalTime);
            yield return new WaitForEndOfFrame();
        }
        //rb.SetRotation(endRotation);
        transform.rotation = Quaternion.Euler(0, 0, endRotation);

        yield break;
    }
}

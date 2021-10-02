using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public float totalFlipRotation = 60;
    public GameObject spriteRotate;



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
        if(Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().rotation = 45f;
        }
    }

    private IEnumerator FlipFlippers()
    {
        float totalTime = 0.05f;
        float currentTime = totalTime * transform.rotation.z / totalFlipRotation;

        while(currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            //transform.Rotate(0, 0, totalFlipRotation * Time.deltaTime / totalTime);
            // rb.MoveRotation(Quaternion.Euler(0, 0, currentTime / totalTime * totalFlipRotation));
            GetComponent<Rigidbody2D>().SetRotation(totalFlipRotation * currentTime / totalTime);
            //spriteRotate.transform.rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody2D>().rotation);
            yield return new WaitForEndOfFrame();
        }
        //rb.SetRotation(totalFlipRotation);
        GetComponent<Rigidbody2D>().SetRotation(totalFlipRotation);
        //spriteRotate.transform.rotation = Quaternion.Euler(0, 0, totalFlipRotation);
        //transform.rotation = Quaternion.Euler(0, 0, totalFlipRotation);

        yield break;
    }

    private IEnumerator ReleaseFlippers()
    {
        float totalTime = 0.05f;
        float currentTime = totalTime - (totalTime * transform.rotation.eulerAngles.z / totalFlipRotation);

        Debug.Log("TRANSFORM Z ROTATION : " + transform.rotation.eulerAngles.z);
        Debug.Log("Current Time : " + currentTime);

        while (currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            //transform.Rotate(0, 0, -totalFlipRotation * Time.deltaTime / totalTime);
            //rb.MoveRotation(Quaternion.Euler(0, 0, currentTime / totalTime * totalFlipRotation));
            //GetComponent<Rigidbody2D>().MoveRotation(Quaternion.Euler(0, 0, currentTime / totalTime * totalFlipRotation));
            GetComponent<Rigidbody2D>().SetRotation(totalFlipRotation * currentTime / totalTime);
            //spriteRotate.transform.rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody2D>().rotation);
            yield return new WaitForEndOfFrame();
        }
        //rb.SetRotation(0);
        GetComponent<Rigidbody2D>().SetRotation(0);
        //spriteRotate.transform.rotation = Quaternion.Euler(0, 0, 0);
        //transform.rotation = Quaternion.Euler(0, 0, 0);

        yield break;
    }
}

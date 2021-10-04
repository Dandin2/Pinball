using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Button;

public class Launcher : MonoBehaviour
{
    public ButtonClickedEvent onTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 2000), ForceMode2D.Impulse);
            onTrigger?.Invoke();
        }
    }
}

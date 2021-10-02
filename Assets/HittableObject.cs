using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Button;

public class HittableObject : MonoBehaviour
{
	public ButtonClickedEvent collideEvent;

	void OnCollisionEnter2D(Collision2D collision)
	{
        GameObject contactObject = collision.gameObject;
		if (contactObject.name == "Ball")
			collideEvent?.Invoke();
	}
}

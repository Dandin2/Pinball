using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplenishAutoPlunger : MonoBehaviour
{
    public GameObject blocker;
	public SpriteRenderer Replenisher;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(blocker.activeSelf == true)
		{
			Replenisher.color = new Color(112 / 255, 255 / 255, 115 / 255);
			blocker.SetActive(false);
		}
	}
}

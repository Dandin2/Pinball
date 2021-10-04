using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Button;

[RequireComponent(typeof(Bumper))]
public class ComplexBumper : MonoBehaviour
{
    public int health;
    public Quest myQuest;
    public OrderlyFight myFight;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            health--;
            if(health <= 0)
            {
                myFight.OnFisticuffsBroken();
                Destroy(gameObject);
            }
            else
            {
                myFight.OnFisticuffsDamaged();
            }

        }
    }
}

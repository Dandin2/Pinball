using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Button;

public class OrderlyFight : MonoBehaviour
{
    public List<string> thingsToSayWhenDamaged;
    public List<string> thingsToSayOnThingBreaking;

    public Quest myQuest;

    public List<ComplexBumper> fisticuffs;
    public ButtonClickedEvent onOrderlyDefeated;

    private int brokenLimbs = 0;
    private int orderly;

    private void Awake()
    {
        if (myQuest == Quest.LivingQuarters)
            orderly = 1;
        else if (myQuest == Quest.DiningHall)
            orderly = 2;
        else if (myQuest == Quest.SocialArea)
            orderly = 3;
    }

    public void Activate()
    {
        foreach(ComplexBumper go in fisticuffs)
        {
            go.gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {

    }

    public void OnFisticuffsBroken()
    {
        brokenLimbs++;
        GameManager.Instance.Border.SetOrderlyText(orderly, thingsToSayOnThingBreaking[UnityEngine.Random.Range(0, thingsToSayOnThingBreaking.Count)], 3);

        if(brokenLimbs >= fisticuffs.Count)
        {
            onOrderlyDefeated.Invoke();
        }
    }

    public void OnFisticuffsDamaged()
    {
        GameManager.Instance.Border.SetOrderlyText(orderly, thingsToSayWhenDamaged[UnityEngine.Random.Range(0, thingsToSayWhenDamaged.Count)], 3);
    }
}

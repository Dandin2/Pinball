using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTwo : MonoBehaviour
{
    public SpriteRenderer Arrow;
    public List<Trigger> QuestTriggers;
    public List<Bumper> Bumpers;
    public GameObject TablePrefab;

    private int activatedTriggers = 0;

    private void Awake()
    {
        foreach (Trigger t in QuestTriggers)
        {
            t.SetTriggerAction(() => { OnTriggerActivationChange(t); });
        }
    }

    private void OnTriggerActivationChange(Trigger t)
    {
        if (t.isTriggered)
        {
            activatedTriggers++;
            if (activatedTriggers == QuestTriggers.Count)
                Arrow.gameObject.SetActive(true);
        }
        else
        {
            activatedTriggers--;
            if (activatedTriggers < QuestTriggers.Count)
                Arrow.gameObject.SetActive(false);
        }
    }

    public void TryStartQuest()
    {
        if (Arrow.gameObject.activeSelf == true)
        {
            GameManager.Instance.ActivateQuestTwo();
        }
        else
        {
            //Ring doorbell, have orderly yell at you that food isn't ready yet
        }
    }
}

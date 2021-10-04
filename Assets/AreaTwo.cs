using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTwo : MonoBehaviour
{
    public SpriteRenderer Arrow;
    public List<Trigger> QuestTriggers;
    public List<Bumper> Bumpers;
    public GameObject TablePrefab;
    public OrderlyFight OrderlyFight;

    private int activatedTriggers = 0;
    private List<GameObject> tables = new List<GameObject>();

    private void Awake()
    {
        foreach (Trigger t in QuestTriggers)
        {
            t.SetTriggerAction(() => { OnTriggerActivationChange(t); });
        }
    }

    public void Deactivate()
    {
        foreach (Trigger t in QuestTriggers)
            t.TurnOff();
        Arrow.gameObject.SetActive(false);
    }

    public void Activate()
    {
        foreach (Trigger t in QuestTriggers)
            t.TurnOn();
    }

    private void OnTriggerActivationChange(Trigger t)
    {
        if (t.isTriggered)
        {
            activatedTriggers++;
            if (activatedTriggers == QuestTriggers.Count)
            {
                Arrow.gameObject.SetActive(true);
                GameManager.Instance.Border.SetObjectiveText("Ring the doorbell to start the event in your living quarter.", true);
            }
        }
        else
        {
            activatedTriggers--;
            if (activatedTriggers < QuestTriggers.Count)
            {
                Arrow.gameObject.SetActive(false);
                GameManager.Instance.Border.RemoveObjectiveText("Ring the doorbell to start the event in your living quarter.");
            }
        }
    }

    public void TryStartQuest()
    {
        if (Arrow.gameObject.activeSelf == true && !GameManager.Instance.activeQuest)
        {
            GameManager.Instance.ActivateQuestTwo();
            Arrow.gameObject.SetActive(false);
            OrderlyFight.Activate();
        }
        else
        {
            //Ring doorbell, have orderly yell at you that food isn't ready yet
        }
    }

    public void QuestCompleted()
    {
        GameManager.Instance.DeactivateQuestTwo();
        ResetBumpers();
        Arrow.gameObject.SetActive(false);
        OrderlyFight.Deactivate();
    }

    public void ResetBumpers()
    {
        foreach (Bumper b in Bumpers)
            b.gameObject.SetActive(true);

        foreach (GameObject go in tables)
            Destroy(go);

        tables.Clear();
    }
}

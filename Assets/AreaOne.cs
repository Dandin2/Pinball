using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOne : MonoBehaviour
{
    public SpriteRenderer Arrow;
    public List<GameObject> targetArrows;
    public List<Trigger> QuestTriggers;
    public List<Bumper> Bumpers;
    public GameObject TablePrefab;
    public OrderlyFight OrderlyFight;

    private int activatedTriggers = 0;
    private List<GameObject> tables = new List<GameObject>();

    private void Start()
    {
        foreach (Trigger t in QuestTriggers)
        {
            t.SetTriggerAction(() => { OnTriggerActivationChange(t); });
        }
        Activate();
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
        foreach (GameObject go in targetArrows)
            go.SetActive(true);
    }

    private void OnTriggerActivationChange(Trigger t)
    {
        if (t.isTriggered)
        {
            activatedTriggers++;
            if (activatedTriggers == QuestTriggers.Count)
            {
                foreach (GameObject go in targetArrows)
                    go.SetActive(false);
                Arrow.gameObject.SetActive(true);
                GameManager.Instance.Border.SetObjectiveText("Go up the left ramp to have a scuffle with the doctor.", true);
            }
        }
        else
        {
            activatedTriggers--;
            if (activatedTriggers < QuestTriggers.Count)
            {
                foreach (GameObject go in targetArrows)
                    go.SetActive(true);
                Arrow.gameObject.SetActive(false);
                GameManager.Instance.Border.RemoveObjectiveText("Go up the left ramp to have a scuffle with the doctor.");
            }
        }
    }

    public void TryStartQuest()
    {
        if (Arrow.gameObject.activeSelf == true && !GameManager.Instance.activeQuest)
        {
            GameManager.Instance.ActivateQuestOne();
            //foreach(Bumper b in Bumpers)
            //{
            //    b.gameObject.SetActive(false);
            //    GameObject table = Instantiate(TablePrefab);
            //    table.transform.position = b.transform.position;
            //    tables.Add(table);
            //}
            Arrow.gameObject.SetActive(false);
            OrderlyFight.Activate();
        }
    }

    public void QuestCompleted()
    {
        GameManager.Instance.DeactivateQuestOne();
        ResetBumpers();
        Arrow.gameObject.SetActive(false);
        OrderlyFight.Deactivate();
    }

    public void ResetBumpers()
    {
        foreach (Bumper b in Bumpers)
            b.gameObject.SetActive(true);

        foreach(GameObject go in tables)
            Destroy(go);

        tables.Clear();
    }

    internal void ResetQuestTriggers()
    {
        activatedTriggers = 0;
        Arrow.gameObject.SetActive(false);
        foreach (GameObject go in targetArrows)
            go.SetActive(true);
    }
}

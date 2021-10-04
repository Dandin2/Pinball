using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaThree : MonoBehaviour
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
                GameManager.Instance.Border.SetObjectiveText("Go up the right ramp to start the event in the therapist's room.", true);
            }
        }
        else
        {
            activatedTriggers--;
            if (activatedTriggers < QuestTriggers.Count)
            {
                Arrow.gameObject.SetActive(false);
                GameManager.Instance.Border.RemoveObjectiveText("Go up the right ramp to start the event in the therapist's room.");
            }
        }
    }

    internal void ResetQuestTriggers()
    {
        activatedTriggers = 0;
    }

    public void TryStartQuest()
    {
        if (Arrow.gameObject.activeSelf == true && !GameManager.Instance.activeQuest)
        {
            GameManager.Instance.ActivateQuestThree();
            Arrow.gameObject.SetActive(false);
            OrderlyFight.Activate();
        }
    }

    public void QuestCompleted()
    {
        GameManager.Instance.DeactivateQuestThree();
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

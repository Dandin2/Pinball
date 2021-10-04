using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public string QuestIndicatorText;
    public string QuestActiveText;

    public SpriteRenderer Arrow;
    public List<Trigger> QuestTriggers;
    public List<Bumper> Bumpers;
    public GameObject TablePrefab;

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
                GameManager.Instance.Border.SetObjectiveText("Go up the left ramp to start the event in the dining hall.", true);
            }
        }
        else
        {
            activatedTriggers--;
            if (activatedTriggers < QuestTriggers.Count)
            {
                Arrow.gameObject.SetActive(false);
                GameManager.Instance.Border.RemoveObjectiveText("Go up the left ramp to start the event in the dining hall.");
            }
        }
    }

    public void TryStartQuest()
    {
        if (Arrow.gameObject.activeSelf == true && !GameManager.Instance.activeQuest)
        {
            GameManager.Instance.ActivateQuestOne();
            foreach (Bumper b in Bumpers)
            {
                b.gameObject.SetActive(false);
                GameObject table = Instantiate(TablePrefab);
                table.transform.position = b.transform.position;
                tables.Add(table);
            }
            Arrow.gameObject.SetActive(false);
        }
    }

    public void ResetBumpers(bool questCompleted)
    {
        foreach (Bumper b in Bumpers)
            b.gameObject.SetActive(true);

        foreach (GameObject go in tables)
            Destroy(go);

        tables.Clear();
    }
}

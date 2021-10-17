using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal enum GameMode
{
    Story = 0,
    FreePlay = 1,
    TimeAttack = 2
}
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public AreaOne AreaOne;
    public AreaTwo AreaTwo;
    public AreaThree AreaThree;
    public Border Border;

    public List<Quest> completedQuests = new List<Quest>();
    internal GameMode gameMode = GameMode.FreePlay;

    public bool activeQuest = false;

	private int totalPoints = 0;
    private int level = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAreaOne(AreaOne ao)
    {
        AreaOne = ao;
    }

    public void SetAreaTwo(AreaTwo atw)
    {
        AreaTwo = atw;
    }

    public void SetAreaThree(AreaThree at)
    {
        AreaThree = at;
    }

    public void SetBorder(Border b)
    {
        Border = b;
    }
    
    public void SetPoints(int points)
    {
        totalPoints = points;
    }

    public void UpdatePoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
        Border.SetScoreText(totalPoints);
    }

    public int GetPoints()
	{
        return totalPoints;
	}

    public void ActivateQuestOne()
    {
        //      var area = GameObject.Find("AreaOne");
        //      var bumpers = area.GetComponentsInChildren<Bumper>();
        //      foreach(var bumper in bumpers)
        //{
        //          bumper.gameObject.SetActive(false);
        //}
        activeQuest = true;
        GameObject.Find("AreaTwo").GetComponent<AreaTwo>().Deactivate();
        GameObject.Find("AreaThree").GetComponent<AreaThree>().Deactivate();
        Border.StartQuest(Quest.DiningHall);
        Border.SetOrderlyText(2, "Shh. Sleep now.");
    }

    public void ActivateQuestTwo()
    {
        //var area = GameObject.Find("AreaTwo");
        //var bumpers = area.GetComponentsInChildren<Bumper>();
        //foreach (var bumper in bumpers)
        //{
        //    bumper.gameObject.SetActive(false);
        //}
        activeQuest = true;
        GameObject.Find("AreaOne").GetComponent<AreaOne>().Deactivate();
        GameObject.Find("AreaThree").GetComponent<AreaThree>().Deactivate();
        Border.StartQuest(Quest.LivingQuarters);
        Border.SetOrderlyText(1, "Calm down before I calm you down.");
    }

    public void ActivateQuestThree()
    {
        //var area = GameObject.Find("AreaThree");
        //var bumpers = area.GetComponentsInChildren<Bumper>();
        //foreach (var bumper in bumpers)
        //{
        //    bumper.gameObject.SetActive(false);
        //}
        activeQuest = true;
        GameObject.Find("AreaTwo").GetComponent<AreaTwo>().Deactivate();
        GameObject.Find("AreaOne").GetComponent<AreaOne>().Deactivate();
        Border.StartQuest(Quest.SocialArea);
        Border.SetOrderlyText(3, "Why don't we talk about your wife?");
    }



    internal void ResetGame()
    {
        this.totalPoints = 0;
        LivesManager.Instance.ResetLives();
    }



    public void DeactivateQuestOne()
    {
        var area = GameObject.Find("AreaOne");
        if (!completedQuests.Contains(Quest.LivingQuarters))
        {
            var go = GameObject.Find("AreaTwo").GetComponent<AreaTwo>();
            go.Activate();
            go.ResetQuestTriggers();

        }
        if (!completedQuests.Contains(Quest.SocialArea))
        {
            var go = GameObject.Find("AreaThree").GetComponent<AreaThree>();
            go.Activate();
            go.ResetQuestTriggers();
        }

        GameObject.Find("AreaOne").GetComponent<AreaOne>().Deactivate();
        var bumpers = area.GetComponentsInChildren<Bumper>(true);
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(true);
        }
        completedQuests.Add(Quest.DiningHall);
        Border.QuestCompleted(Quest.DiningHall);
        TableManager.Instance.CrackRing();

        activeQuest = false;
        if(completedQuests.Count == 3)
        {
            GoToNextTable();
        }
    }

    public void DeactivateQuestTwo()
    {
        var area = GameObject.Find("AreaTwo");
        if (!completedQuests.Contains(Quest.DiningHall))
        {
            var go = GameObject.Find("AreaOne").GetComponent<AreaOne>();
            go.Activate();
            go.ResetQuestTriggers();
        }
        if (!completedQuests.Contains(Quest.SocialArea))
        {
            var go = GameObject.Find("AreaThree").GetComponent<AreaThree>();
            go.Activate();
            go.ResetQuestTriggers();
        }

        GameObject.Find("AreaTwo").GetComponent<AreaTwo>().Deactivate();
        var bumpers = area.GetComponentsInChildren<Bumper>(true);
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(true);
        }
        completedQuests.Add(Quest.LivingQuarters);
        Border.QuestCompleted(Quest.LivingQuarters);
        TableManager.Instance.CrackRing();

        activeQuest = false;
        if (completedQuests.Count == 3)
        {
            GoToNextTable();
        }
    }

    public void DeactivateQuestThree()
    {
        var area = GameObject.Find("AreaThree");
        if (!completedQuests.Contains(Quest.DiningHall))
        {
            var go = GameObject.Find("AreaOne").GetComponent<AreaOne>();
            go.Activate();
            go.ResetQuestTriggers();
        }
        if (!completedQuests.Contains(Quest.LivingQuarters))
        { 
            var go = GameObject.Find("AreaTwo").GetComponent<AreaTwo>();
            go.Activate();
            go.ResetQuestTriggers();
        }

        GameObject.Find("AreaThree").GetComponent<AreaThree>().Deactivate();
        var bumpers = area.GetComponentsInChildren<Bumper>(true);
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(true);
        }
        completedQuests.Add(Quest.SocialArea);
        Border.QuestCompleted(Quest.SocialArea);
        TableManager.Instance.CrackRing();

        activeQuest = false;
        if (completedQuests.Count == 3)
        {
            GoToNextTable();
        }
    }


    public void StartNextLevel()
    {
        level++;
        if (level == 1)
        {
            NextTableInitialize();
        }
        else
            FinalTableInitialize();
    }

    public void GoToNextTable()
    {
        TableManager.Instance.ActivateTransition(1);
    }

    public void NextTableInitialize()
    {
        Border.SetActTwo();
    }

    public void GoToFinalTable()
    {
        TableManager.Instance.ActivateTransition(2);
    }


    public void FinalTableInitialize()
    {
        Border.SetActThree();
    }

    public void Victory()
    {
        TableManager.Instance.ShowVictoryScreenThenQuit();
    }


    public void StartTimeAttack()
    {
        StartCoroutine(WaitThenStartTimeAttack());
    }

    public void EndTimeAttack()
    {
        Border.EndTimer();
    }

    private IEnumerator WaitThenStartTimeAttack()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(TableManager.Instance.DoTheCountdown(null));
        Border.StartTimer();
        TableManager.Instance.Ball.UnSuspend();

        yield break;
    }
}

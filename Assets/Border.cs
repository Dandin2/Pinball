using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Border : MonoBehaviour
{
    public Text OrderlyOneText;
    public Text OrderlyTwoText;
    public Text OrderlyThreeText;
    public Text ObjectiveText;
    public Text ScoreText;

    public GameObject o1Fade;
    public GameObject o2Fade;
    public GameObject o3Fade;

    private List<Quest> completedQuests = new List<Quest>(); //probably could go in GameManager but fuck it.

    private void Awake()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetBorder(this);
        else
            StartCoroutine(DeleteMe());
    }

    private IEnumerator DeleteMe()
    {
        yield return new WaitForSeconds(0.4f);
        GameManager.Instance.SetBorder(this);
        yield break;
    }


    public void SetScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void SetOrderlyText(int orderly, string text)
    {
        if (orderly == 1)
            OrderlyOneText.text = text;
        else if (orderly == 2)
            OrderlyTwoText.text = text;
        else if (orderly == 3)
            OrderlyThreeText.text = text;
    }

    public void SetObjectiveText(string text, bool addToCurrentText)
    {
        if (addToCurrentText)
            ObjectiveText.text += Environment.NewLine + text;
        else
            ObjectiveText.text = text;
    }

    public void RemoveObjectiveText(string text)
    {
        ObjectiveText.text = ObjectiveText.text.Replace(text, string.Empty);
    }

    public void QuestCompleted(Quest completed)
    {
        completedQuests.Add(completed);

        o1Fade.SetActive(completedQuests.Contains(Quest.LivingQuarters));
        o2Fade.SetActive(completedQuests.Contains(Quest.SocialArea));
        o3Fade.SetActive(completedQuests.Contains(Quest.DiningHall));
    }

    public void StartQuest(Quest toStart)
    {
        if(toStart == Quest.LivingQuarters)
        {
            o1Fade.SetActive(false);
            o2Fade.SetActive(true);
            o3Fade.SetActive(true);
            ObjectiveText.text = "Have a scuffle with the nurse!";
        }
        else if (toStart == Quest.SocialArea)
        {
            o1Fade.SetActive(true);
            o2Fade.SetActive(true);
            o3Fade.SetActive(false);
            ObjectiveText.text = "Get that television remote from the orderly!";
        }
        else if (toStart == Quest.DiningHall)
        {
            o1Fade.SetActive(true);
            o2Fade.SetActive(false);
            o3Fade.SetActive(true);
            ObjectiveText.text = "Participate in the food fight!";
        }

    }
}

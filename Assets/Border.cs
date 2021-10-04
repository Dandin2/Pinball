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
    private string baseOrderly1Text = "";
    private string baseOrderly2Text = "";
    private string baseOrderly3Text = "";

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetBorder(this);
            GameManager.Instance.SetPoints(0);
        }
        else
        {
            StartCoroutine(DeleteMe());
        }
        SetScoreText(0);
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

    public void SetOrderlyText(int orderly, string text, float time = 0)
    {
        if (orderly == 1)
        {
            OrderlyOneText.text = text;
            if (time > 0)
            {
                StopAllCoroutines();
                StartCoroutine(WaitThenClearText(OrderlyOneText, time, baseOrderly1Text));
            }
            else
                baseOrderly1Text = text;
        }
        else if (orderly == 2)
        {
            OrderlyTwoText.text = text;
            if (time > 0)
            {
                StopAllCoroutines();
                StartCoroutine(WaitThenClearText(OrderlyTwoText, time, baseOrderly2Text));
            }
            else
                baseOrderly2Text = text;
        }
        else if (orderly == 3)
        {
            OrderlyThreeText.text = text;
            if (time > 0)
            {
                StopAllCoroutines();
                StartCoroutine(WaitThenClearText(OrderlyThreeText, time, baseOrderly3Text));
            }
            else
                baseOrderly3Text = text;
        }
    }

    private IEnumerator WaitThenClearText(Text toSetAndClear, float time, string initialText)
    {
        yield return new WaitForSeconds(time);
        toSetAndClear.text = initialText;
        yield break;
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
        StopAllCoroutines();
        completedQuests.Add(completed);
        OrderlyOneText.text = string.Empty;
        OrderlyTwoText.text = string.Empty;
        OrderlyThreeText.text = string.Empty;
        ObjectiveText.text = string.Empty;

        o1Fade.SetActive(completedQuests.Contains(Quest.LivingQuarters));
        o2Fade.SetActive(completedQuests.Contains(Quest.DiningHall));
        o3Fade.SetActive(completedQuests.Contains(Quest.SocialArea));
    }

    public void StartQuest(Quest toStart)
    {
        if (toStart == Quest.LivingQuarters)
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
            ObjectiveText.text = "Have a scuffle with the therapist!";
        }
        else if (toStart == Quest.DiningHall)
        {
            o1Fade.SetActive(true);
            o2Fade.SetActive(false);
            o3Fade.SetActive(true);
            ObjectiveText.text = "Have a scuffle with the doctor!";
        }

    }
}

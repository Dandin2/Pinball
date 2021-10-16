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
    public Text TimeText;

    public GameObject o1Fade;
    public GameObject o2Fade;
    public GameObject o3Fade;

    private List<Quest> completedQuests = new List<Quest>(); //probably could go in GameManager but fuck it.
    private string baseOrderly1Text = "";
    private string baseOrderly2Text = "";
    private string baseOrderly3Text = "";

    private float elapsedTime = 0f;
    private Coroutine OrderlyOne;
    private Coroutine OrderlyTwo;
    private Coroutine OrderlyThree;
    private Coroutine Timer;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetBorder(this);
            if(GameManager.Instance.gameMode == GameMode.TimeAttack)
            {
                ScoreText.transform.parent.gameObject.SetActive(false);
                TimeText.transform.parent.gameObject.SetActive(true);
                TimeText.text = TimeSpan.FromSeconds(0).ToString("mm':'ss':'ff");
            }
            else
            {
                ScoreText.transform.parent.gameObject.SetActive(true);
                TimeText.transform.parent.gameObject.SetActive(false);
                GameManager.Instance.SetPoints(0);
            }
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

    public void StartTimer()
    {
        Timer = StartCoroutine(TimerGo());
    }

    private IEnumerator TimerGo()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            TimeText.text = TimeSpan.FromSeconds(elapsedTime).ToString("mm':'ss':'ff");
        }
    }

    public void EndTimer()
    {
        StopCoroutine(Timer);
    }

    public void ResetTimer()
    {
        elapsedTime = 0;
    }

    public void SetOrderlyText(int orderly, string text, float time = 0)
    {
        if (orderly == 1)
        {
            OrderlyOneText.text = text;
            if (time > 0)
            {
                if (OrderlyOne != null)
                    StopCoroutine(OrderlyOne);

                OrderlyOne = StartCoroutine(WaitThenClearText(OrderlyOneText, time, baseOrderly1Text));
            }
            else
                baseOrderly1Text = text;
        }
        else if (orderly == 2)
        {
            OrderlyTwoText.text = text;
            if (time > 0)
            {
                if (OrderlyTwo != null)
                    StopCoroutine(OrderlyTwo);

                OrderlyTwo = StartCoroutine(WaitThenClearText(OrderlyTwoText, time, baseOrderly2Text));
            }
            else
                baseOrderly2Text = text;
        }
        else if (orderly == 3)
        {
            OrderlyThreeText.text = text;
            if (time > 0)
            {
                if (OrderlyThree != null)
                    StopCoroutine(OrderlyThree);

                OrderlyThree = StartCoroutine(WaitThenClearText(OrderlyThreeText, time, baseOrderly3Text));
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
            ObjectiveText.text = "Have a scuffle with the nurse!" + Environment.NewLine + Environment.NewLine + "Destroy the 3 white targets.";
        }
        else if (toStart == Quest.SocialArea)
        {
            o1Fade.SetActive(true);
            o2Fade.SetActive(true);
            o3Fade.SetActive(false);
            ObjectiveText.text = "Have a scuffle with the therapist!" + Environment.NewLine + Environment.NewLine + "Destroy the 3 white targets.";
        }
        else if (toStart == Quest.DiningHall)
        {
            o1Fade.SetActive(true);
            o2Fade.SetActive(false);
            o3Fade.SetActive(true);
            ObjectiveText.text = "Have a scuffle with the doctor!" + Environment.NewLine + Environment.NewLine + "Destroy the 3 white targets.";
        }

    }


    public void FadeOut()
    {
        gameObject.SetActive(false);
    }

    public void SetActTwo()
    {
        SetObjectiveText("RAGE!  Break all the tables!", false);
        o1Fade.SetActive(true);
        o2Fade.SetActive(true);
        o3Fade.SetActive(true);
        o1Fade.GetComponent<SpriteRenderer>().color = new Color(.4f, 0, 0, .5f);
        o2Fade.GetComponent<SpriteRenderer>().color = new Color(.4f, 0, 0, .5f);
        o3Fade.GetComponent<SpriteRenderer>().color = new Color(.4f, 0, 0, .5f);
    }

    public void SetActThree()
    {
        SetObjectiveText("Defeat your inner demons!", false);
        SetOrderlyText(1, "...");
        SetOrderlyText(2, "...");
        SetOrderlyText(3, "...");
        o1Fade.GetComponent<SpriteRenderer>().color = new Color(.6f, 0, 0, .7f);
        o2Fade.GetComponent<SpriteRenderer>().color = new Color(.6f, 0, 0, .7f);
        o3Fade.GetComponent<SpriteRenderer>().color = new Color(.6f, 0, 0, .7f);
    }

}

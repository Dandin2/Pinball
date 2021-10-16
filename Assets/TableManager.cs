using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    public LevelTransition Transition1;
    public LevelTransition Transition2;
    public GameObject VictoryScreen;
    public Ring Ring;
    public Syringes Syringes;
    public Ball Ball;
    public Text CountdownText;

    public static TableManager Instance = null;

    public int activeTable = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if(GameManager.Instance.gameMode == GameMode.TimeAttack)
            {
                Transition1.DoStuffInstantly();
                Ball.transform.position = new Vector3(0, 0, Ball.transform.position.z);
                Ball.Suspend();
                GameManager.Instance.StartTimeAttack();
            }
        }
    }

    public void ShowVictoryScreenThenQuit()
    {
        GameManager.Instance.Border.FadeOut();
        VictoryScreen.SetActive(true);
        VictoryScreen.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        float current = 0;

        while (current < 2)
        {
            current += Time.deltaTime;
            VictoryScreen.GetComponentInChildren<Image>().color = new Color(0, 0, 0, current * 0.5f);
        }
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("TitleScreen");
    }

    public void ActivateTransition(int level)
    {
        if (level == 1)
            Transition1.gameObject.SetActive(true);
        else if (level == 2)
            Transition2.gameObject.SetActive(true);

        GameManager.Instance.Border.SetObjectiveText("Descend", false);
    }

    public void CrackRing()
    {
        Ring.GoToNextSprite();
        Syringes.ActivateNextSyringe();
    }

    public IEnumerator DoTheCountdown(Action endAction)
    {
        CountdownText.gameObject.transform.parent.gameObject.SetActive(true);
        CountdownText.text = "3";

        Vector2 oSize = CountdownText.rectTransform.sizeDelta;
        Color oC = new Color(CountdownText.color.r, CountdownText.color.g, CountdownText.color.b, 1);
        float alp = 1;
        while(CountdownText.color.a >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            CountdownText.color = new Color(oC.r, oC.g, oC.b, alp);
            alp -= 0.1f;
            CountdownText.rectTransform.sizeDelta = oSize * alp;
        }

        CountdownText.text = "2";
        alp = 1;
        CountdownText.color = oC;
        CountdownText.rectTransform.sizeDelta = oSize;

        while (CountdownText.color.a >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            CountdownText.color = new Color(oC.r, oC.g, oC.b, alp);
            alp -= 0.1f;
            CountdownText.rectTransform.sizeDelta = oSize * alp;
        }

        CountdownText.text = "1";
        alp = 1;
        CountdownText.color = oC;
        CountdownText.rectTransform.sizeDelta = oSize;

        while (CountdownText.color.a >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            CountdownText.color = new Color(oC.r, oC.g, oC.b, alp);
            alp -= 0.1f;
            CountdownText.rectTransform.sizeDelta = oSize * alp;
        }

        CountdownText.text = "GO!";
        alp = 1;
        CountdownText.color = oC;
        CountdownText.rectTransform.sizeDelta = oSize;

        while (CountdownText.color.a >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            CountdownText.color = new Color(oC.r, oC.g, oC.b, alp);
            alp -= 0.1f;
            CountdownText.rectTransform.sizeDelta = oSize * alp;
        }

        CountdownText.gameObject.transform.parent.gameObject.SetActive(false);
        endAction?.Invoke();
        yield break;
    }

}

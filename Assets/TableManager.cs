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

    public static TableManager Instance = null;

    public int activeTable = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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
        yield return new WaitForSeconds(2);
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

}

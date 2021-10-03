using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public AreaOne AreaOne;
    public AreaTwo AreaTwo;
    public AreaThree AreaThree;
    public Border Border;

    private int totalPoints = 0;
    private int livesUsed = 0;

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


    public void UpdatePoints(int pointsToAdd)
	{
        totalPoints += pointsToAdd;
    }
    public void UseLife()
    {
        livesUsed++;
    }

    public void ActivateQuestOne()
	{
  //      var area = GameObject.Find("AreaOne");
  //      var bumpers = area.GetComponentsInChildren<Bumper>();
  //      foreach(var bumper in bumpers)
		//{
  //          bumper.gameObject.SetActive(false);
		//}
        GameObject.Find("AreaTwo").GetComponent<AreaTwo>().Deactivate();
        GameObject.Find("AreaThree").GetComponent<AreaThree>().Deactivate();
        Border.StartQuest(Quest.DiningHall);
        Border.SetOrderlyText(2, "Food FIGHT!!!!");
    }

    public void ActivateQuestTwo()
    {
        var area = GameObject.Find("AreaTwo");
        var bumpers = area.GetComponentsInChildren<Bumper>();
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(false);
        }
        GameObject.Find("AreaOne").GetComponent<AreaOne>().Deactivate();
        GameObject.Find("AreaThree").GetComponent<AreaThree>().Deactivate();
        Border.StartQuest(Quest.LivingQuarters);
        Border.SetOrderlyText(1, "Time for bed!   NOW!");
    }

    public void ActivateQuestThree()
    {
        var area = GameObject.Find("AreaThree");
        var bumpers = area.GetComponentsInChildren<Bumper>();
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(false);
        }
        GameObject.Find("AreaTwo").GetComponent<AreaTwo>().Deactivate();
        GameObject.Find("AreaOne").GetComponent<AreaOne>().Deactivate();
        Border.StartQuest(Quest.SocialArea);
        Border.SetOrderlyText(3, "The television STAYS on channel 4!");
    }

    public void DeactivateQuestOne()
    {
        var area = GameObject.Find("AreaOne");
        var bumpers = area.GetComponentsInChildren<Bumper>(true);
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(true);
        }
    }

    public void DeactivateQuestTwo()
    {
        var area = GameObject.Find("AreaTwo");
        var bumpers = area.GetComponentsInChildren<Bumper>(true);
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(true);
        }
    }

    public void DeactivateQuestThree()
    {
        var area = GameObject.Find("AreaThree");
        var bumpers = area.GetComponentsInChildren<Bumper>(true);
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameObject AreaOne;
    public GameObject AreaTwo;
    public GameObject AreaThree;

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
        var area = GameObject.Find("AreaOne");
        var bumpers = area.GetComponentsInChildren<Bumper>();
        foreach(var bumper in bumpers)
		{
            bumper.gameObject.SetActive(false);
		}
        GameObject.Find("AreaTwo").GetComponent<AreaTwo>().Deactivate();
        GameObject.Find("AreaThree").GetComponent<AreaTwo>().Deactivate();
    }

    public void ActivateQuestTwo()
    {
        var area = GameObject.Find("AreaTwo");
        var bumpers = area.GetComponentsInChildren<Bumper>();
        foreach (var bumper in bumpers)
        {
            bumper.gameObject.SetActive(false);
        }
        GameObject.Find("AreaOne").GetComponent<AreaTwo>().Deactivate();
        GameObject.Find("AreaThree").GetComponent<AreaTwo>().Deactivate();
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
        GameObject.Find("AreaOne").GetComponent<AreaTwo>().Deactivate();
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

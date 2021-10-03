using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

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
}

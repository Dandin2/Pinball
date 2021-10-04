using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public LevelTransition Transition1;
    public LevelTransition Transition2;

    public static TableManager Instance = null;

    public int activeTable = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ActivateTransition(int level)
    {
        if (level == 1)
            Transition1.SetActive(true);
        else if (level == 2)
            Transition2.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table2 : MonoBehaviour
{
    public List<BreakableObject> tables;
    private int brokenTables = 0;


    private void Awake()
    {
        foreach(BreakableObject bo in tables)
        {
            bo.breakAction = () => { OnTableBreak(); };
        }
    }

    public void OnTableBreak()
    {
        brokenTables++;
        if (brokenTables >= tables.Count)
            GameManager.Instance.GoToFinalTable();
    }
}

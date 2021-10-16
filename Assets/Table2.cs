using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table2 : MonoBehaviour
{
    public List<BreakableObject> tables;
    public List<OrderlyDialog> thingsToSayOnTableBreak;
    private int brokenTables = 0;
    private List<int> brokenTableIDs = new List<int>();

    private void Awake()
    {
        int i = 0;
        foreach(BreakableObject bo in tables)
        {
            int j = i;
            bo.breakAction = () => { OnTableBreak(j); };
            i++;
        }
    }

    public void OnTableBreak(int tableID)
    {
        if (!brokenTableIDs.Contains(tableID))
        {
            brokenTableIDs.Add(tableID);
            brokenTables++;

            if (UnityEngine.Random.value > 0.3f)
            {
                int orderlyComment = UnityEngine.Random.Range(0, thingsToSayOnTableBreak.Count);
                OrderlyDialog od = thingsToSayOnTableBreak[orderlyComment];
                string selectedDialog = od.thingsToSay[UnityEngine.Random.Range(0, od.thingsToSay.Count)];

                if (od.orderly == Quest.LivingQuarters)
                {
                    GameManager.Instance.Border.SetOrderlyText(1, selectedDialog, 3);
                }
                else if (od.orderly == Quest.DiningHall)
                {
                    GameManager.Instance.Border.SetOrderlyText(2, selectedDialog, 3);
                }
                else if (od.orderly == Quest.SocialArea)
                {
                    GameManager.Instance.Border.SetOrderlyText(3, selectedDialog, 3);
                }
            }

            if (brokenTables >= tables.Count)
            {
                if (GameManager.Instance.gameMode == GameMode.TimeAttack)
                {
                    GameManager.Instance.Border.SetOrderlyText(1, "Woo!", 3);
                    GameManager.Instance.Border.SetOrderlyText(2, "Woo!", 3);
                    GameManager.Instance.Border.SetOrderlyText(3, "Woo!", 3);
                    GameManager.Instance.EndTimeAttack();
                }
                else
                {
                    GameManager.Instance.Border.SetOrderlyText(1, "Going down?", 3);
                    GameManager.Instance.Border.SetOrderlyText(2, "Going down?", 3);
                    GameManager.Instance.Border.SetOrderlyText(3, "Going down?", 3);
                    GameManager.Instance.GoToFinalTable();
                }
            }
        }
    }
}

[Serializable]
public class OrderlyDialog
{
    public Quest orderly;
    public List<string> thingsToSay;
}
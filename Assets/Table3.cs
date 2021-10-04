using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Table3 : MonoBehaviour
{
    public List<TableRespawner> tableRespawnConditions;

    private void Awake()
    {
        foreach (TableRespawner tr in tableRespawnConditions)
        {
            tr.toHit.bounceAction = () =>
            {
                tr.numTimesHit++;
                tr.respawnThings.ForEach(x => x.BreakInstantly()); 
                DelayThenSetTablesBackUp(tr.respawnThings);
            };
        }
    }

    public void DelayThenSetTablesBackUp(List<BreakableObject> bos)
    {
        StartCoroutine(DSTBU(bos));
    }

    private IEnumerator DSTBU(List<BreakableObject> bos)
    {
        yield return new WaitForSeconds(1);
        foreach (BreakableObject bo in bos)
        {
            bo.UnBreak();
        }
    }

    public void FightCompleted()
    {
        //todo: maybe put a hole in the wall you have to shoot through
        GameManager.Instance.Victory();
    }
}

[Serializable]
public class TableRespawner
{
    public Bumper toHit;
    public List<BreakableObject> respawnThings;
    [HideInInspector]
    public int numTimesHit = 0;
}

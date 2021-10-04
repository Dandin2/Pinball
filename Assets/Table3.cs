using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Table3 : MonoBehaviour
{
    public List<TableRespawner> tableRespawnConditions;
    public GameObject arrow;
    public GameObject exit;
    public List<GameObject> enableWhenDone;

    private int defeatedLimbs = 0;

    private void Awake()
    {
        foreach (TableRespawner tr in tableRespawnConditions)
        {
            tr.toHit.bounceAction = () =>
            {
                tr.numTimesHit++;
                if(tr.numTimesHit == 3)
                {
                    defeatedLimbs++;
                    SetOrderlyDialog();
                }
                tr.respawnThings.ForEach(x => x.BreakInstantly()); 
                DelayThenSetTablesBackUp(tr.respawnThings);
            };
        }
    }

    private void SetOrderlyDialog()
    {
        if (defeatedLimbs == 1)
        {
            GameManager.Instance.Border.SetOrderlyText(1, "Asleep now?  Good.  I can finally get some rest.");
            GameManager.Instance.Border.o1Fade.GetComponent<SpriteRenderer>().color = new Color(.2f, .2f, .2f, .2f);
        }
        else if (defeatedLimbs == 2)
        {
            GameManager.Instance.Border.SetOrderlyText(2, "Down.  Yes.  Forget.  Down... Good...");
            GameManager.Instance.Border.o2Fade.GetComponent<SpriteRenderer>().color = new Color(.2f, .2f, .2f, .2f);
        }
        else if (defeatedLimbs == 3)
        {
            GameManager.Instance.Border.SetOrderlyText(3, "Patient seems to finally be making some progress.");
            GameManager.Instance.Border.o3Fade.GetComponent<SpriteRenderer>().color = new Color(.2f, .2f, .2f, .2f);
        }
        else if (defeatedLimbs == 4)
        {
            GameManager.Instance.Border.o1Fade.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            GameManager.Instance.Border.o2Fade.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            GameManager.Instance.Border.o3Fade.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            GameManager.Instance.Border.SetOrderlyText(1, "Rest does good for the soul.");
            GameManager.Instance.Border.SetOrderlyText(2, "Better... Down... Forget... Sleep.");
            GameManager.Instance.Border.SetOrderlyText(3, "Maybe there is some hope after all.");
            GameManager.Instance.Border.SetObjectiveText("Be free.", false);
            arrow.SetActive(true);
            FightCompleted();
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
        exit.SetActive(true);
        exit.GetComponent<SpriteRenderer>().enabled = false;
        exit.GetComponent<PolygonCollider2D>().enabled = false;
        foreach (Transform t in exit.transform)
            t.gameObject.SetActive(true);

        StopAllCoroutines();
        foreach (TableRespawner tr in tableRespawnConditions)
        {
            foreach(BreakableObject bo in tr.respawnThings)
            {
                bo.BreakInstantly();
            }
        }

        arrow.SetActive(true);
        foreach(GameObject go in enableWhenDone)
        {
            go.SetActive(true);
        }
    }

    public void Victory()
    {
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringes : MonoBehaviour
{
    public List<Syringe> syringes;

    private int count = 0;
    public void ActivateNextSyringe()
    {
        if (count < syringes.Count)
        {
            syringes[count].full.SetActive(false);
            syringes[count].empty.SetActive(true);
            count++;
        }
    }
}

[Serializable]
public class Syringe
{
    public GameObject full;
    public GameObject empty;
}

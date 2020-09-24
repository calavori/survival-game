using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Plant
{
    Timer timer;

    public float AmountRemaining
    {
        get
        {
            return growth;
        }
    }
}

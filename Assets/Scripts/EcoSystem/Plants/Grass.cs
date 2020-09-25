using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Plant
{
    public float Consume(float amount)
    {
        float amountConsumed = Mathf.Max(0, Mathf.Min(growth, amount));
        growth -= amount;

        if (growth <= 0)
        {
            // Die
            print("die");
        }
        return amountConsumed;
    }
}

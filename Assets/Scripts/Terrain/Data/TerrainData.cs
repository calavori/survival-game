using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : UpdatetableData
{
    public float uniformScale = 5f;
    public float meshHeightMutiplier;
    public AnimationCurve meshHeightCurve;
    public bool useFallOff;

    // get min height of map
    public float minHeight
    {
        get
        {
            return meshHeightMutiplier * meshHeightCurve.Evaluate(0) * uniformScale;
        }
    }

    // get max height of map
    public float maxHeight
    {
        get
        {
            return meshHeightMutiplier * meshHeightCurve.Evaluate(1) * uniformScale;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : UpdatetableData
{
    public float meshHeightMutiplier;
    public AnimationCurve meshHeightCurve;
    public bool useFallOff;
}

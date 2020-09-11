using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    static MapGenerator mapGenerator;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        mapGenerator.DrawMapInEditor();
    }
}

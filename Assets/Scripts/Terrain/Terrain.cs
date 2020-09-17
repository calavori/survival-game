using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    public float landHeight;

    static MapGenerator mapGenerator;
    Mesh mesh;

    void Awake()
    {
        // Generate map
        mapGenerator = FindObjectOfType<MapGenerator>();
        mapGenerator.Generate();

        // Get mesh of map
        GameObject mapMesh = GameObject.Find("Map Mesh");

        // Get coord of 4 bound of map
        MeshFilter meshFilter = mapMesh.GetComponent<MeshFilter>();
        Bounds bounds = meshFilter.sharedMesh.bounds;

        minX = (bounds.center.x - bounds.extents.x) * 10;
        minZ = (bounds.center.z - bounds.extents.z) * 10;
        maxX = (bounds.center.x + bounds.extents.x) * 10;
        maxZ = (bounds.center.z + bounds.extents.z) * 10;
    }
}

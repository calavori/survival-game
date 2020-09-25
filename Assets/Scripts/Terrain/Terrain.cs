using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public TerrainDetail terrainDetail;

    void Awake()
    {
        // Generate map
        MapGenerator mapGenerator  = FindObjectOfType<MapGenerator>();
        mapGenerator.Generate();

        // Get mesh of map
        GameObject mapMesh = GameObject.Find("Map Mesh");
        terrainDetail = new TerrainDetail(mapMesh);
    }
}

public class TerrainDetail
{
    public readonly float minX;
    public readonly float minZ;
    public readonly float maxX;
    public readonly float maxZ;

    MeshCollider collider;
    RaycastHit hit;

    public TerrainDetail(GameObject mesh)
    {
        // Get collider
        collider = mesh.GetComponent<MeshCollider>();

        // Get coord of 4 bound of map
        Bounds bounds = collider.sharedMesh.bounds;
        float minX = (bounds.center.x - bounds.extents.x) * 10;
        float minZ = (bounds.center.z - bounds.extents.z) * 10;
        float maxX = (bounds.center.x + bounds.extents.x) * 10;
        float maxZ = (bounds.center.z + bounds.extents.z) * 10;

        // Get material of map to get height of each regions
        MeshRenderer meshRenderer = mesh.GetComponent<MeshRenderer>();
        Material material = meshRenderer.material;
    }

    // Get coord of y by x and z
    public Vector3 GetCoord(float x, float z)
    {
        Ray ray = new Ray(new Vector3(x, 50, z), Vector3.down);
        if (collider.Raycast(ray, out hit, 2.0f * 50f) && hit.transform.tag == "ground")
        {
            return hit.point;
        }
        else
        {
            return new Vector3(-1,-1,-1);
        }
    }
}

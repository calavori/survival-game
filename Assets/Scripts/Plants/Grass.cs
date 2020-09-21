using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grass : MonoBehaviour
{
    // Max scale size of grass
    [SerializeField] float maxScaleSize;
    // time to max growth in second
    [SerializeField] float maxGrowTime;

    // Time to reproduct in second
    [SerializeField] float reproductTime;
    // Reproduct radius
    [SerializeField] float reproductRadius;

    // Distance between each grass
    [SerializeField] float distance;

    // Get object that has need Components
    [SerializeField] GameObject grassPrefab;

    // Can be seen as how many percent it has grow
    [Range(0, 1)]
    public float growth;
    [Range(0, 1)]
    [SerializeField] float reproduction;

    Timer timer;
    TerrainDetail terrainDetail;

    // grow speed in second
    private float growSpeed
    {
        get
        {
            return 1 / maxGrowTime;

        }
    }

    // reproduct speed in second
    private float reproductSpeed
    {
        get
        {
            return 1 / reproductTime;

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Get need component
        timer = GameObject.Find("Sky").GetComponent<Timer>();
        terrainDetail = GameObject.Find("Map generator").GetComponent<Terrain>().terrainDetail;
    }

    // Update is called once per frame
    void Update()
    {
        GrowUp();
        Reproduct();
    }

    void GrowUp()
    {
        if (growth <= 1f)
        {
            growth += growSpeed * timer.lastFrameTime;
            float scaleSize = maxScaleSize * growth;
            transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
        }
    }

    void Reproduct()
    {
        if (growth >= 1 && reproduction <= 1f)
        {
            reproduction += reproductSpeed * timer.lastFrameTime;
        }
        if (reproduction >= 1f)
        {
            reproduction = 0f;
            SpawnNewGrass();            
        }
    }

    void SpawnNewGrass()
    {
        // Todo Control number
        // Todo Check enviroment to see if any object in that position
        float xSpawn = transform.localPosition.x + Random.Range(distance, reproductRadius) * ((Random.Range(0, 2) == 0) ? 1 : -1);
        float zSpawn = transform.localPosition.z + Random.Range(distance, reproductRadius) * ((Random.Range(0, 2) == 0) ? 1 : -1);
        GameObject newGrass = Instantiate(grassPrefab, terrainDetail.GetCoord(xSpawn, zSpawn), Quaternion.identity);
        newGrass.GetComponent<Grass>().growth = 0;
    }
}
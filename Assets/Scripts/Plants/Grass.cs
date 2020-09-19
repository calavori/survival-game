using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grass : MonoBehaviour
{
    // Min scale size of grass
    public float minScaleSize;
    // Max scale size of grass
    public float maxScaleSize;
    // time to max growth in second
    public float maxGrowTime;

    // Time to reproduct in second
    public float reproductTime;

    // Can be seen as how many percent it has grow
    [Range(0,1)]
    public float growth;
    [Range(0,1)]
    public float reproduction;

    Timer timer;

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
        timer = GameObject.Find("Sky").GetComponent<Timer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GrowUp();
        Reproduct();
    }

    void GrowUp()
    {   if (growth <= 1f)
        {
            growth += growSpeed * (float)timer.lastFrameTime;
            float scaleSize = (maxScaleSize - minScaleSize) * growth;
            transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
        }
    }

    void Reproduct()
    {
        if (growth >= 1 && reproduction <= 1f)
        {
            reproduction += reproductSpeed * (float)timer.lastFrameTime;
        }
        if (reproduction >= 1f)
        {
            reproduction = 0f;
            print("give birth");
        }
    }
}

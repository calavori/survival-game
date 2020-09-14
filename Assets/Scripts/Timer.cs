using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Timer : MonoBehaviour
{
    public static float startTime;
    [Range(1, 10)]
    public float timeAccel;
    public float timer;

    float lastFrameTime
    {
        get
        {
            return Time.deltaTime* timeAccel;
        }
    }
    float seconds
    {
        get
        {
            return timer % 60;
        }
    }

    float days
    {
        get
        {
            return seconds % 86400;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Todo Load start time in save file
        timer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            timer += lastFrameTime;
        }
    }

}

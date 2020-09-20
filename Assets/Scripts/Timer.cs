using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Timer : MonoBehaviour
{
    public float timeAccel;

    // Time.delta after accelerate
    public float lastFrameTime
    {
        get
        {
            return Time.deltaTime * timeAccel;
        }
    }

    public float timePercent
    {
        get
        {
            return (oneDayToSecond - dayTimer) / oneDayToSecond;
        }
    }


    public float dayTimer;
    public float daysCount;

    public enum DayState { Day, Night };
    public DayState dayState
    {
        get
        {
            if (dayTimer <= 64800 && dayTimer > 21600)
            {
                return DayState.Day;
            }
            else
            {
                return DayState.Night;
            }
        }
    }

    private const float oneDayToSecond = 86400;         

    // Start is called before the first frame update
    void Start()
    {
        // Todo Load start time in save file
        // If new game, game start at 12h at noon
        if (daysCount == 0 && dayTimer == 0)
        {
            dayTimer = oneDayToSecond / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            // Count down timer
            dayTimer -= lastFrameTime;

            // Reset day timer and days count after a day
            if (dayTimer <= 0)
            {
                dayTimer = oneDayToSecond;
                daysCount += 1;
            }
        }
    }
}

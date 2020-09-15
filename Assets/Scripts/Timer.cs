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
            return Time.deltaTime / timeAccel;
        }
    }


    public float dayNightTimer;
    public float oneDaysTimer;
    public float daysCount;

    public enum DayState { Day, Night };
    public DayState dayState;

    private const float _FramesUntilEnd12h = 43200 * 60;         // Count down second to end 12h
    private const float _FramesUntilEndDay = 86400 * 60;         // Count down second to end a day = 24h

    // Start is called before the first frame update
    void Start()
    {
        // Todo Load start time in save file
        // If new game, game start at 12h at noon
        if (dayNightTimer == 0 && daysCount == 0 && oneDaysTimer == 0)
        {
            dayState = DayState.Day;
            dayNightTimer = _FramesUntilEnd12h;
            oneDaysTimer = _FramesUntilEnd12h;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            // If the first day in new game
            if (daysCount == 0)
            {
                StartTimersInFirstDay();
            }
            // If other than first day
            else
            {
                RunTimersInNormal();
            }
        }
    }

    void StartTimersInFirstDay()
    {
        oneDaysTimer -= (1 * timeAccel);
        
        // When over 18h in game
        if (oneDaysTimer <= _FramesUntilEnd12h / 2)
        {
            // Switch on night state at 18h
            if (dayState == DayState.Day)
            {
                dayState = DayState.Night;
            }
            // Start the day/night timer at 18h
            dayNightTimer -= (1 * timeAccel);
        }

        // When 24 night in game
        if (oneDaysTimer <= 0)
        {
            daysCount = 1;
            oneDaysTimer = _FramesUntilEndDay;
        }
    }

    void RunTimersInNormal()
    {
        // Count timer
        oneDaysTimer -= (1 * timeAccel);
        dayNightTimer -= (1 * timeAccel);

        // Swith day/night state when day/night timer is run out and reset timer
        if (dayNightTimer <= 0)
        {
            dayNightTimer = _FramesUntilEnd12h;
            if (dayState == DayState.Day)
            {
                dayState = DayState.Night;
            }
            else
            {
                dayState = DayState.Day;
            }
        }

        // Reset day timer and days count after a day
        if (oneDaysTimer <= 0)
        {
            oneDaysTimer = _FramesUntilEndDay;
            daysCount += (1 * timeAccel);
        }

    }

}

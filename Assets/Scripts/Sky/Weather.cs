using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public enum WeatherState { Rain, Sun}
    public WeatherState weatherState;
    public float timeToChangeWeather;

    Timer timer;
    RainScript rainScript;


    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        rainScript = GameObject.Find("Rain").GetComponent<RainScript>();
        rainScript.enabled = false;

        // Todo load save 

        // If new game
        if (timer.dayNightTimer == 0 && timer.daysCount == 0 && timer.oneDaysTimer == 0)
        {
            // Default is sun
            weatherState = WeatherState.Sun;
            // Random time to change weather from 3d - 4d
            timeToChangeWeather = Random.Range(3 * 21600 * 60, 4 * 21600 * 60);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeToChangeWeather -= 1 * timer.timeAccel;
        if (timeToChangeWeather <= 0)
        {
            if (weatherState == WeatherState.Sun)
            {
                rainScript.enabled = true;
                weatherState = WeatherState.Rain;
                // Random time to change weather from 5h - 2d
                timeToChangeWeather = Random.Range(18000 * 60, 2* 21600 * 60);
            }
            else
            {
                rainScript.enabled = false;
                weatherState = WeatherState.Sun;
                // Random time to change weather from 1d - 6d
                timeToChangeWeather = Random.Range(21600 * 60, 6 * 21600 * 60);
            }
        }
        
    }
}

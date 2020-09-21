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

        //rainScript.RainIntensity = 0;
        rainScript.EnableWind = false;

        // Todo load save 

        // If new game
        if (timer.daysCount == 0 && timer.dayTimer == 0)
        {
            // Default is sun
            weatherState = WeatherState.Sun;
            // Random time to change weather from 3d - 4d
            timeToChangeWeather = Random.Range(3 * 21600, 4 * 21600);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Count down weather timer
        timeToChangeWeather -= timer.lastFrameTime;
        SwitchWeather();
        ApplyWeather();
    }

    void SwitchWeather()
    {
        if (timeToChangeWeather <= 0)
        {
            if (weatherState == WeatherState.Sun)
            {
                // Change weather
                weatherState = WeatherState.Rain;
                // Random time to change weather from 5h - 2d
                timeToChangeWeather = Random.Range(18000, 2 * 21600);
            }
            else
            {
                // Change weather
                weatherState = WeatherState.Sun;
                // Random time to change weather from 1d - 6d
                timeToChangeWeather = Random.Range(21600, 6 * 21600);
            }
        }
    }

    void ApplyWeather()
    {
        if (weatherState == WeatherState.Sun)
        {
            rainScript.RainIntensity = 0;
            rainScript.EnableWind = false;
        }
        else
        {
            rainScript.RainIntensity = 0.2f;
            rainScript.EnableWind = true;
        }
    }
}

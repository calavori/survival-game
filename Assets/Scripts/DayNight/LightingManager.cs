using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] Light directionalLight;
    [SerializeField] LightingPreset preset;
    [SerializeField] Material daySkyBox;
    [SerializeField] Material nightSkyBox;

    Timer timer;

    private void Start()
    {
        timer = GetComponent<Timer>();
    }


    private void Update()
    {
        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            UpdateLighting(timer.timePercent);

            // Change skybox
            if (timer.timePercent > 0.76)
            {
                RenderSettings.skybox = nightSkyBox;
            } else if (timer.timePercent > 0.24)
            {

                RenderSettings.skybox = daySkyBox;
            }
        }
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);

            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }



    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (directionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}

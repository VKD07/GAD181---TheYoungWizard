using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] Light[] environmentLights;
    [SerializeField] float lightAdjustmentRate = 250f;
    [SerializeField] float lightsMaximumIntensity = 415f;
    [SerializeField] float currentLightTime;
    [SerializeField] BossScript bossScript;
    float darknessDuration;
    bool lightisDisabled;
    private void Start()
    {
        bossScript = FindObjectOfType<BossScript>();
        darknessDuration = bossScript.darknessDuration;

    }

    void Update()
    {

        if (bossScript.lightsDisabled == true)
        {
            // light controll
            StartDarknessTime();
        }

        ControlLight();
    }

    void StartDarknessTime()
    {
        if (currentLightTime < darknessDuration)
        {
            currentLightTime += Time.deltaTime;
        }
        else
        {
            bossScript.lightsDisabled = false;
            currentLightTime = 0;
        }
    }

    void ControlLight()
    {
        if (bossScript.lightsDisabled == true)
        {
            //disable lights
            for (int i = 0; i < environmentLights.Length; i++)
            {
                if (environmentLights[i].intensity > 0)
                {
                    environmentLights[i].intensity -= lightAdjustmentRate * Time.deltaTime;
                    environmentLights[i].intensity = Mathf.Clamp(environmentLights[i].intensity, 0.0f, lightsMaximumIntensity);
                }
            }

        }
        else
        {
            //enable lights
            for (int i = 0; i < environmentLights.Length; i++)
            {
                if (environmentLights[i].intensity < lightsMaximumIntensity)
                {
                    environmentLights[i].intensity += lightAdjustmentRate * Time.deltaTime;
                    environmentLights[i].intensity = Mathf.Clamp(environmentLights[i].intensity, 0.0f, lightsMaximumIntensity);
                }
            }
        }
    }
}
       


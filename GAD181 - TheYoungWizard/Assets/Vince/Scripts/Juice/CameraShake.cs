using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance { get; private set; }

    [SerializeField] CinemachineFreeLook thirdPersonCamera;
    [SerializeField] CinemachineVirtualCamera[] virtualCamera;
    float shakeTime;
    float startingIntensity;
    float totalShakeTime;
    public bool disableLerping;
    public bool finalCutScene;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        SmoothShakeReduction();
    }

    private void SmoothShakeReduction()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (!finalCutScene)
            {
                SetShakeIntensity(Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTime / totalShakeTime)));
            }
            else if (finalCutScene && virtualCamera.Length > 0)
            {
                SetVirtualCameraShakeIntensity(Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTime / totalShakeTime)));
            }
        }
    }

    public void ShakeCamera(float time, float intensity)
    {
        for (int i = 0; i < 3; i++)
        {
            thirdPersonCamera.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        }

        startingIntensity = intensity;
        shakeTime = time;
        totalShakeTime = time;
    }

    void SetShakeIntensity(float intensity)
    {
        for (int i = 0; i < 3; i++)
        {
            thirdPersonCamera.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        }
    }

    public void SetVirtualCameraShakeIntensity(float intensity)
    {
        for (int i = 0; i < virtualCamera.Length; i++)
        {
            virtualCamera[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        }
    }

    public void ShakeVirtualCamera(float time, float intensity)
    {
        for (int i = 0; i < virtualCamera.Length; i++)
        {
            virtualCamera[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        }
        startingIntensity = intensity;
        shakeTime = time;
        totalShakeTime = time;
    }
}

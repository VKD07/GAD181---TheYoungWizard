using Cinemachine;
using Cinemachine.PostFX;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    bool settingsEnabled;
    [SerializeField] GameObject settingsUI;
    [SerializeField] GameObject controlsUI;
    [Header("Volume Settings")]
    [SerializeField] float volumeStartingValue = 0.4f;
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] Slider volumeSlider;
    [SerializeField] TextMeshProUGUI volumeTxt;
    int volumeToText;

    [Header("Sensitivty Settings")]
    [SerializeField] float sensitivtyStartingValue = 150f;
    [SerializeField] CinemachineFreeLook cinemachine;
    [SerializeField] Slider mouseSensitivtySlider;
    [SerializeField] TextMeshProUGUI sensitivtyValue;
    int sensitivtyToText;

    [Header("CastMode Settings")]
    [SerializeField] Toggle castModeToggle;
    [SerializeField] CastModeManager castModeManager;

    void Start()
    {
        //volume starting value
        volumeSlider.value = volumeStartingValue;
        AudioListener.volume = volumeStartingValue;
        volumeTxt.SetText((volumeStartingValue * 100).ToString() + "%");

        //sensitivty starting value
        cinemachine = GameObject.Find("ThirdPersonLook")?.GetComponent<CinemachineFreeLook>();
        mouseSensitivtySlider.value = sensitivtyStartingValue;
        if (cinemachine != null)
        {
            cinemachine.m_XAxis.m_MaxSpeed = mouseSensitivtySlider.value;
            sensitivtyValue.SetText(sensitivtyStartingValue.ToString());
        }

        //castMode starting condition
        castModeToggle.isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        VolumeSettings();
        MouseSensitivtySettings();
        AutoCastModeSettings();
    }

    void VolumeSettings()
    {
        audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            AudioListener.volume = volumeSlider.value;
        }
        volumeToText = Mathf.RoundToInt(volumeSlider.value * 100);
        volumeTxt.SetText(volumeToText.ToString() + "%");
    }

    private void MouseSensitivtySettings()
    {
        cinemachine = GameObject.Find("ThirdPersonLook")?.GetComponent<CinemachineFreeLook>();

        if (cinemachine != null)
        {
            cinemachine.m_XAxis.m_MaxSpeed = mouseSensitivtySlider.value;
            sensitivtyToText = Mathf.RoundToInt(mouseSensitivtySlider.value);
            sensitivtyValue.SetText(sensitivtyToText.ToString());
        }
    }

    private void AutoCastModeSettings()
    {
        castModeManager = GameObject.Find("CastMode")?.GetComponentInChildren<CastModeManager>();

        if (castModeManager != null)
        {
            if(castModeToggle.isOn)
            {
                castModeManager.autoCast = true;
            }
            else
            {
                castModeManager.autoCast = false;
            }
        }
    }

    public void ResetGame()
    {
        LoadAsync.instance.LoadScene("RoomScene");
    }

    //enabling Settings UI
    public void EnableSettingsUI()
    {
        if (!settingsUI.activeSelf)
        {
            if (controlsUI.activeSelf)
            {
                controlsUI.SetActive(false);
            }
            settingsUI.SetActive(true);
        }
        else
        {
            if (controlsUI.activeSelf)
            {
                controlsUI.SetActive(false);
            }
            settingsUI.SetActive(false);
        }
    }
}

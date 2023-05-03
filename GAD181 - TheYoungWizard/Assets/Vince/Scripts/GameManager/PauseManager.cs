using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject controlsUI;
    [SerializeField] GameObject settingsUI;
    [SerializeField] Button returnHomeBtn;
    [SerializeField] Button resetLevelBtn;
    [SerializeField] TextMeshProUGUI resetGameText;
    public bool paused;
    void Update()
    {
        EnableDisableUI();
    }

    private void EnableDisableUI()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (Input.GetKeyDown(pauseKey))
            {
                if (!pauseUI.activeSelf)
                {
                    paused = true;
                    EnableCursor(true);
                    pauseUI.SetActive(true);
                    Time.timeScale = 0f;
                }
                else
                {
                    paused = false;
                    ResumeGame();
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            returnHomeBtn.interactable = false;
            resetLevelBtn.interactable = true;
        }
        else if (SceneManager.GetActiveScene().name == "RoomScene 1" || SceneManager.GetActiveScene().name == "RoomScene")
        {
            returnHomeBtn.interactable = false;
            resetLevelBtn.interactable = false;
        }
        else
        {
            returnHomeBtn.interactable = true;
            resetLevelBtn.interactable = true;
        }
       

        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            resetGameText.SetText("END TUTORIAL");
        }
        else
        {
            resetGameText.SetText("RESET LEVEL");
        }
    }

    void EnableCursor(bool enable)
    {
        Cursor.visible = enable;
        if (enable)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ResumeGame()
    {
        controlsUI.SetActive(false);
        settingsUI.SetActive(false);
        EnableCursor(false);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnHome()
    {
        ResumeGame();
        LoadAsync.instance.LoadScene("RoomScene 1");
    }

    public void ResetGame()
    {
        ResumeGame();
        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            LoadAsync.instance.LoadScene("RoomScene");
        }
        else
        {
            LoadAsync.instance.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ResetSettings()
    {

    }

    public void ShowControls()
    {
        if (!controlsUI.activeSelf)
        {
            if (settingsUI.activeSelf)
            {
                settingsUI.SetActive(false);
            }
            controlsUI.SetActive(true);
        }
        else
        {
            if (settingsUI.activeSelf)
            {
                settingsUI.SetActive(false);
            }
            controlsUI.SetActive(false);
        }
    }

    public void Exitgame()
    {
        Application.Quit();
    }
}

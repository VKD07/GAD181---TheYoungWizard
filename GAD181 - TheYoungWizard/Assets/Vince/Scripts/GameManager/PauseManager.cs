using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject controlsUI;
    [SerializeField] Button returnHomeBtn;
    [SerializeField] TextMeshProUGUI resetGameText;
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
                    EnableCursor(true);
                    pauseUI.SetActive(true);
                    Time.timeScale = 0f;
                }
                else
                {
                    ResumeGame();
                }
            }
        }

        if(SceneManager.GetActiveScene().name == "RoomScene 1" || SceneManager.GetActiveScene().name == "RoomScene" || SceneManager.GetActiveScene().name == "TutorialScene")
        {
            returnHomeBtn.interactable = false;
        }
        else
        {
            returnHomeBtn.interactable = true;
        }

        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            resetGameText.SetText("END TUTORIAL");
        }
        else
        {
            resetGameText.SetText("RESET GAME");
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
        LoadAsync.instance.LoadScene("RoomScene");
    }

    private void ResetSettings()
    {
        
    }

    public void ShowControls()
    {
        if (!controlsUI.activeSelf)
        {
            controlsUI.SetActive(true);
        }
        else
        {
            controlsUI.SetActive(false);
        }
    }

    public void Exitgame()
    {
        Application.Quit();
    }
}

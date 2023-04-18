using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralMenu : MonoBehaviour
{
    MainMenu startingMenu;
    [SerializeField] KeyCode menuKey = KeyCode.Escape;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject menuText;
    public GameObject StartingMenu;
    public bool mainMenuOpened;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        findStartingMenu();
        OpenGeneralMenu();
        SwitchScenes();
    }

    private void findStartingMenu()
    {
        StartingMenu = GameObject.FindGameObjectWithTag("StartingMenu");
    }

    private void OpenGeneralMenu()
    {
        if (StartingMenu == null)
        {
            menuText.SetActive(true);
            if (Input.GetKeyDown(menuKey))
            {
                if (!mainMenuOpened)
                {
                    Time.timeScale = 0f;
                    mainMenuOpened = true;
                    mainMenuUI.SetActive(true);
                    ShowCursor();
                }
                else
                {
                    HideCursor();
                    Time.timeScale = 1f;
                    mainMenuOpened = false;
                    mainMenuUI.SetActive(false);
                }
            }
        }
        else
        {
            menuText.SetActive(false);
        }

    }

    private void SwitchScenes()
    {
        if (mainMenuOpened)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ShowCursor();
                SceneManager.LoadScene("RoomScene");
                mainMenuOpened = false;
                mainMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene("Room Scene 1");
                mainMenuOpened = false;
                mainMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene("TutorialScene");
                mainMenuOpened = false;
                mainMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneManager.LoadScene("Forest Level");
                mainMenuOpened = false;
                mainMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SceneManager.LoadScene("WinterLand");
                mainMenuOpened = false;
                mainMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SceneManager.LoadScene("FinalBoss");
                mainMenuOpened = false;
                mainMenuUI.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}

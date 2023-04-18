using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public bool mainMenuActive;
    [SerializeField] GameObject mainMenuCamera;
    [SerializeField] GameObject mainMenuUi;
    [SerializeField] KeyCode mainMenuKeyCode = KeyCode.Return;
    [SerializeField] GameObject player;
    [SerializeField] CanvasGroup alpha;
    [SerializeField] CanvasGroup exitImage;
    [SerializeField] float fadeOutRate = 0.5f;
    [SerializeField] CinemachineFreeLook playerCamera;
    bool startGame;
    public bool cameraControl;
    bool fadeOut;
    // Update is called once per frame

    private void Start()
    {
        cameraControl = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.SetActive(false);
        exitImage.alpha = 0;
        startGame = false;
    }
    void Update()
    {
        TutorialTransition();

        ProceedToRoom();

        DisableCameraControl();
    }

    private void TutorialTransition()
    {
        if (mainMenuActive == false)
        {
            alpha.alpha -= Time.deltaTime * fadeOutRate;
        }
        if (fadeOut)
        {
            if (exitImage.alpha < 1)
            {
                exitImage.alpha += Time.deltaTime * fadeOutRate;
            }
            else
            {
                exitImage.alpha = 1;
                SceneManager.LoadScene("TutorialScene");
            }
        }
    }

    private void ProceedToRoom()
    {
        if (startGame)
        {
            if (alpha.alpha > 0)
            {
                alpha.alpha -= Time.deltaTime * fadeOutRate;
                player.SetActive(true);
                //mainMenuCamera.SetActive(false);
            }
            else
            {
               // cameraControl = true;
                startGame = false;
                alpha.alpha = 0;
                //mainMenuUi.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
       startGame = true;
    }

    public void TutorialScene()
    {
        fadeOut = true;
        mainMenuActive = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisableCameraControl()
    {
        if(!cameraControl)
        {
            playerCamera.m_YAxis.m_MaxSpeed = 0;
            playerCamera.m_XAxis.m_MaxSpeed = 0;
        }
        else
        {
            playerCamera.m_YAxis.m_MaxSpeed = 2;
            playerCamera.m_XAxis.m_MaxSpeed = 300f;
        }
    }
}

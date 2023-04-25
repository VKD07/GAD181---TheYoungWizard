using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public bool mainMenuActive;
    [SerializeField] GameObject mainMenuCamera;
    [SerializeField] GameObject mainMenuUi;
    [SerializeField] GameObject player;
    [SerializeField] CanvasGroup alpha;
    [SerializeField] CanvasGroup exitImage;
    [SerializeField] float fadeOutRate = 0.5f;
    [SerializeField] CinemachineFreeLook playerCamera;
    [SerializeField] Button [] mapButtons;
    GameObject gameManager;
    MapUnlockHandler mapUnlockHandler;
    SpellUnlockHandler spellUnlockHandler;
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
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
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
            }
           
        }
    }

    public void LoadTutoialScene()
    {
        StartCoroutine(Loadtutorial());
    }

    IEnumerator Loadtutorial()
    {
        yield return new WaitForSeconds(5f);
        LoadAsync.instance.LoadScene("TutorialScene");
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
        if (!cameraControl)
        {
            playerCamera.m_YAxis.m_MaxSpeed = 0;
            playerCamera.m_XAxis.m_MaxSpeed = 0;
        }
        else
        {
            playerCamera.m_YAxis.m_MaxSpeed = 2;
            playerCamera.m_XAxis.m_MaxSpeed = 250f;
        }
    }

    public void ResetSettings()
    {
        if(gameManager != null)
        {
            mapUnlockHandler = gameManager.GetComponent<MapUnlockHandler>(); 
            spellUnlockHandler = gameManager.GetComponent<SpellUnlockHandler>();

            mapUnlockHandler.unlockSnowIsland = false;
            mapUnlockHandler.unlockUnknownIsland = false;

            spellUnlockHandler.unlockIceWall = false;
            spellUnlockHandler.unlockLuminous = false;
            spellUnlockHandler.unlockWindGust = false;

            for (int i = 0; i < mapButtons.Length; i++)
            {
                mapButtons[i].interactable = false;
            }
        }
    }
}

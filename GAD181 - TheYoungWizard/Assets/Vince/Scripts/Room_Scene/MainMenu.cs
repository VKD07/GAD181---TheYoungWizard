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
    bool fadeOut;
    // Update is called once per frame

    private void Start()
    {
        player.SetActive(false);
        exitImage.alpha = 0;
    }
    void Update()
    {
        if(mainMenuActive == false)
        {
            alpha.alpha -= Time.deltaTime * fadeOutRate;
        }

        if (fadeOut)
        {
            if(exitImage.alpha < 1)
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

    public void StartGame()
    {
        mainMenuCamera.SetActive(false);
        player.SetActive(true);
        mainMenuActive = false;
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
}

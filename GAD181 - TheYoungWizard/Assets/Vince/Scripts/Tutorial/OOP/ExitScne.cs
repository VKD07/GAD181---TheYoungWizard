using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScne : MonoBehaviour
{
    [SerializeField] CanvasGroup exitImage;
    [SerializeField] CanvasGroup gameLogo;
    [SerializeField] Player_Movement playerMovement;
    [SerializeField] playerCombat playerCombat;
    bool fadeIn;
    public bool startFade;
    bool introTextFadeOut;

    void Start()
    {
        exitImage.alpha = 1;
        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartFadeIn();
        StartFadeOut();
    }

    private void StartFadeIn()
    {
        if (fadeIn)
        {
            if (exitImage.alpha > 0)
            {
                exitImage.alpha -= Time.deltaTime * 0.3f;
            }
            else
            {
                exitImage.alpha = 0;
                fadeIn = false;
            }
        }
    }

    private void StartFadeOut()
    {
        if (startFade)
        {
            if (gameLogo.alpha < 1)
            {
                playerMovement.enabled = false;
                playerCombat.enabled = false;
                gameLogo.alpha += Time.deltaTime * 0.5f;
            }
            else
            {
                if (exitImage.alpha < 1)
                {
                    exitImage.alpha += Time.deltaTime * 0.5f;
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndingScene : MonoBehaviour
{
    [SerializeField] GameObject blackImage;
    [SerializeField] GameObject titleImg;
    [SerializeField] CanvasGroup blckImage;
    [SerializeField] CanvasGroup titleImage;
    public bool startEndScene;
    bool starting;
    void Start()
    {
        blckImage.alpha = 1.0f;
    }

    void Update()
    {
        StartEndScene();
        BlackImageFadeOut();
    }

    private void StartEndScene()
    {
        if (startEndScene)
        {
            blackImage.SetActive(true);
            titleImg.SetActive(true);
            StartCoroutine(BlackImageFadeIn());
            if(blckImage.alpha < 1)
            {
                blckImage.alpha += 0.09f * Time.deltaTime;
            }
            else
            {
                blckImage.alpha = 1f;
            }
        }
    }

    IEnumerator BlackImageFadeIn()
    {
        yield return new WaitForSeconds(3);

        if(titleImage.alpha < 1)
        {
            titleImage.alpha += 0.09f * Time.deltaTime;
        }
        else
        {
            titleImage.alpha = 1f;
            //Ending
            StartCoroutine(ResetScene(10));
        }
    }

    void BlackImageFadeOut()
    {
        if (!starting)
        {
            if (blckImage.alpha > 0)
            {
                blckImage.alpha -= 1f * Time.deltaTime;
            }
            else
            {
                blackImage.SetActive(false);
                blckImage.alpha = 0;
                starting = true;
            }
        }
    }
    
    IEnumerator ResetScene(float time)
    {
        yield return new WaitForSeconds(time);
        //SceneManager.LoadScene(0);
        LoadAsync.instance.LoadScene("RoomScene");
    }
}

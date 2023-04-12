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
    void Start()
    {
        
    }

    void Update()
    {
        StartEndScene();
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
            SceneManager.LoadScene(0);
        }
    }
}

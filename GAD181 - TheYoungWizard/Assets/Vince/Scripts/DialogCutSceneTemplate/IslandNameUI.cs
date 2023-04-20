using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandNameUI : MonoBehaviour
{
    CanvasGroup canvas;
    bool fadeOut;
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        FadeInText();
    }

    private void FadeInText()
    {
        if (canvas.alpha < 1 && !fadeOut)
        {
            canvas.alpha += 0.5f * Time.deltaTime;
        }
        else
        {
            fadeOut = true;
        }
        if (fadeOut)
        {
            StartCoroutine(FadeOutText(3f));
        }
    }

    IEnumerator FadeOutText(float time)
    {
        yield return new WaitForSeconds(time);
        canvas.alpha -= Time.deltaTime * 0.5f;

        if (canvas.alpha == 0)
        {
           this.gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StoneSpell : MonoBehaviour
{
    [SerializeField] GameObject runeStone;
    [SerializeField] GameObject circleVFX;
    [SerializeField] Sprite spellIcon;
    [SerializeField] Image spellImageIcon;
    [SerializeField] GameObject notifCanvas;
    CanvasGroup notifAlpha;
    bool showMessage;
    public bool unlocked;
    

    private void Start()
    {
        notifAlpha = notifCanvas.GetComponent<CanvasGroup>();
        notifAlpha.alpha = 0;
    }

    private void Update()
    {
        SpellUnlockedPopUpMessage();
    }

    private void SpellUnlockedPopUpMessage()
    {
        if (showMessage)
        {
            spellImageIcon.sprite = spellIcon;
            if(notifAlpha.alpha < 1)
            {
                notifCanvas.SetActive(true);
                notifAlpha.alpha += Time.deltaTime;
            }
            else
            {
                StartCoroutine(FadeDelay(5));
            }
        }

        if (notifCanvas.activeSelf && showMessage == false)
        {
            if(notifAlpha.alpha > 0)
            {
                notifAlpha.alpha -= Time.deltaTime;
            }
            else
            {
                notifCanvas.SetActive(false);
                notifAlpha.alpha = 0;
            }
        }
    }

    public void HideRuneStone()
    {
        showMessage = true;
        runeStone.SetActive(false);
        circleVFX.SetActive(false);
    }

    IEnumerator FadeDelay(float time)
    {
        yield return new WaitForSeconds(time);
        showMessage = false;
    }
}

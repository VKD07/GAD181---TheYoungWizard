using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoneSpell : MonoBehaviour
{
    public enum Island
    {
        snowIsland,
        UnknownIsland,
        none,
    }

    public enum Spells
    {
        FrostWall,
        WindGust,
        Luminous,
    }

    [SerializeField] GameObject runeStone;
    [SerializeField] GameObject circleVFX;
    [SerializeField] Sprite spellIcon;
    [SerializeField] Image spellImageIcon;
    [SerializeField] GameObject notifCanvas;
    [SerializeField] MapUnlockHandler mapUnlockHandler;
    [SerializeField] SpellUnlockHandler spellUnlockHandler;
    [SerializeField] Island islandToUnlock;
    [SerializeField] Spells spellsToUnlock;
    [SerializeField] bool disableGoingBack;
    [SerializeField] GameObject spellBookUnlock;
    public bool headBackToRoom;
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
        MessageGameManager();
    }

    private void MessageGameManager()
    {
        mapUnlockHandler = FindObjectOfType<MapUnlockHandler>();
        spellUnlockHandler = FindObjectOfType<SpellUnlockHandler>();

        if (unlocked)
        {
            if (mapUnlockHandler != null)
            {
                MapUnlock();
            }

            if (spellUnlockHandler != null)
            {
                SpellUnlock();
            }
        }
    }

    private void MapUnlock()
    {
        if (islandToUnlock == Island.snowIsland)
        {
            StartCoroutine(GoBackToRoom(5));
            mapUnlockHandler.unlockSnowIsland = true;
        }
        else if (islandToUnlock == Island.UnknownIsland)
        {
            StartCoroutine(GoBackToRoom(5));
            mapUnlockHandler.unlockUnknownIsland = true;
        }
        else if (islandToUnlock == Island.none)
        {
            //do nothing
        }
    }

    private void SpellUnlock()
    {
        if (spellsToUnlock == Spells.WindGust)
        {
            spellBookUnlock.SetActive(true);
            spellUnlockHandler.unlockWindGust = true;
        }
        else if (spellsToUnlock == Spells.FrostWall)
        {
            spellBookUnlock.SetActive(true);
            spellUnlockHandler.unlockIceWall = true;
        }
        else if (spellsToUnlock == Spells.Luminous)
        {
            spellBookUnlock.SetActive(true);
            spellUnlockHandler.unlockLuminous = true;
        }
    }

    private void SpellUnlockedPopUpMessage()
    {
        if (showMessage)
        {
            spellImageIcon.sprite = spellIcon;
            if (notifAlpha.alpha < 1)
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
            if (notifAlpha.alpha > 0)
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

    IEnumerator GoBackToRoom(float time)
    {
        yield return new WaitForSeconds(time);
        if (!disableGoingBack)
        {
            SceneManager.LoadScene("RoomScene 1");
        }
    }
}

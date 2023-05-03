using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideUiScript : MonoBehaviour
{
    [Header("Guide UIs")]
    [SerializeField] GameObject combineUI;
    [SerializeField] GameObject spellBookUI;
    [SerializeField] KeyCode openSpellBookKey = KeyCode.P;
    [SerializeField] Image spellBookGuide;
    [SerializeField] Image SpellCastModeGuide;
    [SerializeField] Image useSpellGuide;
    [SerializeField] Image spellBookIcon;
    [SerializeField] Sprite openedSpellBook;
    [SerializeField] Sprite spellBookClosed;
    [Header("SFX")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip spellBookSfx;
    [Header("References")]
    [SerializeField] playerCombat pc;
    [SerializeField] public bool disableTimePause;
    public bool spellBookOpened;
    [SerializeField] FirstChallenge challengeScript;

    void Start()
    {
        combineUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SpellBookUI();
    }

    private void SpellBookUI()
    {
        if (Input.GetKeyDown(openSpellBookKey))
        {
            if (!spellBookOpened)
            {
                //for the final cutscene
                if(challengeScript != null)
                {
                    //challengeScript.stopTimer = true;
                }
                //end

                EnableMouse(true);
                audioSource.PlayOneShot(spellBookSfx, 0.5f);
                spellBookIcon.sprite = openedSpellBook;
                if (disableTimePause)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                spellBookOpened = true;
                spellBookUI.SetActive(true);
            }
            else
            {
                CloseSpellBook();
                //end
            }
        }
    }

    public void CloseSpellBook()
    {
        EnableMouse(false);
        Time.timeScale = 1f;
        spellBookIcon.sprite = spellBookClosed;
        spellBookOpened = false;
        spellBookUI.SetActive(false);

        //for the final cutscene
        if (challengeScript != null)
        {
            challengeScript.stopTimer = false;
        }
    }

    public void SpellCastingMode()
    {
        combineUI.SetActive(true);
        SpellCastModeGuide.enabled = false;
        useSpellGuide.enabled = false;
    }

    public void DisableUISpellCastingMode()
    {
        combineUI.SetActive(false);
        SpellCastModeGuide.enabled = true;
        useSpellGuide.enabled = true;
    }

    void EnableMouse(bool enable)
    {
        Cursor.visible = enable;

        if(enable)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

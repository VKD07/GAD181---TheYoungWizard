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
    [Header("References")]
    [SerializeField] playerCombat pc;
    [SerializeField] bool disableTimePause;
    public bool spellBookOpened;

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
                if (disableTimePause)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                spellBookOpened = true;
                spellBookGuide.enabled = false;
                spellBookUI.SetActive(true);
            }
            else
            {

                Time.timeScale = 1f;

                spellBookOpened = false;
                spellBookGuide.enabled = true;
                spellBookUI.SetActive(false);
            }
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
}

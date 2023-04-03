using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentsHandler : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Player_Movement pm;
    [SerializeField] playerCombat pCombat;
    [SerializeField] GameObject thirdPersonCam;
    [SerializeField] GameObject playerAttrib;

    [Header("Player SpellCast UI")]
    [SerializeField] GameObject castMode;
    [SerializeField] GameObject spellCast;
    [SerializeField] GameObject useSpell;
    [SerializeField] GameObject spellBookGuide;
    [SerializeField] GameObject spellBookMainUI;


    void Start()
    {
        thirdPersonCam.SetActive(false);
        pm.enabled = false;
        pCombat.enabled = false;
        castMode.SetActive(false);
        playerAttrib.SetActive(false);
        useSpell.SetActive(false);
        spellCast.SetActive(false);
        spellBookGuide.SetActive(false);
        spellBookMainUI.SetActive(false);

    }

    public void EnablePlayerMovement(bool value)
    {
        pm.enabled = value;
        thirdPersonCam.SetActive(value);
    }

    public void EnablePlayerCombat()
    {
        pCombat.enabled = true;
    }

    public void EnablePlayerAttrib(bool value)
    {
        if (value)
        {
            playerAttrib.SetActive(true);
        }
        else
        {
            playerAttrib.SetActive(false);
        }
    }

    public void EnableSpellCastUI(bool value)
    {
        if (value)
        {
            castMode.SetActive(true);
            spellCast.SetActive(true);
            useSpell.SetActive(true);
        }
        else
        {
            castMode.SetActive(false);
            spellCast.SetActive(false);
            useSpell.SetActive(false);
        }
    }

    public void DisableCastMode(bool value)
    {
        pCombat.disableCastMode = value;
    }

    public void EnableSpellBook(bool value)
    {
        if (value)
        {
            spellBookGuide.SetActive(true);
        }
        else
        {
            spellBookMainUI.SetActive(false);
        }
    }

}

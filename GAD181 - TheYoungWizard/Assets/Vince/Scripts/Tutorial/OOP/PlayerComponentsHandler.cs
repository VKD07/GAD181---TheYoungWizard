using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class PlayerComponentsHandler : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Player_Movement pm;
    [SerializeField] playerCombat pCombat;
    [SerializeField] GameObject thirdPersonCam;
    [SerializeField] GameObject playerAttrib;
    [SerializeField] Animator playerAnimator;

    [Header("Player SpellCast UI")]
    [SerializeField] GameObject castMode;
    [SerializeField] GameObject spellCast;
    [SerializeField] GameObject useSpell;
    [SerializeField] GameObject spellBookGuide;
    [SerializeField] GameObject spellBookMainUI;
    [SerializeField] GameObject pauseMenu;
    public bool disableMouse;


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

    private void Update()
    {
        DisableMouse();
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
        playerAttrib.SetActive(value);
    }

    public void EnableSpellCastUI(bool value)
    {
        castMode.SetActive(value);
        spellCast.SetActive(value);
        useSpell.SetActive(value);
    }

    public void DisableCastMode(bool value)
    {
        pCombat.disableCastMode = value;
    }

    public void EnableSpellBook(bool value)
    {
        spellBookGuide.SetActive(value);
    }

    public void DisablePlayerAnimation()
    {
        playerAnimator.enabled = false;
    }

    void DisableMouse()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        if (disableMouse)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            StartCoroutine(EnableMouse());
        }
    }

    IEnumerator EnableMouse()
    {
        yield return new WaitForSeconds(5f);
        disableMouse = false;
    }
}

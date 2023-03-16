using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastModeManager : MonoBehaviour
{
    [SerializeField] Player_Movement playerScript;
    [SerializeField] playerCombat pc;
    [SerializeField] float castModeTimer;
    //animators effects
    [SerializeField] Animator spellCastUiAnim;
    [SerializeField] Animator centerCircleAnim;
    [SerializeField] GameObject castEffectObj;
    [SerializeField] Animator castEffect;
    //arrays of elements and slots
    [SerializeField] Sprite[] spriteElements;
    [SerializeField] Image[] elementSlots;
    [SerializeField] bool[] elementState;
    [SerializeField] GameObject[] selectionSlots;

    //Spells 
    public int[] spellIDs;
    public int currentSpellID;
    public int availableSpellID;
    [SerializeField] Image spellSlot;
    [SerializeField] public Sprite[] spellIcons;
    [SerializeField] Sprite defaultIcon;
    public bool castingMode = false;
    public bool doneCombining = false;

    private void Update()
    {
        //after you finished casting
        if (this.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.R))
        {
            spellCastUiAnim.SetBool("CastMode", false);
            castEffect.SetBool("ActivateEffect", false);
            castEffectObj.SetActive(false);
            pc.casting = false;
            //resets spell combinations after UI is disabled;
            settingSpellSprite();
            doneCombining = false;
            castingMode = false;
            ResetSpell();
            this.gameObject.SetActive(false);
            playerScript.enabled = true;
            Time.timeScale = 1f;
        }
        CastMode();
    }
    private void OnEnable()
    {
        refreshSpell();
        //activatinng effects
        castEffectObj.SetActive(true);
        castEffect.SetBool("ActivateEffect", true);
        //when the UI is enable start the timer, disable the movement scrpt then reactived everything again after few seconds
        StartCoroutine(disableUI());
        castingMode = true;
        playerScript.enabled = false;
    }

    //if the timer is finished
    IEnumerator disableUI()
    {
        //if the UI is disabled 
        yield return new WaitForSeconds(castModeTimer);
        pc.casting = false;
        Time.timeScale = 1;
        doneCombining = false;
        castingMode = false;
        playerScript.enabled = true;

        settingSpellSprite();

        //resets spell combinations after UI is disabled;
        ResetSpell();
        spellCastUiAnim.SetBool("CastMode", false);
        centerCircleAnim.SetBool("SpinCircle", false);
        castEffect.SetBool("ActivateEffect", false);
        castEffectObj.SetActive(false);
        this.gameObject.SetActive(false);

    }
    void CastMode()
    {
        //Choose first slot
        if (elementState[0] == false)
        {
            Element(0);
        }
        // Choose Second slot if first slot done
        else if (elementState[1] == false && elementState[0] == true)
        {
            Element(1);
        }
        //Choose third slot if second slot done
        else if (elementState[2] == false && elementState[1] == true)
        {
            Element(2);
        }
        //this prevents combining spells with only combing 2 elements
        if(elementState[2] == true)
        {
            doneCombining = true;
        }
    }

    //handling the elements chosen by the player
    void Element(int elementValue)
    {
        if (this.gameObject.activeSelf == true)
        {
            selectionSlots[elementValue].SetActive(true);
        }

        //if player choosed 1st element
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //element decided is true
            elementState[elementValue] = true;

            //remove transparecy of the color 
            Color currentColor = elementSlots[elementValue].color;
            currentColor.a = 1f;
            elementSlots[elementValue].color = currentColor;

            //change the slot image to the element chosen
            elementSlots[elementValue].sprite = spriteElements[0];
            selectionSlots[elementValue].SetActive(false);
            //adding the element ID
            currentSpellID += 5;

        }
        //if player choosed 2nd element
        else if (Input.GetKeyDown(KeyCode.W))
        {
            selectionSlots[elementValue].SetActive(true);
            elementState[elementValue] = true;
            Color currentColor = elementSlots[elementValue].color;
            currentColor.a = 1f;
            elementSlots[elementValue].color = currentColor;
            elementSlots[elementValue].sprite = spriteElements[1];
            selectionSlots[elementValue].SetActive(false);
            currentSpellID += 15;
        }
        // if player choosed 3rd element
        else if (Input.GetKeyDown(KeyCode.E))
        {
            selectionSlots[elementValue].SetActive(false);
            elementState[elementValue] = true;
            Color currentColor = elementSlots[elementValue].color;
            currentColor.a = 1f;
            elementSlots[elementValue].color = currentColor;
            elementSlots[elementValue].sprite = spriteElements[2];
            selectionSlots[elementValue].SetActive(false);
            currentSpellID += 20;
        }
    }

    void settingSpellSprite()
    {
        for (int i = 0; i < spellIDs.Length; i++)
        {
            if (spellIDs[i] == currentSpellID && doneCombining == true)
            {
                spellSlot.sprite = spellIcons[i];
                //Store the current spell available to use
                availableSpellID = currentSpellID;
                break;
            }
            else
            {
                //insert the consequences here if the player miscombined the elements------------------------
                spellSlot.sprite = defaultIcon;
            }
        }
    }

    private void refreshSpell()
    {
        //loop throuhg the element slots and reset everything from the transparecy color, 
        // to the element sprits and the element states;
        for (int i = 0; i < elementSlots.Length; i++)
        {
            elementSlots[i].sprite = null;
            // transparecy of the color activated
            Color currentColor = elementSlots[i].color;
            currentColor.a = 0f;

            elementSlots[i].color = currentColor;
            elementState[i] = false;
            selectionSlots[i].SetActive(false);
            //reset the spell ID after finish combining
            //reset the spell ID after finish combining
            currentSpellID = 0;
        }
    }
    void ResetSpell()
    {
        //loop throuhg the element slots and reset everything from the transparecy color, 
        // to the element sprits and the element states;
        for (int i = 0; i < elementSlots.Length; i++)
        {
            elementState[i] = false;
            selectionSlots[i].SetActive(false);
            //reset the spell ID after finish combining
            //reset the spell ID after finish combining
            currentSpellID = 0;
        }
    }
}

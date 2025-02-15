using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Auto_CastMode : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Player_Movement playerScript;
    [SerializeField] playerCombat pc;
    [SerializeField] float castModeTimer;
    [SerializeField] bool disableMovement;
    [Header("Animator Effects")]
    [SerializeField] Animator spellCastUiAnim;
    [SerializeField] Animator centerCircleAnim;
    [SerializeField] GameObject castEffectObj;
    [SerializeField] Animator castEffect;
    [Header("Arrays of Element slots")]
    [SerializeField] Sprite[] spriteElements;
    [SerializeField] Image[] elementSlots;
    [SerializeField] bool[] elementState;
    [SerializeField] GameObject[] selectionSlots;

    [Header("Spells")]
    public int[] spellIDs;
    public int currentSpellID;
    public int availableSpellID;
    [SerializeField] Image spellSlot;
    [SerializeField] public Sprite[] spellIcons;
    [SerializeField] Sprite defaultIcon;
    public bool castingMode = false;
    public bool doneCombining = false;
    public bool correctCombination;
    public bool wrongCombination = false;

    [Header("Sound Effects")]
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip choosingElementFx;
    [SerializeField] AudioClip correctCombiFx;

    [Header("Locked Spell")]
    [SerializeField] public bool windGustLocked = true;
    [SerializeField] public bool IceSpellLocked = true;
    [SerializeField] public bool luminousLocked = true;

    private void Update()
    {
        //after you finished casting
        if (this.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.R))
        {
            spellCastUiAnim.SetBool("CastMode", false);
            centerCircleAnim.SetBool("SpinCircle", false);
            castEffect.SetBool("ActivateEffect", false);
            castEffectObj.SetActive(false);
            pc.casting = false;
            //resets spell combinations after UI is disabled;

            doneCombining = false;
            castingMode = false;
            //ResetSpell();
            this.gameObject.SetActive(false);
            if (!disableMovement)
            {
                playerScript.enabled = true;
            }
            Time.timeScale = 1f;
        }
        CastMode();

        settingSpellSprite();
    }

    private void OnEnable()
    {
        refreshSpell();
        playerAudio.PlayOneShot(choosingElementFx, 0.1f);
        //activatinng effects
        castEffectObj.SetActive(true);
        castEffect.SetBool("ActivateEffect", true);
        centerCircleAnim.SetBool("SpinCircle", true);
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
        if (!disableMovement)
        {
            playerScript.enabled = true;
        }
        settingSpellSprite();
        //resets spell combinations after UI is disabled;
        //ResetSpell();
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
        if (elementState[2] == true)
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

            //sound effects
            playerAudio.PlayOneShot(choosingElementFx, 0.1f);
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
            playerAudio.PlayOneShot(choosingElementFx, 0.1f);
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
            playerAudio.PlayOneShot(choosingElementFx, 0.1f);
        }
    }

    void settingSpellSprite()
    {
        if (doneCombining)
        {
            for (int i = 0; i < spellIDs.Length; i++)
            {
                if (spellIDs[i] == currentSpellID)
                {
                    //Spell lock
                    if (IceSpellLocked && currentSpellID == 30)
                    {
                        return;
                    }
                    else if (windGustLocked && currentSpellID == 35)
                    {
                        return;
                    }
                    else if (luminousLocked && currentSpellID == 40)
                    {
                        return;
                    }
                    else
                    {
                        wrongCombination = false;
                        spellSlot.sprite = spellIcons[i];
                        DisableCastMode();
                        //Store the current spell available to use
                        availableSpellID = currentSpellID;
                        correctCombination = true;
                        playerAudio.PlayOneShot(correctCombiFx, 0.1f);
                        break;
                    }
                }
                else
                {
                    DisableCastMode();
                    wrongCombination = false;
                }


            }
            //else
            //{
            //    //insert the consequences here if the player miscombined the elements------------------------
            //    wrongCombination = true;
            //    correctCombination = false;
            //    spellSlot.sprite = defaultIcon;
            //}
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

    void DisableCastMode()
    {
        pc.casting = false;
        Time.timeScale = 1;
        doneCombining = false;
        castingMode = false;
        if (!disableMovement)
        {
            playerScript.enabled = true;
        }
        settingSpellSprite();
        //resets spell combinations after UI is disabled;
        //ResetSpell();
        spellCastUiAnim.SetBool("CastMode", false);
        centerCircleAnim.SetBool("SpinCircle", false);
        castEffect.SetBool("ActivateEffect", false);
        castEffectObj.SetActive(false);
        this.gameObject.SetActive(false);
    }
}

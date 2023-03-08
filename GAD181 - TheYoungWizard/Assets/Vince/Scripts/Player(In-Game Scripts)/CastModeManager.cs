using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastModeManager : MonoBehaviour
{
    [SerializeField] Player_Movement playerScript;
    [SerializeField] float castModeTimer;

    //arrays of elements and slots
    [SerializeField] Sprite[] spriteElements;
    [SerializeField] Image[] elementSlots;
    [SerializeField] bool[] elementState;


    //Spells 
    public int[] spellIDs;
    public int currentSpellID;
    public int availableSpellID;
    [SerializeField] Image spellSlot;
    [SerializeField] public Sprite[] spellIcons;
    public bool castingMode = false;



    void Start()
    {

    }

    private void Update()
    {
        //after you finished casting
        if (this.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.R))
        {
            //resets spell combinations after UI is disabled;
            spellCasting();
            castingMode = false;
            ResetSpellCombinations();
            this.gameObject.SetActive(false);
            playerScript.enabled = true;
            Time.timeScale = 1f;
        }

        CastMode();

    }
    private void OnEnable()
    {
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

        Time.timeScale = 1;
        castingMode = false;
        playerScript.enabled = true;

        spellCasting();

        //resets spell combinations after UI is disabled;
        ResetSpellCombinations();

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
    }

    //handling the elements chosen by the player
    void Element(int elementValue)
    {
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

            //adding the element ID
            currentSpellID += 5;

        }
        //if player choosed 2nd element
        else if (Input.GetKeyDown(KeyCode.W))
        {
            elementState[elementValue] = true;
            Color currentColor = elementSlots[elementValue].color;
            currentColor.a = 1f;
            elementSlots[elementValue].color = currentColor;
            elementSlots[elementValue].sprite = spriteElements[1];

            currentSpellID += 15;
        }
        // if player choosed 3rd element
        else if (Input.GetKeyDown(KeyCode.E))
        {
            elementState[elementValue] = true;
            Color currentColor = elementSlots[elementValue].color;
            currentColor.a = 1f;
            elementSlots[elementValue].color = currentColor;
            elementSlots[elementValue].sprite = spriteElements[2];

            currentSpellID += 20;
        }
    }

    void spellCasting()
    {
        for (int i = 0; i < spellIDs.Length; i++)
        {
            if (spellIDs[i] == currentSpellID)
            {
                spellSlot.sprite = spellIcons[i];
                //Store the current spell available to use
                availableSpellID = currentSpellID;
                break;
            }
            else
            {
                //insert the consequences here if the player miscombined the elements------------------------
            }
        }
    }

    private void ResetSpellCombinations()
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

            //reset the spell ID after finish combining
            //reset the spell ID after finish combining
            currentSpellID = 0;
        }
    }



}

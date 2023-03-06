using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject castManager;
    CastModeManager castMode;

    public float spellID;
    public int[] spells;

    [Header("Spell CoolDowns")]
    public float fireBall = 10f;
    public float ice = 10f;
    public float windGust = 10f;
    public float spark = 5f;

    //spell cooldownStates
    public bool iceCooldown = false;
    public float iceCurrentCoolDown = 0f;

    public bool windGustCoolDown = false;
    public float windGustCurrentCoolDown = 0f;

    public bool sparkCoolDown = false;
    public float sparkCurrentCoolDown = 0f;

    public bool fireBallCoolDown = false;
    public float fireBallCurrentCoolDown = 0f;

    //spell images 
    public Sprite[] spellImages;
    [SerializeField] Image spellSlot;




    // Update is called once per frame
    void Update()
    {
        //find the cast manager game object
        castManager = GameObject.FindGameObjectWithTag("CastMode");

        //spell cooldown handler
        if (spellID != 0)
        {
            //if player is on the target mode release spell
            if (spellID == spells[0] && Input.GetKey(KeyCode.Mouse1) && Input.GetKeyDown(KeyCode.E))
            {
                iceCooldown = true;

                iceSpell();
            }
            else if (spellID == spells[1] && Input.GetKey(KeyCode.Mouse1) && Input.GetKeyDown(KeyCode.E))
            {
                windGustCoolDown = true;
                windGustSpell();
            }
            else if (spellID == spells[2] && Input.GetKey(KeyCode.Mouse1) && Input.GetKeyDown(KeyCode.E))
            {
                sparkCoolDown = true;
                sparkSpell();
            }
            else if (spellID == spells[3] && Input.GetKey(KeyCode.Mouse1) && Input.GetKeyDown(KeyCode.E))
            {
                fireBallCoolDown = true;
                fireBallSpell();

            }
        }

        //spell cooldown functions
        iceSpell();
        windGustSpell();
        sparkSpell();
        fireBallSpell();
        //handles the switching of cooldown values
        coolDownUI();

        // if cast mode UI is disabled then handle the null error
        if (castManager == null)
        {
            return;
            //reset the variables
        }

        castMode = castManager.GetComponent<CastModeManager>();

        if (castMode != null)
        {
            //Get ID elements and store it to this script
            for (int i = 0; i < castMode.spellIDs.Length; i++)
            {

                spells[i] = castMode.spellIDs[i];
            }

            for (int i = 0; i < spellImages.Length; i++)
            {

                spellImages[i] = castMode.spellIcons[i];
            }

            //get the current spell ID chosen
            spellID = castMode.currentSpellID;

        }




    }

    private void coolDownUI()
    {
        if (spellImages != null)
        {
            //if the image appeared match it with the cool down
            if (spellSlot.sprite == spellImages[0])
            {
                //slider value will change according to the image shown in the screen
                slider.value = iceCurrentCoolDown;
            }
            else if (spellSlot.sprite == spellImages[1])
            {
                slider.value = windGustCurrentCoolDown;
            }
            else if (spellSlot.sprite == spellImages[2])
            {
                slider.value = sparkCurrentCoolDown;
            }
            else if (spellSlot.sprite == spellImages[3])
            {
                slider.value = fireBallCurrentCoolDown;
            }
        }
    }

    void fireBallSpell()
    {
        if (fireBallCoolDown == true)
        {

            if (fireBallCurrentCoolDown > 0)
            {
                fireBallCurrentCoolDown -= Time.deltaTime;

            }
            else
            {
                fireBallCurrentCoolDown = fireBall;
                fireBallCoolDown = false;
            }
        }
    }

    void iceSpell()
    {

        if (iceCooldown == true)
        {

            if (iceCurrentCoolDown > 0)
            {
                iceCurrentCoolDown -= Time.deltaTime;

            }
            else
            {
                iceCurrentCoolDown = ice;
                iceCooldown = false;
            }
        }
    }

    void windGustSpell()
    {
        if (windGustCoolDown == true)
        {
            print("casted Ice");
            if (windGustCurrentCoolDown > 0)
            {
                windGustCurrentCoolDown -= Time.deltaTime;
                slider.value = windGustCurrentCoolDown;
            }
            else
            {
                windGustCurrentCoolDown = windGust;
                windGustCoolDown = false;
            }
        }
    }

    void sparkSpell()
    {
        if (sparkCoolDown == true)
        {
            print("casted Ice");
            if (sparkCurrentCoolDown > 0)
            {
                sparkCurrentCoolDown -= Time.deltaTime;

            }
            else
            {
                sparkCurrentCoolDown = spark;
                sparkCoolDown = false;
            }
        }
    }

}

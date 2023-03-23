using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    [Header("Spells Settings")]
    [SerializeField] GameObject castManager;
    [SerializeField] playerCombat pc;
    CastModeManager castMode;

    [Header("Spell CoolDowns")]
    public float spellID;
    public int[] spells;
    public float fireBall = 10f;
    public float ice = 10f;
    public float windGust = 10f;
    public float spark = 5f;

    [Header("Cooldown Manager")]
    [SerializeField] Slider slider;
    [SerializeField] GameObject coolDownUi;
    [SerializeField] Sprite[] coolDownSprites;
    [SerializeField] Image coolDownFillImg;

    public bool iceCooldown = false;
    public float iceCurrentCoolDown = 0f;

    public bool windGustCoolDown = false;
    public float windGustCurrentCoolDown = 0f;

    public bool sparkCoolDown = false;
    public float sparkCurrentCoolDown = 0f;

    public bool fireBallCoolDown = false;
    public float fireBallCurrentCoolDown = 0f;

    [Header("SpellImages")]
    public Sprite[] spellImages;
    [SerializeField] Image spellSlot;
    Color spellColor;

    private void Start()
    {
        pc = FindObjectOfType<playerCombat>();
        coolDownUi.SetActive(false);
        setSpellUiTransparency(1f);
    }

    // Update is called once per frame
    void Update()
    {
        //find the cast manager game object
        castManager = GameObject.FindGameObjectWithTag("CastMode");
        //spell cooldown handler
        if (spellID != 0)
        {
            //if player is on the target mode release spell
            if (spellID == spells[0]  && pc.GetPlayerMana() > 20 && iceCooldown == true)
            {
                iceSpell();
            }
            else if (spellID == spells[1] && pc.GetPlayerMana() > 20 && windGustCoolDown == true)
            {
                
                windGustSpell();
            }
            else if (spellID == spells[2] && pc.GetPlayerMana() > 20 && sparkCoolDown == true)
            {
                sparkSpell();
            }
            else if (spellID == spells[3] && pc.GetPlayerMana() > 20 && fireBallCoolDown == true)
            {
                fireBallSpell();
            }
            else
            {
                coolDownUi.SetActive(false);
                setSpellUiTransparency(1f);
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
                coolDownFillImg.sprite = coolDownSprites[0];
                SetCoolDownImage(iceCooldown);
            }
            else if (spellSlot.sprite == spellImages[1])
            {
                slider.value = windGustCurrentCoolDown;
                coolDownFillImg.sprite = coolDownSprites[1];
                SetCoolDownImage(windGustCoolDown);
            }
            else if (spellSlot.sprite == spellImages[2])
            {
                slider.value = sparkCurrentCoolDown;
                coolDownFillImg.sprite = coolDownSprites[2];
                SetCoolDownImage(sparkCoolDown);
            }
            else if (spellSlot.sprite == spellImages[3])
            {
                slider.value = fireBallCurrentCoolDown;
                coolDownFillImg.sprite = coolDownSprites[3];
                SetCoolDownImage(fireBallCoolDown);
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

    //activating and deactivating UI cool down depends if the spell is cooldown or not
    void SetCoolDownImage(bool spellCoolDown)
    {
        if (spellCoolDown == true)
        {
            coolDownUi.SetActive(true);
            //changing the opacity if on cooldown
            setSpellUiTransparency(0.3f);
        }
        else
        {
            setSpellUiTransparency(1f);
            coolDownUi.SetActive(false);
        }
    }

    void setSpellUiTransparency(float value)
    {
        spellColor = new Color(spellSlot.color.r, spellSlot.color.g, spellSlot.color.b, value);
        spellSlot.color = spellColor;
    }
}

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class playerCombat : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float playerHealth;
    [SerializeField] float playerMana;
    Player_Movement playerMovement;

    [Header("DeathHandler")]
    AudioSource[] audioSources;
    public bool enableAudioSource;

    [Header("Damage Indicator")]
    [SerializeField] GameObject damageIndicator;
    [SerializeField] float damageIndicatorDuration = 0.2f;
    [SerializeField] Animator damageIndicatorAnimation;
    bool tookDamage;
    float currentIndicatorTime;

    [Header("Spell Mana Cost")]
    [SerializeField] int fireballManaCost;
    [SerializeField] int iceSpellManaCost;
    [SerializeField] int luminousManaCost;
    [SerializeField] int windGustManaCost;

    [Header("Item Settings")]
    [SerializeField] float healthPotionValue = 40f;
    [SerializeField] float manaPotionValue = 40f;

    [Header("Character Animation")]
    [SerializeField] public Animator anim;
    bool paused = false;

    [Header("Cast Mode")]
    [SerializeField] GameObject castUI;
    [SerializeField] CastModeManager castModeManager;
    [SerializeField] Animator spellCastUIAnim;
    public bool disableCastMode;

    [Header("Spell Cast")]
    [SerializeField] public KeyCode activateSpellKey = KeyCode.E;
    [SerializeField] SpellSlot spellManager;
    [SerializeField] Animator sliderAnimation;
    [HideInInspector] public bool casting = false;
    public bool disableSlowDownTime = false;
    public bool castingSpell = false;

    [Header("Aim Mode")]
    [SerializeField] bool disablePlayerAttack;
    CinemachineComposer midRig;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] CinemachineFreeLook cam;
    [SerializeField] GameObject targetSight;
    [SerializeField] float normalFOV = 40f;
    [SerializeField] float aimFOV = 30f;
    public bool targetMode = false;
    public static bool rolled = false;

    [Header("Item Manager")]
    [SerializeField] ItemManager itemManager;

    //Attack animation combo
    [Header("Animation Combo")]
    public int AttackNumber;
    public bool attacking = false;
    public float currentTimeToChangeAnim;
    public float timeLimit = 1f;

    [Header("ForceField")]
    [SerializeField] PlayerForceField forceField;
    [SerializeField] KeyCode dodgeKey = KeyCode.LeftControl;

    [Header("Damage Indicator")]
    [SerializeField] GameObject awarenessUI;
    bool sensesEnabled;
    [SerializeField] float damageIndicatorMaxTime = 5f;
    [SerializeField] bool enableTimeScale;
    float currentDmgIndTime;

    [Header("Potions Effects")]
    [SerializeField] ParticleSystem healParticles;
    [SerializeField] ParticleSystem manaParticles;

    [Header("SFX")]
    [SerializeField] public PlayerSoundsHandler sfx;

    [Header("CheckPoint")]
    [SerializeField] RespawnPointHandler respawnPointHandler;
    public bool dodge = false;
    public float shieldDuration = 3f;
    public bool tutorial;


    private void Awake()
    {
        Spawning();
    }
    void Start()
    {
        CursorLock();
        //giving control of camera
        cam.m_YAxis.m_MaxSpeed = 2;
        cam.m_XAxis.m_MaxSpeed = 200;
        //getting the cinemachinefree look mid rig
        midRig = cam.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        //disabling sight at first
        targetSight.SetActive(false);
        //instantiating midrig values
        cam.m_Lens.FieldOfView = 33f;
        midRig.m_TrackedObjectOffset.x = 0.25f;
        playerMovement = GetComponent<Player_Movement>();
        audioSources = FindObjectsOfType<AudioSource>();
    }

    private void Spawning()
    {
        if (!tutorial)
        {
            respawnPointHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnPointHandler>();
            this.gameObject.transform.position = respawnPointHandler.storedRespawnPoint;
            Time.timeScale = 1f;
        }
        //print(respawnPointHandler.storedRespawnPoint + "," + transform.position);
    }

    private void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //pause game
        if (Input.GetKeyDown(KeyCode.Slash) && paused == false)
        {
            paused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Slash) && paused == true)
        {
            paused = false;
            Time.timeScale = 1;
        }

        //focusing on targeting the enemy
        aimMode();
        attack();
        castMode();

        //Item handler
        ItemHandler();
        Dodge();

        //spell cast
        SpellCastAnimation();
        DisablingDamageIndicator();
        DeathHandler();
        ShowDamageFeedback();
    }

    private void DeathHandler()
    {
        if (playerHealth <= 0 && !tutorial)
        {
            // StopAllAudio();
            // enableAudioSource = true;
            sfx.PlayDefeatSfx();
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.SetBool("Dead", true);
            Time.timeScale = 0f;
        }
    }

    private void SpellCastAnimation()
    {
        //if combination is not wrong then you can activate a spell
        if (castModeManager.wrongCombination == false)
        {
            //casting ice
            if (spellManager.iceCooldown == false && castModeManager.availableSpellID == 30 && castModeManager.castingMode == false
                && playerMovement.rolling == false && castingSpell == false && Input.GetKeyDown(activateSpellKey)
                && playerMana >= 20)
            {
                castingSpell = true;
                spellManager.iceCooldown = true;
                anim.SetTrigger("IceSpell");
                playerMana -= iceSpellManaCost;
            }

            //casting a fireball
            if (castModeManager.availableSpellID == 55 && spellManager.fireBallCoolDown == false
                && castModeManager.castingMode == false && playerMovement.rolling == false
                && castingSpell == false && Input.GetKeyDown(activateSpellKey) && playerMana >= 20)
            {
                castingSpell = true;
                spellManager.fireBallCoolDown = true;
                anim.SetTrigger("FireBall");
                playerMana -= fireballManaCost;
            }

            //casting wind gust
            if (castModeManager.availableSpellID == 35 && spellManager.windGustCoolDown == false
                && castModeManager.castingMode == false && playerMovement.rolling == false
                && castingSpell == false && Input.GetKeyDown(activateSpellKey) && playerMana >= 20)
            {
                castingSpell = true;
                spellManager.windGustCoolDown = true;
                anim.SetTrigger("WindGust");
                playerMana -= windGustManaCost;
            }

            //luminous spell
            //casting wind gust
            if (castModeManager.availableSpellID == 40 && spellManager.sparkCoolDown == false
                && castModeManager.castingMode == false && playerMovement.rolling == false
                && castingSpell == false && Input.GetKeyDown(activateSpellKey) && playerMana >= 20)
            {
                castingSpell = true;
                spellManager.sparkCoolDown = true;
                anim.SetTrigger("Luminous");
                playerMana -= luminousManaCost;
            }
            //if player tries to cast a spell with not enough mana
            if (castModeManager.castingMode == false && playerMovement.rolling == false
                && castingSpell == false && Input.GetKeyDown(activateSpellKey) && playerMana <= 20)
            {
                sliderAnimation.SetTrigger("NotEnoughMana");
            }
        }
    }

    //player has shield while rolling
    private void Dodge()
    {
        if (Input.GetKeyDown(dodgeKey))
        {
            dodge = true;
        }

        if (dodge == true && shieldDuration > 0)
        {
            disableSenses();

            if (casting == false)
            {
                if (!enableTimeScale)
                {
                    Time.timeScale = 1;
                }
            }

            shieldDuration -= 0.3f * Time.deltaTime;

            if (shieldDuration <= 0)
            {
                dodge = false;
                shieldDuration = 0.2f;
            }
        }

    }

    private void castMode()
    {
        if (Input.GetKeyDown(KeyCode.R) && !disableCastMode)
        {
            if (castUI.activeSelf == false)
            {
                spellCastUIAnim.SetBool("CastMode", true);
                casting = true;
                //activate cast mode UI
                castUI.SetActive(true);
                //slow down time
                if (!disableSlowDownTime)
                {
                    Time.timeScale = 0.1f;
                }
            }
        }
    }

    private void aimMode()
    {
        //sight activated
        if (Input.GetKey(KeyCode.Mouse1) && playerMovement.rolling == false)
        {
            targetMode = true;
            targetSight.SetActive(true);
            if (!playerMovement.isMoving)
            {
                cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
            }
            else
            {
                cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
            }
            if (midRig.m_TrackedObjectOffset.x < 0.95f)
            {
                midRig.m_TrackedObjectOffset.x += 5f * Time.deltaTime;
            }

            if (cam.m_Lens.FieldOfView > aimFOV)
            {
                cam.m_Lens.FieldOfView -= 60f * Time.deltaTime;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) || castingSpell == false)
        {
            targetMode = false;
            targetSight.SetActive(false);
            cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
            if (midRig.m_TrackedObjectOffset.x > 0.2371475f)
            {
                midRig.m_TrackedObjectOffset.x -= 5f * Time.deltaTime;
            }
            if (cam.m_Lens.FieldOfView < normalFOV)
            {
                cam.m_Lens.FieldOfView += 60f * Time.deltaTime;
            }

        }

    }

    public void attack()
    {
        if (!disablePlayerAttack)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && targetMode && !attacking && !forceField.startAttackDelay)
            {
                playerMovement.stopMoving = true;
                attacking = true;
                anim.SetTrigger("Attack");
                anim.SetBool("Attacking", true);
                AttackNumber++;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && targetMode && attacking && AttackNumber == 1)
            {
                playerMovement.stopMoving = true;
                anim.SetTrigger("Attack2");
                AttackNumber++;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && targetMode && attacking && AttackNumber == 2)
            {
                playerMovement.stopMoving = true;
                anim.SetTrigger("Attack3");
                AttackNumber++;
            }

            if (AttackNumber >= 3)
            {
                AttackNumber = 0;
            }

            if (!targetMode)
            {
                playerMovement.stopMoving = false;
                ResetAttack();
            }

            if (attacking)
            {
                currentTimeToChangeAnim += Time.deltaTime;

                if (currentTimeToChangeAnim >= timeLimit)
                {
                    playerMovement.stopMoving = false;
                    ResetAttack();
                }
            }
        }
    }

    //public void attack()
    //{
    //    int currentAttack;
    //    if (Input.GetKeyDown(KeyCode.Mouse0) && targetMode == true && attacking == false && playerMovement.isMoving == false)
    //    {
    //        attacking = true;
    //        currentAttack = UnityEngine.Random.Range(1, 3 + 1);
    //        anim.SetTrigger("Attack" + currentAttack.ToString());
    //        anim.SetBool("Attacking", true);
    //    }

    //    if (attacking == true)
    //    {
    //        if (currentTimeToChangeAnim < timeLimit)
    //        {
    //            currentTimeToChangeAnim += Time.deltaTime;
    //        }
    //        else
    //        {
    //            attacking = false;
    //            anim.SetBool("Attacking", false);
    //            currentTimeToChangeAnim = 0;
    //        }
    //    }
    //}

    private void ResetAttack()
    {
        attacking = false;
        anim.SetBool("Attacking", attacking);
        AttackNumber = 0;
        currentTimeToChangeAnim = 0;
    }

    private void ItemHandler()
    {
        //if slot 1 is full and player wanted to use it
        if (itemManager.numberOfHealthP > 0 && Input.GetKeyDown(KeyCode.Alpha1) && playerHealth < 100 && !forceField.shieldIsActive)
        {
            sfx.PlayhealSfx();
            float totalHealthValue = playerHealth + healthPotionValue;
            healParticles.Play(); //Play Particles of Restoring Health
            //this is to avoid the character having more than 100 health
            if (totalHealthValue > 100)
            {
                playerHealth = 100;
            }
            else
            {
                playerHealth += healthPotionValue;
            }
            //hide slot if slot is zero, else dont hide but subtract the quantity
            if (itemManager.numberOfHealthP <= 0)
            {
                itemManager.HideItemSlot(0);
            }
            else
            {
                itemManager.numberOfHealthP--;
            }
        } //if slot 2 is full and player wants to use it
        else if (itemManager.numberOfManaP > 0 && Input.GetKeyDown(KeyCode.Alpha2) && playerMana < 100)
        {
            sfx.PlayManaSfx();
            float totalManaValue = playerMana + manaPotionValue;
            manaParticles.Play(); //Play Particles of Restoring Mana
            if (totalManaValue > 100)
            {
                playerMana = 100;
            }
            else
            {
                playerMana += manaPotionValue;
            }
            if (itemManager.numberOfManaP <= 0)
            {
                itemManager.HideItemSlot(1);
            }
            else
            {
                itemManager.numberOfManaP--;
            }
        }
    }
    //take damage from enemy
    public void damagePlayer(float damage, bool ignoreShield)
    {
        if (!tutorial)
        {
            if(!forceField.shieldIsActive || ignoreShield)
            {
                tookDamage = true;
                anim.SetTrigger("hit");
                playerHealth -= damage;
                disableSenses();
                Time.timeScale = 1;
            }
        }
    }

    void ShowDamageFeedback()
    {
        if (tookDamage && currentIndicatorTime < damageIndicatorDuration)
        {
            currentIndicatorTime += Time.deltaTime;
            damageIndicator.SetActive(true);
        }
        else if (currentIndicatorTime > damageIndicatorDuration)
        {
            tookDamage = false;
            damageIndicator.SetActive(false);
            currentIndicatorTime = 0;
        }

        else if (playerHealth <= 25)
        {
            damageIndicator.SetActive(true);
            damageIndicatorAnimation.SetBool("LowHealth", true);
        }
        else
        {
            damageIndicator.SetActive(false);
            damageIndicatorAnimation.SetBool("LowHealth", false);
        }
    }
    public void damagePlayer2(float damage)
    {
        if (!tutorial)
        {
            playerHealth -= damage;
            disableSenses();
            Time.timeScale = 1;
        }
    }

    //get player health
    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    //get player mana
    public float GetPlayerMana()
    {
        return playerMana;
    }

    public void ReducePlayerMana(float value)
    {
        playerMana -= value;
    }

    public void SetPlayerMana(float value)
    {
        playerMana += value;
    }

    public void SetPlayerHealth(float health)
    {
        playerHealth = health;
    }


    public void enableSenses()
    {
        awarenessUI.SetActive(true);
        sensesEnabled = true;
    }

    public void disableSenses()
    {
        awarenessUI.SetActive(false);
    }

    private void DisablingDamageIndicator()
    {
        if (sensesEnabled && currentDmgIndTime < damageIndicatorMaxTime)
        {
            currentDmgIndTime += Time.deltaTime;
        }
        else
        {
            currentDmgIndTime = 0;
            disableSenses();
        }
    }

    //collisions handler

    public void RollCamera()
    {
        targetMode = false;
        rolled = true;
        cam.m_YAxis.m_MaxSpeed = 0;
        cam.m_XAxis.m_MaxSpeed = 0;
        targetSight.SetActive(false);

        if (midRig.m_TrackedObjectOffset.x > 0.2371475f)
        {
            midRig.m_TrackedObjectOffset.x -= 10f * Time.deltaTime;
        }
        if (cam.m_Lens.FieldOfView < 30)
        {
            cam.m_Lens.FieldOfView += 80f * Time.deltaTime;
        }
    }

    private void StopAllAudio()
    {
        if (!enableAudioSource)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.Stop();
            }
        }
    }

    public void ResetPlayer()
    {
        enableAudioSource = false;
        sfx.defeatedSfxPlayed = false;
        anim.SetBool("Dead", false);
        Time.timeScale = 0f;
    }
}

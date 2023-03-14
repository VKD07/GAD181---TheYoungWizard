using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] int playerHealth;
    Player_Movement playerMovement;

    [Header("Character Animation")]
    [SerializeField] public Animator anim;
    bool paused = false;

    [Header("Cast Mode")]
    [SerializeField] GameObject castUI;
    [SerializeField] CastModeManager castModeManager;
    [SerializeField] Animator spellCastUIAnim;

    [Header("Spell Cast")]
    [SerializeField] SpellSlot spellManager;
    [HideInInspector] public bool casting = false;
    public bool castingSpell = false;
 
    [Header("Aim Mode")]
    CinemachineComposer midRig;
    [SerializeField] CinemachineFreeLook cam;
    [SerializeField] GameObject targetSight;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] LayerMask layerMask;
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

    

    //Awareness 
    [SerializeField] GameObject awarenessUI;

    public bool dodge = false;
    public float shieldDuration = 3f;

    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        //pause game
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            paused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
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

    }

    private void SpellCastAnimation()
    {
        //casting ice
        if (castModeManager.availableSpellID == 30 && spellManager.iceCooldown == false && castModeManager.castingMode == false
            && playerMovement.rolling == false && castingSpell == false && Input.GetKeyDown(KeyCode.E))
        {
            castingSpell = true;
            anim.SetTrigger("IceSpell");
        }

        //casting a fireball
        if (castModeManager.availableSpellID == 55 && spellManager.fireBallCoolDown == false 
            && castModeManager.castingMode == false && playerMovement.rolling == false
            && castingSpell == false && Input.GetKeyDown(KeyCode.E))
        {
            castingSpell = true;
            anim.SetTrigger("FireBall");
        }

        //casting wind gust
        if (castModeManager.availableSpellID == 35 && spellManager.windGustCoolDown == false
            && castModeManager.castingMode == false && playerMovement.rolling == false
            && castingSpell == false && Input.GetKeyDown(KeyCode.E))
        {
            castingSpell = true;
            anim.SetTrigger("WindGust");
        }

        //luminous spell
        //casting wind gust
        if (castModeManager.availableSpellID == 40 && spellManager.sparkCoolDown == false
            && castModeManager.castingMode == false && playerMovement.rolling == false
            && castingSpell == false && Input.GetKeyDown(KeyCode.E))
        {
            castingSpell = true;
            anim.SetTrigger("Luminous");
        }

    }

    //player has shield while rolling
    private void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            dodge = true;
        }

        if (dodge == true && shieldDuration > 0)
        {
            disableSenses();
            Time.timeScale = 1;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (castUI.activeSelf == false)
            {
                spellCastUIAnim.SetBool("CastMode", true);
                casting = true;
                //activate cast mode UI
                castUI.SetActive(true);
                //slow down time
                Time.timeScale = 0.1f;
            }
        }
    }

    private void aimMode()
    {
        //sight activated
        if (Input.GetKey(KeyCode.Mouse1) && rolled == false && playerMovement.rolling == false)
        {
            targetMode = true;
            targetSight.SetActive(true);
            
            if (midRig.m_TrackedObjectOffset.x < 0.95f)
            {
                midRig.m_TrackedObjectOffset.x += 5f * Time.deltaTime;
            }

            if (cam.m_Lens.FieldOfView > 20)
            {
                cam.m_Lens.FieldOfView -= 60f * Time.deltaTime;
            }

        }
        else if(Input.GetKeyUp(KeyCode.Mouse1) || castingSpell == false)
        {
            targetMode = false;
            targetSight.SetActive(false);

            if (midRig.m_TrackedObjectOffset.x > 0.2371475f)
            {
                midRig.m_TrackedObjectOffset.x -= 5f * Time.deltaTime;
            }
            if (cam.m_Lens.FieldOfView < 30)
            {
                cam.m_Lens.FieldOfView += 60f * Time.deltaTime;
            }

        }

    }

    public void attack()
    {

        // if reticle hits player then dont attack
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if the player is moving then dont proceed to the attacking combo
            if (playerMovement.isMoving == true && Input.GetKeyDown(KeyCode.Mouse0) && targetMode == true)
            {
                anim.SetTrigger("Attack");
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && targetMode == true && AttackNumber == 0 && attacking == false)
                {
                    attacking = true;
                    anim.SetTrigger("Attack");
                    anim.SetBool("Attacking", true);

                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && targetMode == true && AttackNumber == 1)
                {
                    anim.SetTrigger("Attack2");
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && targetMode == true && AttackNumber == 2)
                {
                    anim.SetTrigger("Attack3");
                }
            }
        

        if (AttackNumber == 3)
        {
            AttackNumber = 0;
        }

        //attack animation resets if aiming mode is disabled
        if(targetMode == false)
        {
            attacking = false;
            currentTimeToChangeAnim = 0;
            AttackNumber = 0;
        }

        //Timer to switch to second animation
        //If player Starts to attack then start timer
        if (attacking == true)
        {
            //if current time doesnt reeach the time limit. Keep counting
            if(currentTimeToChangeAnim < timeLimit)
            {
                currentTimeToChangeAnim = currentTimeToChangeAnim + Time.deltaTime;
            }
            //if current time reached the time, then reset the animation to first attack
            if(currentTimeToChangeAnim > timeLimit)
            {
                attacking = false;
                anim.SetBool("Attacking", attacking);
                AttackNumber = 0;
                currentTimeToChangeAnim = 0;
            }
            //if the time hasn't reached yet and player pressed left click again, change the attack number
            if (currentTimeToChangeAnim < timeLimit && Input.GetKeyDown(KeyCode.Mouse0))
            {
                AttackNumber++;
            }
        }

        //base layer adjustments
        if (playerMovement.isMoving == false && Input.GetKeyDown(KeyCode.Mouse0) && targetMode == true)
        {
            anim.SetBool("notAttacking", false);
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 1);
        }
        else if (playerMovement.isMoving == true)
        {
            anim.SetBool("notAttacking", true);
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0.4f);
        }
    }

    private void ItemHandler()
    {
        //if slot 1 is full and player wanted to use it
        if (itemManager.isFull[0] == true && Input.GetKeyDown(KeyCode.Alpha1))
        {
            itemManager.isFull[0] = false;
            itemManager.itemSlots[0].sprite = null;

            //if slot 2 is full and player wants to use it
        }
        else if (itemManager.isFull[1] == true && Input.GetKeyDown(KeyCode.Alpha2))
        {
            itemManager.isFull[1] = false;
            itemManager.itemSlots[1].sprite = null;
        }

    }

    //take damage from enemy
    public void damagePlayer(int damage)
    {
        if (dodge == false)
        {
            playerHealth -= damage;
            disableSenses();
            Time.timeScale = 1;
        }

    }


    public void enableSenses()
    {
        awarenessUI.SetActive(true);
    }

    public void disableSenses()
    {
        awarenessUI.SetActive(false);
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
}

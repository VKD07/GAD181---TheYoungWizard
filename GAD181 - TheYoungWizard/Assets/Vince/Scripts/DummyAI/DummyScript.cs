using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DummyScript : MonoBehaviour
{
    Animator animator;
    [Header("Spell Components")]
    [SerializeField] GameObject spellImageUI;
    [SerializeField] Image spellSlot;
    [SerializeField] int[] spellIDs;
    [SerializeField] Sprite[] spellIcons;
    public Player_SpellCast spell;
    public int spellChosenIndex;
    GameObject castUI;
    bool enteredWindRange = false;

    [Header("Dummy Settings")]
    [SerializeField] Transform player;
    [SerializeField] float attackRange = 2f;
    [SerializeField] int attackDamage = 5;
    playerCombat pc;
    NavMeshAgent ai;
    bool isAlive = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
        ai.stoppingDistance = attackRange;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCombat>();
        spell = GameObject.Find("KaelModel").GetComponent<Player_SpellCast>();
        chooseASpell();
        randomSpeed();
    }

    private void randomSpeed()
    {
        ai.speed = Random.Range(2, 4);
    }

    private void Update()
    {
        ChasePlayer();
        WindGustEffect();
    }

    void chooseASpell()
    {
        spellImageUI.SetActive(true);

        //choose a spell
        spellChosenIndex = Random.Range(0, spellIDs.Length);
        if (spellChosenIndex == 4)
        {
            return;
        }
        else
        {
            spellSlot.sprite = spellIcons[spellChosenIndex];
        }

    }

    private void ChasePlayer()
    {
        if (isAlive == true)
        {

            player = GameObject.FindGameObjectWithTag("Player").transform;


            //dummy looking at the player always

            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            ai.SetDestination(player.position);

            //calculate distance
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else if (pc.casting == false)
            {
                Time.timeScale = 1f;
                pc.disableSenses();
                animator.SetBool("Attacking", false);
            }
        }

    }

    private void Attack()
    {
        animator.SetBool("Attacking", true);
    }

    private void WindGustEffect()
    {
        if (spellChosenIndex == 1)
        {
            gameObject.tag = "windEnemy";
        }
        if (enteredWindRange == true && spell.releaseWind == true)
        {
            ai.enabled = false;
            DeathState();
        }
    }

    private void DamagePlayer()
    {
        pc.damagePlayer(attackDamage);
    }

    private void DamageIndicator()
    {
        Time.timeScale = 0.5f;
        pc.enableSenses();
    }


    private void OnTriggerEnter(Collider collision)
    {
        //dummy gets destroyed depends on the chosen spells
        if (spellChosenIndex == 0 && collision.tag == "Fireball")
        {
            DeathState();

        }
        else if (spellChosenIndex == 2 && collision.tag == "frostWall")
        {
            DeathState();
        }
        else if (spellChosenIndex == 3 && collision.tag == "luminous")
        {
            DeathState();
        }
        else if (spellChosenIndex == 4 && collision.tag == "playerBullet")
        {
            DeathState();
        }
        if (spellChosenIndex == 1 && collision.tag == "windGust")
        {
            print("windRange");
            enteredWindRange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        //dummy gets destroyed depends on the chosen spells
        if (spellChosenIndex == 0 && other.tag == "Fireball")
        {
            DeathState();
        }
        else if (spellChosenIndex == 2 && other.tag == "frostWall")
        {
            DeathState();
        }
        else if (spellChosenIndex == 3 && other.tag == "luminous")
        {
            DeathState();
        }
        if (spellChosenIndex == 1 && other.tag == "windGust")
        {
            print("windRange");
            enteredWindRange = true;
        }
    }

    void DeathState()
    {
        pc.disableSenses();
        animator.SetTrigger("Dead");
        Destroy(gameObject, 1);
        isAlive = false;
    }
}

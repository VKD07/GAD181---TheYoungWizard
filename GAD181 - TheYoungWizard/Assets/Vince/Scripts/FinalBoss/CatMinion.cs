using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CatMinion : MonoBehaviour
{
    [Header("Minion Settings")]
    [SerializeField] GameObject player;
    [SerializeField] float minionHealth = 50f;
    [SerializeField] float minionDamage = 5f;
    [SerializeField] float minionSpeed = 4f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackInterval = 5f;
    [SerializeField] float jumpForce;
    [SerializeField] Transform rayCastPoint;
    [Header("Minion Settings")]
    [SerializeField] Slider healthSlider;
    public float currentTime;
    float jumpTime = 2f;
    float jumpTimeCurrentTime;
    bool jump;
    Animator anim;
    bool playerInRange;
    NavMeshAgent ai;
    RaycastHit hit;
    Rigidbody rb;
    private void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ai.speed = minionSpeed;
        ai.stoppingDistance = attackRange;
        healthSlider.maxValue = minionHealth;
    }
    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        UpdateHealth();
        JumpToPlayer();
    }

    private void UpdateHealth()
    {
        healthSlider.value = minionHealth;
    }

    private void ChasePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange)
        {
           transform.LookAt(player.transform.position);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
            if (Physics.Raycast(rayCastPoint.position, rayCastPoint.forward, out hit, attackRange, LayerMask.GetMask("Player")))
            {
                AttackPlayer();
            }
            else
            {
                playerInRange = false;
            }
        }
        else if (distanceToPlayer > attackRange || playerInRange == false)
        {
            currentTime = 0;
            ai.SetDestination(player.transform.position);
            anim.SetBool("Idle", false);
        }
        Debug.DrawRay(rayCastPoint.position, rayCastPoint.forward * attackRange, Color.red);
    }

    private void AttackPlayer()
    {
        if (currentTime < attackInterval)
        {
            anim.SetBool("Idle", true);
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            anim.SetBool("Idle", false);
            anim.SetTrigger("Attack");
        }
        playerInRange = true;
    }

    void JumpToPlayer()
    {
        if (jump == true && jumpTimeCurrentTime < jumpTime)
        {
            jumpTimeCurrentTime += Time.deltaTime;
            rb.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
        } else if (jumpTimeCurrentTime > jumpTime || jump == false)
        {
            jumpTimeCurrentTime = 0;
            jump = false;
            rb.isKinematic = true;
        }
    }

    void Jump()
    {
        jump = true;
        rb.isKinematic = false;
    }

    void DisableJump()
    {
        jump = false;
        player.GetComponent<playerCombat>().disableSenses();
    }

    void enablePlayerSenses()
    {
        player.GetComponent<playerCombat>().enableSenses();
    }

    void DamagePlayer()
    {
        if (playerInRange)
        {
            if (playerInRange == true)
            {
                player.GetComponent<playerCombat>().damagePlayer(minionDamage);
            }
        }
    }

    public void DamageMinion(float damage)
    {
        if(minionHealth <= 0)
        {
            anim.SetTrigger("Dead");
            healthSlider.gameObject.SetActive(false);
            ai.enabled= false;
            this.enabled = false;
            Destroy(gameObject, 2f);
        }
        else
        {
            minionHealth -= damage;
        }
    }
}
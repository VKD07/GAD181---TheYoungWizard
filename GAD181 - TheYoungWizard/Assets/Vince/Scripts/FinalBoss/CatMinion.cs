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
    [SerializeField] Transform rayCastPoint;
    [Header("Minion Settings")]
    [SerializeField] Slider healthSlider;
    Animator anim;
    bool playerInRange;
    NavMeshAgent ai;
    RaycastHit hit;

    private void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ai.speed = minionSpeed;
        healthSlider.maxValue = minionHealth;
    }
    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        UpdateHealth();
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
                anim.SetTrigger("Attack");
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }
        }
        else if (distanceToPlayer > attackRange)
        {
            ai.SetDestination(player.transform.position);
        }
        Debug.DrawRay(rayCastPoint.position, rayCastPoint.forward * attackRange, Color.red);
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
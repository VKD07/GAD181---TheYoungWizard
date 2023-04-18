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
    [SerializeField] float attackInterval;
    [SerializeField] float jumpForce;
    [SerializeField] Transform rayCastPoint;
    [SerializeField] LayerMask layerMask;
    [Header("Minion Settings")]
    [SerializeField] Slider healthSlider;
    public float distanceToPlayer;
    public float currentTime;
    float jumpTime = 2f;
    float jumpTimeCurrentTime;
    bool jump;
    bool attacking;
    Animator anim;
    bool playerInRange;
    NavMeshAgent ai;
    RaycastHit hit;
    Rigidbody rb;
    GameObject playerForceField;

    [Header("VFX")]
    [SerializeField] GameObject shieldBlock;

    private void Start()
    {
        attackInterval = UnityEngine.Random.Range(3, 8);
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
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange)
        {
            transform.LookAt(player.transform.position);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
            if (Physics.Raycast(rayCastPoint.position, rayCastPoint.forward, out hit, attackRange, layerMask))
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
            attackInterval = UnityEngine.Random.Range(3, 8);
            currentTime = 0;
            anim.SetBool("Idle", false);
            anim.SetTrigger("Attack");
        }
        playerInRange = true;
    }

    private void PlayerShield()
    {
        if (hit.rigidbody != null && hit.rigidbody.gameObject != null && hit.rigidbody.gameObject.tag == "PlayerForceField")
        {
            GameObject explosionObj = Instantiate(shieldBlock, hit.point, Quaternion.identity);
            Destroy(explosionObj, 1f);
        }
    }


    void JumpToPlayer()
    {
        if (jump == true && jumpTimeCurrentTime < jumpTime)
        {
            jumpTimeCurrentTime += Time.deltaTime;
            if (distanceToPlayer > 2)
            {
                rb.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
            }
        }
        else if (jumpTimeCurrentTime > jumpTime || jump == false)
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
                player.GetComponent<playerCombat>().damagePlayer(minionDamage, false);
            }
        }
    }

    public void DamageEnemy(float damage)
    {
        if (minionHealth <= 0)
        {
            anim.SetBool("Die", true);
            healthSlider.gameObject.SetActive(false);
            ai.enabled = false;
            this.enabled = false;
            Destroy(gameObject, 2f);
        }
        else
        {
            minionHealth -= damage;
        }
    }

    void Freeze()
    {
        ai.enabled = false;
        anim.enabled = false;
    }
    void UnFreeze()
    {
        ai.enabled = true;
        anim.enabled = true;
    }
}
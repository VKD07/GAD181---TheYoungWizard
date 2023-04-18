using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class spiderScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject raycastOrigin;
    [SerializeField] float rayCastLength = 5f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float spiderDamage = 10f;
    [SerializeField] GameObject playerShieldExplosion;
    GameObject player;
    [SerializeField] Player playerScript;
    NavMeshAgent agent;
    [SerializeField] float spiderHp;
    [SerializeField] float attackTime;
    public float currentAttackTime;
    bool jump;
    Rigidbody rb;
    public float distanceToPlayer;
    float randomTimeToAttack;
    RaycastHit hit;

    //slider
    [SerializeField] Slider slider;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        slider.maxValue = spiderHp;
        slider.value = spiderHp;
        randomTimeToAttack = UnityEngine.Random.Range(3,7);
        attackTime = randomTimeToAttack;
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (playerScript == null)
        {

            // Look at the player
            transform.LookAt(player.transform.position);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, currentRotation.y, currentRotation.z);
            chase();
        }

        Death();

        UpdateHealthSlider();

    }

    private void UpdateHealthSlider()
    {
        slider.value = spiderHp;
    }

    private void Death()
    {
        if (spiderHp <= 0)
        {
            FindObjectOfType<EnemyDeathCounter>().EnemyDeath++;
            animator.SetTrigger("Death");
            //  healthSlider.gameObject.SetActive(false);
            agent.enabled = false;
            this.enabled = false;
            Destroy(gameObject, 2f);
        }
    }

    void attack()
    {

        if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, out hit, rayCastLength, layerMask))
        {
            attackTime = randomTimeToAttack;
            if (currentAttackTime < attackTime)
            {
                currentAttackTime += Time.deltaTime;
            }
            else if (currentAttackTime >= attackTime)
            {
                randomTimeToAttack = UnityEngine.Random.Range(3, 7);
                animator.SetTrigger("Attack");
                currentAttackTime = 0;
            }
        }
        else
        {
            playerScript = null;
        }
        Debug.DrawLine(raycastOrigin.transform.position, raycastOrigin.transform.forward * 1f, Color.red);
    }
    void chase()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > agent.stoppingDistance)
        {
            animator.SetBool("Run", true);
            agent.SetDestination(player.transform.position);
        }
        else
        {
            attack();
            animator.SetBool("Run", false);
        }

    }

    public void WarnPlayer()
    {
        if (player != null)
        {
            player.GetComponent<playerCombat>().enableSenses();
        }
    }

    public void DisableWarnPlayer()
    {
        if (player != null)
        {
            player.GetComponent<playerCombat>().disableSenses();
        }
    }

    public void Jump()
    {
        if (player != null)
        {
            if (distanceToPlayer > 3)
            {
                transform.position += transform.forward * 3f; //jump
                player.GetComponent<playerCombat>().damagePlayer(spiderDamage, false);
            }
        }


    }

    public void playerShield()
    {
        if (hit.transform.name == "Player_ForceField")
        {
            GameObject explosion = Instantiate(playerShieldExplosion, hit.point, Quaternion.identity);
            Destroy(explosion, 1f);
        }
    }

    public void resetPos()
    {
        transform.position -= transform.forward * 2f;
    }

    public void DamageEnemy(float damage)
    {
        spiderHp -= damage;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class SkeletonScript : MonoBehaviour
{

    private Animator animator;
    [SerializeField] GameObject raycastOrigin;
    [SerializeField] float rayCastLength = 5f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float skeletonDamage = 10f;
    [SerializeField] GameObject playerShieldExplosion;
    GameObject player;
    [SerializeField] Player playerScript;
    NavMeshAgent agent;
    [SerializeField] float spiderHp;
    [SerializeField] float attackTime;
    [SerializeField] BoxCollider bossTriggerBox;
    public float currentAttackTime;
    bool jump;
    Rigidbody rb;
    public float distanceToPlayer;
    float randomTimeToAttack;
    RaycastHit hit;
    [SerializeField] float idleDistance = 30f;
    AudioSource audioSource;
    [SerializeField] AudioClip swingSword;
    [SerializeField] AudioClip footStep;
    float initHp;
    //slider
    [SerializeField] Slider slider;
    //unlock spell
    [SerializeField] GameObject runeStone;

    bool attacking;
    bool shieldExploded;


    void Awake()
    {
        initHp = spiderHp;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        slider.maxValue = spiderHp;
        slider.value = spiderHp;
        randomTimeToAttack = UnityEngine.Random.Range(3, 7);
        attackTime = randomTimeToAttack;
    }

    private void OnEnable()
    {
        spiderHp = initHp;
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
        playerShield();
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
            runeStone.SetActive(true);
            FindObjectOfType<EnemyDeathCounter>().EnemyDeath++;
            animator.SetTrigger("Dead");
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
                animator.SetTrigger("attack");
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
            animator.SetBool("walk", true);
            agent.SetDestination(player.transform.position);
        }
        else
        {
            attack();
            animator.SetBool("walk", false);
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
            }
            player.GetComponent<playerCombat>().damagePlayer(skeletonDamage, false);

        }


    }

    public void playerShield()
    {
        if (attacking && hit.transform.name == "Player_ForceField" && !shieldExploded)
        {
            shieldExploded = true;
            GameObject explosion = Instantiate(playerShieldExplosion, hit.point, Quaternion.identity);
            Destroy(explosion, 1f);
        }
    }

    public void resetPos()
    {
        transform.position -= transform.forward * 3f;
    }

    public void DamageEnemy(float damage)
    {
        spiderHp -= damage;
    }

    public void PlaySwing()
    {
        audioSource.PlayOneShot(swingSword);
    }

    public void PlayFootStep()
    {
        audioSource.PlayOneShot(footStep);
    }

    public void Attacking()
    {
        attacking = true;
    }

    public void NotAttacking()
    {
        attacking = false;
        shieldExploded = false;
    }
}

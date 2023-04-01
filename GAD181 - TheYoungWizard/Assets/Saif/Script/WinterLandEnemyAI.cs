using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WinterLandEnemyAI : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] float currentHealth;
    [SerializeField] Slider slider;
    float maxHealth;
    public GameObject thatPlayer;
    public float attackDamage;
    public Transform player;
    public LayerMask thePlayer;
    public LayerMask theGround;
    Animator animEnemy;
    public float sightRange;
    public float attackRange;
    public bool playerSightRange;
    public bool playerAttackRange;
    private void Start()
    {
        player = GameObject.Find("Player(In-Game)").transform;
        agent = GetComponent<NavMeshAgent>();
        animEnemy = GetComponent<Animator>();
        slider.maxValue = currentHealth;
        maxHealth = currentHealth;

    }
    public void DamageEnemy(float playerDamage)
    {
        currentHealth -= playerDamage;
    }
    private void EnemyChase()
    {
        
        agent.SetDestination(player.position);
        transform.LookAt(player);
        animEnemy.SetBool("Run Forward", true);
    }
    private void EnemyDeath()
    {
        agent.SetDestination(transform.position);
        animEnemy.SetBool("Death",true);
        Destroy(gameObject, 3);


    }

    private void EnemyAttack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        
        animEnemy.SetTrigger("Attack1");
        animEnemy.SetTrigger("Attack2");
        animEnemy.SetBool("Run Forward", false);
    }

    private void EnemyDealDamage()
    {
        thatPlayer.GetComponent<playerCombat>().damagePlayer(attackDamage);
    }
    private void UpdateEnemyHealth()
    {
        slider.value = currentHealth;
    }



    private void Update()
    {
        playerSightRange = Physics.CheckSphere(transform.position, sightRange, thePlayer);
        playerAttackRange = Physics.CheckSphere(transform.position, attackRange, thePlayer);
        UpdateEnemyHealth();


        if (playerSightRange && !playerAttackRange || currentHealth < maxHealth)
        {   
            EnemyChase();       
        }

        if (playerSightRange && playerAttackRange)
        {
            EnemyAttack();
        }


        if (currentHealth <= 0)
        {
            EnemyDeath();
        }

    }

}

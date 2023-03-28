using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WinterEnemyAI : playerCombat
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask thePlayer, theGround;
    Animator animEnemy;


    public float timeBetweenAttacks;
   

    public float sightRange, attackRange;
    public bool playerSightRange, playerAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player(In-Game)").transform;
        agent = GetComponent<NavMeshAgent>();
        animEnemy = GetComponent<Animator>();
        
    }
    private void EnemyChase()
    {
        agent.SetDestination(player.position);
        
    }
    private void EnemyAttack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        animEnemy.SetTrigger("Attack1");
        animEnemy.SetTrigger("Attack2");

    }

    private void Update()
    {
        playerSightRange = Physics.CheckSphere(transform.position, sightRange, thePlayer);
        playerAttackRange = Physics.CheckSphere(transform.position, attackRange, thePlayer);


        if (playerSightRange && !playerAttackRange)
        {
            EnemyChase();
            animEnemy.SetBool("Run Forward", true);

        }
        if (playerSightRange && playerAttackRange)
        {
            EnemyAttack();
            animEnemy.SetBool("Run Forward", false);


        }

        if (!playerSightRange && !playerAttackRange)
        {
            animEnemy.SetBool("Run Forward", false);
        }

    }

}

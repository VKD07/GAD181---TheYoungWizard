using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WinterEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask thePlayer, theGround;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerSightRange, playerAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player(InRoomScene)").transform;
        agent = GetComponent<NavMeshAgent>();

    }
    private void EnemyPatroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }


    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(walkPointRange, walkPointRange);
        float randomX = Random.Range(walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, theGround))
        {
            walkPointSet = true;
        }
    }
    private void EnemyChase()
    {
        agent.SetDestination(player.position);
    }
    private void EnemyAttack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Update()
    {
        playerSightRange = Physics.CheckSphere(transform.position, sightRange, thePlayer);
        playerAttackRange = Physics.CheckSphere(transform.position, attackRange, thePlayer);
        
        if(!playerSightRange && !playerAttackRange)
        {
            EnemyPatroling();
        }

        if (playerSightRange && !playerAttackRange)
        {
            EnemyChase();
        }
        if (playerSightRange && playerAttackRange)
        {
            EnemyAttack();
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class WinterLandEnemyAI : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    public GameObject thatPlayer;
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
        thatPlayer.GetComponent<playerCombat>();

    }
    private void EnemyChase()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);

    }
    private void EnemyAttack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        thatPlayer.GetComponent<playerCombat>().damagePlayer(5f);
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
            
            animEnemy.SetTrigger("Attack1");
            animEnemy.SetTrigger("Attack2");
            animEnemy.SetBool("Run Forward", false);
        }

        if (!playerSightRange && !playerAttackRange)
        {
            animEnemy.SetBool("Run Forward", false);
        }

    }

}

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
    Animator animEnemy;
    public float sightRange;
    public float attackRange;
    public bool playerSightRange;
    public bool playerAttackRange;
    public Vector3 playerlastposition;
    float timer = 0f;
    float playerHP;
    float playerDistance;


    private void Start()
    {
        player = GameObject.Find("Player(In-Game)").transform;
        agent = GetComponent<NavMeshAgent>();
        animEnemy = GetComponent<Animator>();
        slider.maxValue = currentHealth;
        maxHealth = currentHealth;
        thatPlayer = GameObject.Find("Player(In-Game)");
 



    }
    public void DamageEnemy(float playerDamage)
    {
        currentHealth -= playerDamage;
    }

    public void LookAtPlayer(bool playerLookAt)
    {
        if (currentHealth > 0)
        {
            playerlastposition = player.position;
        }
        if (playerLookAt == true) 
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            
        }
        else if (playerLookAt == false)
        {
            transform.LookAt(new Vector3(playerlastposition.x, transform.position.y, playerlastposition.z));  
        }
    }
    private void EnemyChase(bool playerSeen)
    {
        if(playerSeen == true) 
        { 
        agent.SetDestination(player.position);
        
        animEnemy.SetBool("Run Forward", true);
        }
    }
    private void EnemyDeath()
    {
        GetComponent<BoxCollider>().enabled = false;
        animEnemy.ResetTrigger("Attack2");
        animEnemy.ResetTrigger("Attack1");
        animEnemy.SetBool("Run Forward", false);
        agent.SetDestination(transform.position);
        animEnemy.SetBool("Death",true);
        Destroy(gameObject, 3);
    }

    private void EnemyAttack()
    {
        agent.SetDestination(transform.position);
        animEnemy.SetTrigger("Attack1");
        animEnemy.SetTrigger("Attack2");
        animEnemy.SetBool("Run Forward", false);
    }



    private void EnemyDealDamage()
    {
        thatPlayer.GetComponent<playerCombat>().damagePlayer(attackDamage, false);
    }
    private void UpdateEnemyHealth()
    {
        slider.value = currentHealth;
    }



    private void Update()
    {
        timer += 1f * Time.deltaTime;
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        UpdateEnemyHealth();
        
        if (currentHealth > 0)
        {
            if (playerDistance < sightRange || currentHealth < maxHealth)
            {
                 LookAtPlayer(true);
                 EnemyChase(true);
                if (timer > 2)
                {
                    GetComponent<WinterEnemySounds>().PlayChaseSound();
                    timer = 0;
                }

            }
            if (playerDistance < attackRange)
            {
                EnemyChase(false);
                EnemyAttack();
                LookAtPlayer(true);
                timer = 0;
            }
        }



        else if (currentHealth <= 0)
        {
            LookAtPlayer(false);
            EnemyDeath();

        }

        playerHP = thatPlayer.GetComponent<playerCombat>().GetPlayerHealth();
        if (playerHP == 0)
        {
            animEnemy.SetTrigger("PlayerDead");
            agent.SetDestination(transform.position);
        }

    }

}

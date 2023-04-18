using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LichWinterLandAI : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] float currentHealth;
    [SerializeField] Slider slider;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;
    public bool playerAttackRange;
    float maxHealth;
    public GameObject thatPlayer;
    public Transform player;
    public LayerMask thePlayer;
    public Animator animEnemy;
    public float sightRange;
    public float attackRange;
    public bool playerSightRange;
    public Vector3 playerlastposition;
    public float wolfSpawnTimer = 0f;
    public float wolfTimeSpawn;
    float playerDistance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animEnemy = GetComponent<Animator>();
        slider.maxValue = currentHealth;
        maxHealth = currentHealth;
        thatPlayer = GameObject.FindGameObjectWithTag("Player");
        wolfSpawnTimer = 15;


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
    private void EnemyChase()
    {
        animEnemy.SetBool("Idle", false);
        animEnemy.SetBool("Walk", true);
        agent.SetDestination(player.position);

    }
    private void EnemyDeath()
    {

        agent.SetDestination(transform.position);
        animEnemy.SetTrigger("Dead");
        Destroy(gameObject, 3);


    }
    private void EnemyAttack()
    {
        
        animEnemy.SetTrigger("Attack");
        animEnemy.SetBool("Walk", false);

    }





    private void UpdateEnemyHealth()
    {
        slider.value = currentHealth;
    }



    private void Update()
    {

        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        UpdateEnemyHealth();
 
        if (currentHealth > 0)
        {
            if (sightRange > playerDistance || currentHealth < maxHealth)
            {
                wolfSpawnTimer += 1 * Time.deltaTime;
                if (wolfSpawnTimer > wolfTimeSpawn)
                {
                  GetComponent<WolfSummoner>().CallWolf();
                    wolfSpawnTimer = 0;
                }
                
                LookAtPlayer(true);
                EnemyChase();

            }
            if (playerDistance < attackRange)
            {
                
                EnemyAttack();
                LookAtPlayer(true);
            }

        }

        else if (currentHealth <= 0)
        {
            LookAtPlayer(false);
            EnemyDeath();

        }


    }

    void ReleaseBullet()
    {
        Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
    }

    void EnablePlayerSense()
    {
        player.GetComponent<playerCombat>().enableSenses();
    }

}



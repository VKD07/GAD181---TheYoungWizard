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

    float maxHealth;
    public GameObject thatPlayer;
    // public float attackDamage;
    public Transform player;
    public LayerMask thePlayer;
    public LayerMask theGround;
    Animator animEnemy;
    public float sightRange;
    public float attackRange;
    public bool playerSightRange;
    public bool playerAttackRange;
    public Vector3 playerlastposition;
    float timer = 0f;
    float playerHP;

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
    private void EnemyChase()
    {

        agent.SetDestination(player.position);

    }
    private void EnemyDeath()
    {

        agent.SetDestination(transform.position);
        animEnemy.SetTrigger("Dead");
        Destroy(gameObject, 3);


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

        if (currentHealth > 0)
        {
            if (playerSightRange && !playerAttackRange || currentHealth < maxHealth)
            {
                LookAtPlayer(true);
                EnemyChase();

            }
 
        }

        else if (currentHealth <= 0)
        {
            LookAtPlayer(false);
            EnemyDeath();

        }


    }

}



using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BossScript : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] float health = 500f;
    [SerializeField] float idleModeTime = 10f;
    public int numberOfSkills = 5;
    float halfHealth;

    [Header("Target")]
    [SerializeField] GameObject player;
    Vector3 playerLastPosition;
    public float currentTime;

    [Header("Attack number")]
    public int attackNumber;

    [Header("Projectile Mode")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] float projectileModeTime = 5f;

    [Header("Cast FireBall")]
    [SerializeField] GameObject fireBall;
    int randomFireBall;
    [SerializeField] GameObject fireBalls;
    [SerializeField] float fireBallsSpeed = 10f;
    [SerializeField] Transform[] multipleFireBallSpawners;

    [Header("Pounce")]
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float maxJumpHeight = 1f;
    [SerializeField] float stompSpeed = 35f;
    bool jumped;
    bool jumpedToPlayer;

    [Header("Cat Bite")]
    [SerializeField] float catBiteDuration = 20f;
    [SerializeField] float runSpeed = 2.5f;
    [SerializeField] float biteDamage = 10f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform rayCastPoint;
    RaycastHit hit;
    public float distanceToplayer;
    bool playerInRange;

    [Header("Spawn Minions")]
    [SerializeField] GameObject minions;
    [SerializeField] float numberOfMinionsSpawned = 3;
    [SerializeField] float healingRate;
    [SerializeField] float healingDuration = 8f;
    [SerializeField] GameObject iceShieldObj;
    public bool icedShield;
    bool minionsSpawned = false;

    [Header("Replicate Mode")]
    [SerializeField] GameObject bossClone;
    [SerializeField] float cloneDuration = 15f;

    //Components
    NavMeshAgent ai;
    Animator anim;


    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        ai.speed = runSpeed;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        halfHealth = health / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //easy stage if the health is not half
        //else additional skills if health is half
       
        print(health);
        if (health > halfHealth)
        {
            numberOfSkills = 5;
            Stage1Skills();
        }
        else
        {
            //increasing difficulty
            numberOfSkills = 7;
            idleModeTime = 2;
            runSpeed = 3.5f;
            stompSpeed = 30f;
            fireBallsSpeed = 700f;

            Stage1Skills();
            Stage2Skills();
        }
    }

    private void Stage1Skills()
    {
        //idle mode in the beginning
        // after the idle mode it will randomize a number to determine whats the next move
        //after every move it will go back to attack 0 which means idle mode
        //the process repeats
        if (attackNumber == 0)
        {
            IdleMode(true);
        }
        else
        {
            IdleMode(false);
        }

        if (attackNumber == 1)
        {
            ProjectTilemode(true);
        }
        else
        {
            ProjectTilemode(false);
        }

        if (attackNumber == 2)
        {
            SpawnFireball(true);
        }
        else
        {
            SpawnFireball(false);
        }

        if (attackNumber == 3)
        {
            Pounce(true);
        }
        else
        {
            Pounce(false);
        }

        if (attackNumber == 4)
        {
            CatBite(true);
        }
        else
        {
            CatBite(false);
        }

        Jump();
    }

    void Stage2Skills()
    {
        if (attackNumber == 5)
        {
            healingMode(true);
        }
        else
        {
            healingMode(false);
        }

        if (attackNumber == 6)
        {
            Replicate(true);
        }
        else
        {
            Replicate(false);
        }
    }


    //skills --------------------------------------------------------------------------------->

    void IdleMode(bool value)
    {
        // idle mode
        if (value == true)
        {
            if (currentTime < idleModeTime)
            {
                transform.LookAt(player.transform.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                attackNumber = UnityEngine.Random.Range(1, numberOfSkills);
            }

        }
    }

    void CatBite(bool value)
    {
        if (value == true)
        {
            if (currentTime < catBiteDuration)
            {
                currentTime += Time.deltaTime;
                distanceToplayer = Vector3.Distance(transform.position, player.transform.position);

                if (distanceToplayer <= attackRange)
                {
                    transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
                    if (Physics.Raycast(rayCastPoint.position, rayCastPoint.forward, out hit, attackRange, LayerMask.GetMask("Player")))
                    {
                        anim.SetTrigger("Attack");
                        playerInRange = true;
                    }
                    else
                    {
                        playerInRange = false;
                    }
                }
                else if (distanceToplayer > attackRange)
                {
                    anim.SetBool("Run", true);
                    ai.SetDestination(player.transform.position);
                    print("Running");
                }
            }
            else
            {
                ai.SetDestination(transform.position);
                anim.SetBool("Run", false);
                currentTime = 0;
                attackNumber = 0;
            }

            Debug.DrawRay(rayCastPoint.position, rayCastPoint.forward * attackRange, Color.red);
        }
    }

    void ProjectTilemode(bool projectileMode)
    {
        if (projectileMode == true)
        {
            if (currentTime < projectileModeTime)
            {
                currentTime += Time.deltaTime;
                transform.LookAt(player.transform.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
                anim.SetBool("LaunchProjectile", true);
            }
            else
            {
                currentTime = 0;
                attackNumber = 0;
                anim.SetBool("LaunchProjectile", false);
            }
        }
    }

    void SpawnFireball(bool fireBallMode)
    {
        //second attack, boss will cast a fireball, that will follow the player
        if (fireBallMode == true)
        {

            randomFireBall = UnityEngine.Random.Range(0, 3);

            if (randomFireBall == 1)
            {
                anim.SetTrigger("ProjectileAttack");
            }
            else
            {
                anim.SetTrigger("ProjectileAttack2");
            }

            currentTime = 0;
            attackNumber = 0;
            randomFireBall = 0;
        }
    }

    void Pounce(bool value)
    {
        if (value == true)
        {
            anim.SetTrigger("Jump");
            attackNumber = 0;
        }
    }

    void JumpEnable()
    {
        jumped = true;
    }

    void Jump()
    {
        if (jumped == true)
        {
            if (transform.position.y <= maxJumpHeight)
            {
                transform.position += Vector3.up * jumpForce * Time.deltaTime;
                ai.enabled = false; // to avoid conflcit 
            }
            else
            {
                jumped = false;
                jumpedToPlayer = true;
                playerLastPosition = player.transform.position;
            }
        }

        if (jumped == false && jumpedToPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerLastPosition, stompSpeed * Time.deltaTime);
        }

        if (transform.position == playerLastPosition)
        {
            anim.SetTrigger("Fall");
            ai.enabled = true;
            jumpedToPlayer = false;
        }
    }

    void DamagePlayer()
    {
        if (playerInRange == true)
        {
            player.GetComponent<playerCombat>().damagePlayer(biteDamage);
        }
    }

    private void LaunchProjectile()
    {
        Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
    }

    void LaunchFireBall()
    {
        Instantiate(fireBall, projectileSpawn.position, Quaternion.identity);
    }
    void LaunchMultipleFireBalls()
    {
        for (int i = 0; i < multipleFireBallSpawners.Length; i++)
        {
            GameObject fireBallsObj = Instantiate(fireBalls, multipleFireBallSpawners[i].position, Quaternion.identity);
            fireBallsObj.GetComponent<Rigidbody>().velocity = multipleFireBallSpawners[i].forward * fireBallsSpeed * Time.deltaTime;
        }
    }

    //Stage 2 skills------------------------------------------------------
    void healingMode(bool value)
    {
        if (value == true)
        {
            if (minionsSpawned == false)
            {
                anim.SetTrigger("SpawnMinions");
                minionsSpawned = true;
            }
            if (currentTime < healingDuration)
            {
                currentTime += Time.deltaTime;
                anim.SetBool("Distracted", true);
                if (icedShield == true && health < 500)//<----------------- To Change
                {
                    iceShieldObj.SetActive(true);
                    health += healingRate * Time.deltaTime;
                }
                else
                {
                    iceShieldObj.SetActive(false);
                }
            }
            else
            {
                iceShieldObj.SetActive(false);
                currentTime = 0;
                attackNumber = 0;
                minionsSpawned = false;
                anim.SetBool("Distracted", false);
            }
        }
    }

    void SpawnMinions()
    {
        icedShield = true;
        for (int i = 0; i < numberOfMinionsSpawned; i++)
        {
            Instantiate(minions, multipleFireBallSpawners[i].position, Quaternion.identity);
        }
    }

    void Replicate(bool value)
    {
        if(value == true)
        {
            anim.SetTrigger("Replicate");
            attackNumber = 0;
            currentTime = 0;
        }
    }

    void SpawnReplica()
    {
        GameObject cloneObj = Instantiate(bossClone, multipleFireBallSpawners[0].position, Quaternion.identity);
        Destroy(cloneObj, cloneDuration);

        if(health <= 0)
        {
            Destroy(cloneObj);
        }
    }


    //Getter setter method
    public void DamageBoss(float value)
    {
        health -= value;
    }

    public float GetBossHealth()
    {
        return health;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossScript : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] float health;
    float maxHealth;
    public int numberOfSkills = 5;
    float halfHealth;

    [Header("IdleMode")]
    [SerializeField] float idleModeTime = 10f;
    public bool damageBoss;

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
    [SerializeField] ParticleSystem chargePower;
    int randomFireBall;
    [SerializeField] GameObject fireBalls;
    [SerializeField] float fireBallsSpeed = 10f;
    [SerializeField] public Transform[] multipleFireBallSpawners;

    [Header("Pounce")]
    [SerializeField] float stompDamage = 10f;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] float maxJumpHeight = 1f;
    [SerializeField] float stompSpeed = 35f;
    bool jumped;
    public bool jumpedToPlayer;

    [Header("Cat Bite")]
    [SerializeField] float catBiteDuration = 20f;
    [SerializeField] float runSpeed = 2.5f;
    [SerializeField] float biteDamage = 10f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform rayCastPoint;
    RaycastHit hit;
    public float distanceToplayer;
    bool playerInRange;

    [Header("Darkness")]
    [SerializeField] public float darknessDuration = 10f;
    public bool distracted;
    [SerializeField] GameObject luminousSpell;
    public float distanceToLight;
    public bool lightsDisabled;

    [Header("Spawn Minions")]
    [SerializeField] GameObject minions;
    [SerializeField] float numberOfMinionsSpawned = 3;
    [SerializeField] float healingRate;
    [SerializeField] float healingDuration = 8f;
    [SerializeField] GameObject iceShieldObj;
    [SerializeField] ParticleSystem stunVfx;
    [SerializeField] BossForceField forceFieldScript;
    public bool shieldIsActivated;
    bool minionsSpawned = false;

    [Header("Replicate Mode")]
    [SerializeField] public GameObject bossClone;
    [SerializeField] public float cloneDuration = 15f;
    [SerializeField] public bool bossReplica;


    //Components
    NavMeshAgent ai;
    Animator anim;


    void Start()
    {
        maxHealth = health;
        print(maxHealth);

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
        //if this is the original boss then start easy
        if (health > halfHealth || bossReplica == true)
        {
            numberOfSkills = 5;
            Stage1Skills();
            //else if the boss health is less than half or this is the replica then make it difficult
        }
        else if (bossReplica == false && health < halfHealth)
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
        HealingMode();
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
            DarkNess(true);
            // CatBite(true);
        }
        else
        {
            DarkNess(false);
            //CatBite(false);
        }

        Jump();
    }

    void Stage2Skills()
    {
        if (attackNumber == 6)
        {
            Replicate(true);
        }
        else
        {
            Replicate(false);
        }
    }

    void HealingMode()
    {
        if (attackNumber == 5)
        {
            healingMode(true);
        }
        else
        {
            healingMode(false);
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
                damageBoss = true;
                transform.LookAt(player.transform.position);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
                currentTime += Time.deltaTime;
            }
            else
            {
                damageBoss = false;
                currentTime = 0;
                attackNumber = UnityEngine.Random.Range(1, numberOfSkills);
            }

        }
    }


    void DarkNess(bool value)
    {
        if (value == true)
        {
            anim.SetTrigger("Darkness");
            currentTime = 0;
            attackNumber = 0;
        }

        //boss distraction
        //luminousSpell = GameObject.FindGameObjectWithTag("luminous");
        //if (luminousSpell != null)
        //{
        //    attackNumber = 0;
        //    distanceToLight = Vector3.Distance(transform.position, luminousSpell.transform.position);
        //    //distract boss
        //    if (lightsDisabled == true && luminousSpell != null && distanceToLight > ai.stoppingDistance)
        //    {
        //        ai.SetDestination(luminousSpell.transform.position);
        //        anim.SetBool("Run", true);
        //    }
        //    else
        //    {
        //        anim.SetBool("Run", false);
        //    }
        //    transform.LookAt(luminousSpell.transform.position);
        //    distracted = true;
        //}
    }

    void Animator_DisableLights()
    {
        lightsDisabled = true;
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
        if (projectileMode == true && distracted == false)
        {
            damageBoss = false;
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
            damageBoss = false;
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
            damageBoss = false;
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
            player.GetComponent<playerCombat>().enableSenses();
        }

        if (transform.position == playerLastPosition)
        {
            anim.SetTrigger("Fall");
            ai.enabled = true;
            jumpedToPlayer = false;
            player.GetComponent<playerCombat>().disableSenses();
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
            shieldIsActivated = forceFieldScript.activateShield;
            //spawning minions
            if (minionsSpawned == false)
            {
                anim.SetTrigger("SpawnMinions");
                minionsSpawned = true;
            }
            
            if (currentTime < healingDuration)
            {
                anim.SetBool("Distracted", true);
                currentTime += Time.deltaTime;
                if (forceFieldScript.activateShield == true)
                {
                    if(health < maxHealth)
                    {
                        health += healingRate * Time.deltaTime;
                    }
                }
            }
            else
            {
                currentTime = 0;
                attackNumber = 0;
                minionsSpawned = false;
                forceFieldScript.activateShield = false;
                stunVfx.Stop();
                anim.SetBool("Distracted", false);
            }
        }
    }

    void SpawnMinions()
    {
        forceFieldScript.activateShield = true;
        for (int i = 0; i < numberOfMinionsSpawned; i++)
        {
            Instantiate(minions, multipleFireBallSpawners[i].position, Quaternion.identity);
        }
    }

    void Replicate(bool value)
    {
        if (value == true)
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

        if (health <= 0)
        {
            Destroy(cloneObj);
        }
    }


    //Getter setter method
    public void DamageEnemy(float value)
    {
        //boss can only be damage during Idle mode
        if (damageBoss == true || forceFieldScript.activateShield == false)
        {
            anim.SetTrigger("TakeDamage");
            health -= value;
        }
    }

    public float GetBossHealth()
    {
        return health;
    }

    void Freeze()
    {
        ai.enabled = false;
        anim.enabled = false;
    }
    void UnFreeze()
    {
        ai.enabled = true;
        anim.enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && jumpedToPlayer == true)
        {
            player.GetComponent<playerCombat>().damagePlayer(stompDamage);
        }
    }

    public void playStunVfx()
    {
        stunVfx.Play();
    }

    public void PlayChargeParticle()
    {
        chargePower.Play();
    }

    public void StopChargeParticle()
    {
        chargePower.Stop();
    }

}

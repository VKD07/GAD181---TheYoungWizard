using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDummy : MonoBehaviour
{
    [SerializeField] float switchDirectionDuration = 5f;
    [SerializeField] float dummyMoveSpeed = 10f;
    [SerializeField] float distance = 2f;
    float currentTimeToMove;
    public bool isDead;
    public bool startMoving;
    public bool startAttack;
    Vector3 position;
    float angle;
    public Animator anim;
    public BasicAttackTutorial basicAttack;
    [SerializeField] public BossForceField forceField;
    public bool startShielding;
    public bool dontDamage;
    [Header("Fireball")]
    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireBallSpawner;
    public bool shieldTask;

    public float fireInterval = 3f;
    public float currentFireTime;

    AudioSource audioSource;
    [SerializeField] AudioClip fireBallSfx;
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        basicAttack = FindObjectOfType<BasicAttackTutorial>();
        StartMoving();
        StartAttacking();
    }

    private void StartMoving()
    {
        if (startMoving)
        {
            angle += dummyMoveSpeed * Time.deltaTime;
            position.z = Mathf.Cos(angle) * distance * Time.deltaTime;
            transform.position += position;
        }
    }

    void StartAttacking()
    {
        if (startAttack)
        {
            currentFireTime += Time.deltaTime;
            if (currentFireTime >= fireInterval)
            {
                anim.SetTrigger("Attack");
                currentFireTime = 0f;
            }
        }
    }

    public void ResetDummy()
    {
        isDead = false;
        anim.SetBool("Reset", true);
    }

    public void KillDummy()
    {
        if (!dontDamage)
        {
            isDead = true;
            anim.SetBool("Reset", false);
            anim.SetTrigger("Dead");
        }
    }

    void spawnFireBall()
    {
        if (shieldTask)
        {
            audioSource.PlayOneShot(fireBallSfx);
            Instantiate(fireball, fireBallSpawner.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "playerBullet" && startShielding == true)
        {
            forceField.activateShield = true;
        }
    }
}

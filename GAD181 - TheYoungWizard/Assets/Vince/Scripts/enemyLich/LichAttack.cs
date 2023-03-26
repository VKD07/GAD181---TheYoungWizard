using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LichAttack : LichMainScript
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float fireRate = 2f;
    public float currentTime;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ai = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Execute();
    }

    public override void Execute()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if(distanceToPlayer <= ai.stoppingDistance)
        {
            anim.SetBool("Idle", true);
            if (currentTime < fireRate)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                anim.SetBool("Idle", false);
                anim.SetTrigger("Attack");
                currentTime = 0;
            }
        }
        else
        {
            anim.SetBool("Idle", false);
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

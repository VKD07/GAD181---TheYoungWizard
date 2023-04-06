using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderChase : MonoBehaviour
{
    Animator anim;
    NavMeshAgent ai;
    GameObject player;
    float distanceToPlayer;

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        KeepLookingAtPlayer();
    }
    public void Chase()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) { return; }

        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > ai.stoppingDistance)
        {
            anim.SetBool("Walking", true);
            ai.SetDestination(player.transform.position);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

    }

    private void KeepLookingAtPlayer()
    {
        Vector3 directionToTarget = player.transform.position - transform.position;
        Vector3 newDirection = new Vector3(directionToTarget.x, 0f, directionToTarget.z);
        Quaternion lookRotation = Quaternion.LookRotation(newDirection, Vector3.up);
        transform.rotation = lookRotation;
    }


}

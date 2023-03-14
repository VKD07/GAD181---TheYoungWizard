using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent ai;
    public Transform player;
    void Start()
    {
       ai = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        ai.SetDestination(player.position);

    }
}

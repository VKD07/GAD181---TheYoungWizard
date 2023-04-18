using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LichChase : LichMainScript
{

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); ;
        ai = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Execute();
    }

    public override void Execute()
    {
        noxRotation();
        ai.SetDestination(player.transform.position);
    }

    void noxRotation()
    {
        Vector3 directionToTarget = player.transform.position - transform.position;
        Vector3 newDirection = new Vector3(directionToTarget.x, 0f, directionToTarget.z);
        Quaternion lookRotation = Quaternion.LookRotation(newDirection, Vector3.up);
        transform.rotation = lookRotation;
    }

}

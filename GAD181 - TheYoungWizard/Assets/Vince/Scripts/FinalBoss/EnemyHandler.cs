using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] Collider[] stageCollider;
    [SerializeField] GameObject [] enemies;
    GameObject player;
    Collider playerCollider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (stageCollider[0].bounds.Intersects(playerCollider.bounds))
        {
            for (int i = 0; i < 2; i++)
            {
                enemies[i].SetActive(true);
            }
        }
        
    }
}

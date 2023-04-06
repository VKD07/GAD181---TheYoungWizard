using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] Collider[] stageCollider;
    [SerializeField] GameObject [] enemies;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossHealth;
    [SerializeField] bool[] stageIsClear;
    [SerializeField] AudioHandler audioHandler;
    GameObject player;
    Collider playerCollider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetectPlayer();
        StageHandler();
    }

    private void StageHandler()
    {
        for (int i = 0; i < 2; i++)
        {
            if (enemies[i] == null)
            {
                stageCollider[1].GetComponent<BoxCollider>().isTrigger = true;
                stageIsClear[0] = true;
            }
        }

        for (int i = 2; i < enemies.Length; i++)
        {
            if (stageIsClear[0] == true && enemies[i] == null)
            {
                stageIsClear[1] = true;
                stageCollider[2].GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }

    private void DetectPlayer()
    {
        if (stageCollider[0].bounds.Intersects(playerCollider.bounds))
        {
            audioHandler.PlayEnemyEncounterMusic();
            for (int i = 0; i < 2; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].SetActive(true);
                }

            }
        }
        else if (stageCollider[1].bounds.Intersects(playerCollider.bounds) && stageIsClear[0] == true)
        {
            for (int i = 2; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].SetActive(true);
                }
            }
        }
        else if (stageCollider[2].bounds.Intersects(playerCollider.bounds) && stageIsClear[1] == true)
        {
            boss.SetActive(true);
            bossHealth.SetActive(true);
            stageCollider[0].isTrigger = false;
            RenderSettings.fogColor = Color.black;

        }

    }
}

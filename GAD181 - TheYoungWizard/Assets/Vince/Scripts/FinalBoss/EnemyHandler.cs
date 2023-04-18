using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] Collider[] stageCollider;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossHealth;
    [SerializeField] bool[] stageIsClear;
    [SerializeField] AudioHandler audioHandler;
    [SerializeField] AudioSource audioSource;
    [SerializeField] StoneSpell runeStone;
    [SerializeField] GameObject bossLighting;
    public int enemiesDead;
    GameObject player;
    Collider playerCollider;
    public bool finalStage;
    public bool spawnBoss;
    GameObject luminous;
    bool musicIsPlayed;

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

        if (enemiesDead == 2)
        {
            stageCollider[1].GetComponent<BoxCollider>().isTrigger = true;
            stageIsClear[0] = true;
        }
        else if (stageIsClear[0] == true && enemiesDead == 5)
        {
            stageIsClear[1] = true;
            stageCollider[2].GetComponent<BoxCollider>().isTrigger = true;
            if (audioSource.volume > 0 && !musicIsPlayed)
            {
                audioSource.volume -= 0.05f * Time.deltaTime;
            }
            else
            {
                musicIsPlayed = true;
            }
        }

    }

    private void DetectPlayer()
    {
        if (runeStone.unlocked)
        {
            stageCollider[0].isTrigger = true;
        }
        if (stageCollider[0].bounds.Intersects(playerCollider.bounds) && runeStone.unlocked)
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
            finalStage = true;
            stageCollider[0].isTrigger = false;
            stageCollider[3].enabled = true;
        }

        //boss meets kael if luminous is lighten up

        if (finalStage)
        {
            luminous = GameObject.FindGameObjectWithTag("luminous");
            if (luminous != null)
            {
                if (!spawnBoss)
                {
                    RenderSettings.fogColor = Color.black;
                    spawnBoss = true;
                    bossLighting.SetActive(true);
                    boss.SetActive(true);
                }
            }
        }
    }


}

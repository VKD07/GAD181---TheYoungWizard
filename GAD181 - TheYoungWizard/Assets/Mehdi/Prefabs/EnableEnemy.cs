using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] colliders;
    [SerializeField] GameObject[] enemies;
    EnemyDeathCounter counter;
    [SerializeField] float setNumberToTrigger;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    bool spiderMusic;

    private void Start()
    {
        counter = FindObjectOfType<EnemyDeathCounter>();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(false);
        }

        if (counter.EnemyDeath == setNumberToTrigger)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.EnemyDeath == setNumberToTrigger)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(true);
            }


            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].SetActive(true);
            }

            if (!spiderMusic)
            {
                spiderMusic = true;
                audioSource.clip = clip;
                audioSource?.Play();
            }
        }
    }
}

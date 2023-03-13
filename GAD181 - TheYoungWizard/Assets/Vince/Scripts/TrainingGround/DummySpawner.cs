using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DummySpawner : MonoBehaviour
{
    [SerializeField] GameObject dummyAi;
    [SerializeField] GameObject[] existingDummies;
    [SerializeField] GameObject[] existingWindDummies;
    [SerializeField] Transform[] spawnerLocations;
    [SerializeField] int stageNumber = 1;
    [SerializeField] int numberOfDummiesToSpawn = 1;
  

    private void Update()
    {

        existingDummies = GameObject.FindGameObjectsWithTag("dummy");
        existingWindDummies = GameObject.FindGameObjectsWithTag("windEnemy");

        if (existingDummies.Length == 0 && existingWindDummies.Length == 0 && stageNumber < 9)
        {
            spawnDummy();
        }
    }

    void spawnDummy()
    {
        for (int i = 0; i < numberOfDummiesToSpawn; i++)
        {
            int randomLocation = Random.Range(0, spawnerLocations.Length);
            Instantiate(dummyAi, spawnerLocations[randomLocation].position, Quaternion.identity);
        }

        StageHander();
    }

    private void StageHander()
    {
        stageNumber++;

        if (stageNumber == 3)
        {
            numberOfDummiesToSpawn++;
        }else if(stageNumber == 7)
        {
            numberOfDummiesToSpawn++;
        }else if(stageNumber == 10)
        {
            numberOfDummiesToSpawn++;
        }
    }
}

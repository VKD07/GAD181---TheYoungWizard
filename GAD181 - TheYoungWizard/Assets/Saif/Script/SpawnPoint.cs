using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField] GameObject player;
    public Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = player.transform.position;
    }

    private void Update()
    {
        //For Player to Respawn when dying

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint") 
        {
        spawnPoint = player.transform.position;
        }
    }
}

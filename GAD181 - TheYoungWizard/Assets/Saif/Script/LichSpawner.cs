using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichSpawner : MonoBehaviour
{
    [SerializeField]GameObject theLich;
    public float playerOnSight;
    public Transform player;
    float playerDistance;
    bool spawned = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if(playerOnSight > playerDistance && !spawned)
        {
            Instantiate(theLich, transform.position,Quaternion.identity);
            spawned = true;
        }

    }
}

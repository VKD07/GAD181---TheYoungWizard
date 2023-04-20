using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] float projectileDamage = 10f;
    [SerializeField] float projectileUpSpeed = 1000f;
    [SerializeField] float projectTileTargetSpeed = 20f;
    [SerializeField] GameObject targetIndicator;
    [SerializeField] GameObject explosionVfx;
    GameObject spawnedIndicator;
    Transform player;
    bool targetLock = false;
    bool indicatorSpawned = false;
    Vector3 playerLastPosition;
    Rigidbody rb;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb =GetComponent<Rigidbody>();
    }
    void Update()
    {
        TargetPlayer();
    }

    private void TargetPlayer()
    {
        //bullet will move up after it is spawned 
        if (transform.position.y < 10f)
        {
            rb.velocity = Vector3.up * projectileUpSpeed * Time.deltaTime;
        }
        //once it reached its maximum height it will move towards the player last position
        else
        {   
            targetLock = true;
            playerLastPosition = player.position;
            if(indicatorSpawned == false)
            {
                indicatorSpawned = true;
                spawnedIndicator = Instantiate(targetIndicator, playerLastPosition, Quaternion.Euler(-90f, 0f, 0f));
                spawnedIndicator.transform.position = new Vector3(playerLastPosition.x, -5.18f, playerLastPosition.z);
                Destroy(spawnedIndicator, 1.5f);
            }
        }
        //if the target lock is true it will continue to move towards the players last position
        if (targetLock == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerLastPosition, projectTileTargetSpeed * Time.deltaTime);
            //if it reaches its destination it will destroy itself
            if(transform.position == playerLastPosition )
            {
                GameObject explosionObj = Instantiate(explosionVfx, transform.position, Quaternion.identity);
                Destroy(explosionObj, 2f);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<playerCombat>().damagePlayer(projectileDamage, true);
            Destroy(gameObject);
        }
    }

}

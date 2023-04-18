using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float speed = 10f;
    ShieldTutorial shieldTutorial;
    Rigidbody rb;
    GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            player.GetComponent<playerCombat>().enableSenses();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);
            player.GetComponent<playerCombat>().damagePlayer2(25);
            Destroy(explosionObj, 1f);
            Destroy(gameObject);
        }

        if (other.tag == "PlayerForceField")
        {
            shieldTutorial = FindObjectOfType<ShieldTutorial>();
            shieldTutorial.currentFireBallsBlocked += 1;
            GameObject explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionObj, 1f);
            Destroy(gameObject);
        }
    }
}


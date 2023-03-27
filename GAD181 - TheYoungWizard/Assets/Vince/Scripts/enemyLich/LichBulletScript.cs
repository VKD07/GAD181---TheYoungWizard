using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichBulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletDamage = 20f;
    bool reduceDamage;
    GameObject player;
    public Vector3 bulletDirection;
    Rigidbody rb;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        bulletDirection = (player.transform.position - transform.position).normalized;
    }
    void Update()
    {
        GoToPlayer();
    }

    private void GoToPlayer()
    {
        rb.velocity = bulletDirection *  bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (reduceDamage)
            {
                float newDamage = bulletDamage / 2;
                player.GetComponent<playerCombat>().damagePlayer(newDamage);
                print(newDamage);
            }
            else
            {
                player.GetComponent<playerCombat>().damagePlayer(bulletDamage);
                print(bulletDamage);
            }
            Destroy(gameObject);
        }

        if (other.tag == "PlayerForceField")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            Destroy(gameObject);
        }
    }

    void ReduceDamage(bool value)
    {
        reduceDamage = value;
    }

}

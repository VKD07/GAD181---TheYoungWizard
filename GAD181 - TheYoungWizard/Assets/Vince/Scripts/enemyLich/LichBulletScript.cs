using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichBulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletDamage = 20f;
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
        if(other.tag == "Player")
        {
            player.GetComponent<playerCombat>().damagePlayer(bulletDamage);
            Destroy(gameObject);
        }
    }
}

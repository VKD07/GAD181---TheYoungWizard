using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall_1 : MonoBehaviour
{
    Transform player;
    [SerializeField] float fireBallSpeed = 50f;
    [SerializeField] float fireBallDamage = 20f;
    [SerializeField] bool projectile1 = false;
    void Update()
    {
        if (projectile1 == true) 
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = Vector3.MoveTowards(transform.position, player.position, fireBallSpeed * Time.deltaTime);
        }

        if(projectile1 == false)
        {
            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
       

        if(collision.tag =="frostWall")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerCombat>().damagePlayer(fireBallDamage);
            Destroy(gameObject);
        }
    }
}

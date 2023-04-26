using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Script : MonoBehaviour
{
    [SerializeField] float fireBallDamage = 30f;
    [SerializeField] GameObject explosionParticle;
    SphereCollider sphereCollider;
    private Vector3 savePoint;
    private GameObject player;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dummy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            ExplosionEffect();
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Boss")
        {
            GameObject boss = collision.gameObject;
            BossScript bossScript = boss.GetComponent<BossScript>();

            if(bossScript.shieldIsActivated == false && bossScript.damageBoss == false)
            {
                boss.GetComponent<BossScript>().DamageEnemy(fireBallDamage);
            }

            ExplosionEffect();
            Destroy(gameObject);
        }else if(collision.tag == "BossForceField")
        {
            BossForceField bossForceField = collision.gameObject.GetComponent<BossForceField>();
            if (bossForceField.activateShield == true)
            {
                ExplosionEffect();
                Destroy(gameObject);
            }
        }
        else if (collision.tag == "CatMinion")
        {
            GameObject minion = collision.gameObject;

            minion.GetComponent<CatMinion>().DamageEnemy(fireBallDamage);
            ExplosionEffect();
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment") || collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            ExplosionEffect();
        }
        if(collision.tag == "Enemy")
        {
            ExplosionEffect();
            Destroy(gameObject);
        }

        if(collision.tag == "ForceField")
        {
            ExplosionEffect();
            Destroy(gameObject);
        }

        if (collision.tag == "CheckPoint")
        {
            collision.GetComponent<CampFire>().PlayFire();
        }

        if (collision.gameObject.tag == "SkeletonGate")
        {
            print("gate");
            collision.gameObject.GetComponent<BossDoor>().destroyGate = true;
            ExplosionEffect();
            Destroy(gameObject);
        }

      

    }

    void ExplosionEffect()
    {
        GameObject explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}

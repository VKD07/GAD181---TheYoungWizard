using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Script : MonoBehaviour
{
    [SerializeField] float fireBallDamage = 30f;
    [SerializeField] GameObject explosionParticle;
    SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dummy")
        {
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
        else if (collision.tag == "Environment" || collision.tag == "ground")
        {
            ExplosionEffect();
        }else if(collision.tag == "Enemy")
        {
            ExplosionEffect();
        }

    }

    void ExplosionEffect()
    {
        GameObject explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}

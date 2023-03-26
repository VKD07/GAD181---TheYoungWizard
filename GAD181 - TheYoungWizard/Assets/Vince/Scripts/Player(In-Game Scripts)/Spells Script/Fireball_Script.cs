using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Script : MonoBehaviour
{
    [SerializeField] float fireBallDamage = 30f;
    [SerializeField] GameObject explosionParticle;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dummy")
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

            boss.GetComponent<BossScript>().DamageBoss(fireBallDamage);

            if (bossScript.icedShield == true)
            {
                bossScript.icedShield = false;
                bossScript.damageBoss = true;
                bossScript.playStunVfx();
            }

            ExplosionEffect();

            Destroy(gameObject);
        }
        else if (collision.tag == "CatMinion")
        {
            GameObject minion = collision.gameObject;

            minion.GetComponent<CatMinion>().DamageMinion(fireBallDamage);
            ExplosionEffect();

            Destroy(gameObject);
        }
        else if (collision.tag == "Environment" || collision.tag == "ground")
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

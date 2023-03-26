using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] float bulletDamage = 10f;
    [SerializeField] GameObject bossImpactVfx;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dummy")
        {
            print("Dummy hit");
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Boss")
        {
            GameObject boss = collision.gameObject;
            boss.GetComponent<BossScript>().DamageBoss(bulletDamage);
            GameObject bossImpact = Instantiate(bossImpactVfx, transform.position, Quaternion.identity);
            Destroy(bossImpact, 2f);
            Destroy(gameObject);
        }

        if (collision.tag == "CatMinion")
        {
            GameObject minion = collision.gameObject;
            minion.GetComponent<CatMinion>().DamageMinion(bulletDamage);
            Destroy(gameObject);
        }
        
        if(collision.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<LichAttributes>().forceFieldScript.activateShield == false)
            {
                collision.gameObject.GetComponent<LichAttributes>().DamageEnemy(bulletDamage);
            }
            Destroy(gameObject);
        }

        if(collision.tag == "ForceField")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            Destroy(gameObject);
        }

        if(collision.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}

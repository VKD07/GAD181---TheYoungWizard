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
            collision.gameObject.GetComponent<LichAttributes>().DamageLich(bulletDamage);
            Destroy(gameObject);
        }
    }
}

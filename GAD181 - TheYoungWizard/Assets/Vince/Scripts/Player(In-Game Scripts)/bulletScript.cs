using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] float bulletDamage = 10f;
    [SerializeField] GameObject bossImpactVfx;
    [SerializeField] GameObject explosion;
    bool exploded;

    private void Update()
    {
        DeathHandler();
    }

    private void DeathHandler()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dummy")
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
            //boss.GetComponent<BossScript>().DamageEnemy(bulletDamage);
            GameObject bossImpact = Instantiate(bossImpactVfx, transform.position, Quaternion.identity);
            Destroy(bossImpact, 2f);
            Destroy(gameObject);
            explodeVFX();
        }

        if (collision.tag == "CatMinion")
        {
           // GameObject minion = collision.gameObject;
           // minion.GetComponent<CatMinion>().DamageEnemy(bulletDamage);
            Destroy(gameObject);
            explodeVFX();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
            explodeVFX();
        }

        if (collision.tag == "ForceField")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            Destroy(gameObject);
            explodeVFX();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment") || collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            Destroy(gameObject, 2f);
            explodeVFX();
        }

        if(collision.tag == "tutorialDummy" || collision.tag == "movingDummy")
        {
            MovingDummy dummyScript = collision.gameObject.GetComponent<MovingDummy>();
            if(dummyScript.startShielding == false)
            {
                dummyScript.KillDummy();
                if (dummyScript.startMoving)
                {
                    dummyScript.startMoving = false;
                    dummyScript.basicAttack.secondTask = true;
                }
                Destroy(gameObject);
                explodeVFX();
            }
        }
        if((collision.tag == "CheckPoint"))
        {
            collision.GetComponent<CampFire>().PlayFire();
        }

    }

    public void explodeVFX()
    {
        if (!exploded)
        {
            exploded = true;
            GameObject explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionObj, 3f);
        }
    }
}

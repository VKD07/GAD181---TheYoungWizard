using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] float bulletDamage = 10f;
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
            Destroy(gameObject);
        }

        if (collision.tag == "CatMinion")
        {
            print("Minion Hit");
            GameObject minion = collision.gameObject;
            minion.GetComponent<Animator>().SetTrigger("Hit");
            minion.GetComponent<CatMinion>().DamageMinion(bulletDamage);
            Destroy(gameObject);
        }
    }
}

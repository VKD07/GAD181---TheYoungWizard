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
            boss.GetComponent<Animator>().SetTrigger("Hit");
            boss.GetComponent<BossScript>().DamageBoss(bulletDamage);
            Destroy(gameObject);
        }
    }
}

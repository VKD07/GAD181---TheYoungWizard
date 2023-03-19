using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Script : MonoBehaviour
{
    [SerializeField] float fireBallDamage = 30f;
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
            boss.GetComponent<Animator>().SetTrigger("Hit");
            boss.GetComponent<BossScript>().DamageBoss(fireBallDamage);
            Destroy(gameObject);
        }
    }
}

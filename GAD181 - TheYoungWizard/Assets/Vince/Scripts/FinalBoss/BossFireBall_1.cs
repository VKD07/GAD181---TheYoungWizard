using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall_1 : MonoBehaviour
{
    Transform player;
    [SerializeField] float fireBallSpeed = 50f;
    [SerializeField] float fireBallSpeedToBoss = 15f;
    [SerializeField] float fireBallDamage = 20f;
    [SerializeField] bool projectile1 = false;
    Player_SpellCast spellCast;
    public bool deflected = false;
    public Transform bossLocation;
    private void Start()
    {
        spellCast = FindObjectOfType<Player_SpellCast>();
    }
    void Update()
    {
        //release wind spell will go back to the boss itself
        if (spellCast.releaseWind == true)
        {
            deflected = true;
        }

        if (deflected == true)
        {
            bossLocation = GameObject.FindGameObjectWithTag("Boss").transform;
            Target(bossLocation, fireBallSpeedToBoss);
            
        }else
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Target(player, fireBallSpeed);
        }
    }

    void Target(Transform target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "frostWall")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "windGust")
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerCombat>().damagePlayer(fireBallDamage);
            Destroy(gameObject);
        }else if(collision.gameObject.tag == "Boss" && deflected == true)
        {
            collision.gameObject.GetComponent<BossScript>().DamageBoss(fireBallDamage);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuminousSpell : MonoBehaviour
{
    [SerializeField] float luminousDamagePerSecond = 1f;
    [SerializeField] float bossDistractionDuration = 4f;
    [SerializeField] Collider[] enemies;
    [SerializeField] float radius = 10f;
    [SerializeField] LayerMask layer;
    GameObject boss;
    public float currentTime;

    private void Update()
    {
        enemies = Physics.OverlapSphere(transform.position, radius, layer);

        foreach (Collider objs in enemies)
        {
            objs.SendMessage("LuminousDamage", luminousDamagePerSecond * Time.deltaTime);
            objs.SendMessage("ReduceDamage", true);
        }
    }

    private void OnDestroy()
    {
        foreach (Collider objs in enemies)
        {
            objs.SendMessage("LuminousDamage", luminousDamagePerSecond * Time.deltaTime);
            objs.SendMessage("ReduceDamage", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerStay(Collider other)
    {
        //if(other.tag == "Boss")
        //{
        //    GameObject boss = other.gameObject;
        //    Animator bossAnim = boss.GetComponent<Animator>();
        //    if(boss.GetComponent<BossScript>().jumpedToPlayer == false && currentTime < bossDistractionDuration)
        //    {
        //        currentTime += Time.deltaTime;
        //        boss.GetComponent<BossScript>().enabled = false;
        //        bossAnim.SetBool("Distracted", true);
        //    }
        //    else
        //    {
        //        boss.GetComponent<BossScript>().enabled = true;
        //        boss.GetComponent<BossScript>().distracted = false;
        //        bossAnim.SetBool("Distracted", false);
        //    }
        //}
    }
}

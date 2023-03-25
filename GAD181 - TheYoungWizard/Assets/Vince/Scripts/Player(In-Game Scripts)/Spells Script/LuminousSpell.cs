using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuminousSpell : MonoBehaviour
{
    [SerializeField] float luminousDamage = 5f;
    [SerializeField] float bossDistractionDuration = 4f;
    GameObject boss;
    public float currentTime;

    private void Update()
    {
       
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuminousSpell : MonoBehaviour
{
    [SerializeField] float luminousDamage = 5f;
    [SerializeField] float bossDistractionDuration = 4f;
    GameObject boss;
    Animator bossAnim;
    public float currentTime;

    private void Update()
    {
       if (boss != null && bossAnim != null && boss.GetComponent<BossScript>().distracted == false)
        {
            boss.GetComponent<BossScript>().enabled = true;
            boss.GetComponent<BossScript>().distracted = false;
            bossAnim.SetBool("Distracted", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Boss")
        {
            boss = other.gameObject;
            bossAnim = boss.GetComponent<Animator>();
            if(boss.GetComponent<BossScript>().jumpedToPlayer == false && currentTime < bossDistractionDuration)
            {
                currentTime += Time.deltaTime;
                boss.GetComponent<BossScript>().enabled = false;
                bossAnim.SetBool("Distracted", true);
            }
            else
            {
                boss.GetComponent<BossScript>().distracted = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkeletonTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] triggerCollider;
    [SerializeField] GameObject boss;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    playerCombat pc;
    bool musicPlayed;

    private void Update()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCombat>();   
        if(pc != null && pc.GetPlayerHealth() <= 0)
        {
            triggerCollider[1].GetComponent<BoxCollider>().isTrigger = true;
            StartCoroutine(DisableBoss());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!musicPlayed )
            {
                musicPlayed = true;
                triggerCollider[1].SetActive(true);
                boss.SetActive(true);
                audioSource.clip = clip;
                audioSource.Play();

            }
            StartCoroutine(EnableBlock());
        }
    }

    IEnumerator EnableBlock()
    {
        yield return new WaitForSeconds(0.5f);
        triggerCollider[1].GetComponent<BoxCollider>().isTrigger = false;
        boss.SetActive(true);
    }
    IEnumerator DisableBoss()
    {
        yield return new WaitForSeconds(1f);
        boss.SetActive(false);
    }

}

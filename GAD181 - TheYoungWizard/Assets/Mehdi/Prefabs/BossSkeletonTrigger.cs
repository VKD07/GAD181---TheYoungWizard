using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkeletonTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] triggerCollider;
    [SerializeField] GameObject boss;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            triggerCollider[1].SetActive(true);
            boss.SetActive(true);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}

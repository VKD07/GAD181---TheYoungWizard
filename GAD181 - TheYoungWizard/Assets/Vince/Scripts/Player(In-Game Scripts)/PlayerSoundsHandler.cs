using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsHandler : MonoBehaviour
{
    AudioSource audioSource;
    [Header("Movement")]
    [SerializeField] AudioClip footStep;
    [Header("Attack")]
    [SerializeField] AudioClip basicAttack;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void playFootStepSound()
    {
        audioSource.PlayOneShot(footStep, 0.08f);
    }

    void PlayBasicAttackSFX()
    {
        audioSource.PlayOneShot(basicAttack, 0.05f);
    }
}

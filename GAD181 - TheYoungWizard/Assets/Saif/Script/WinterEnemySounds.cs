using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterEnemySounds : MonoBehaviour
{
    AudioSource audioSource;
    [Header("Chase")]
    [SerializeField] AudioClip movement;
    [Header("Attack")]
    [SerializeField] AudioClip attack1;
    [SerializeField] AudioClip attack2;
    [Header("Death")]
    [SerializeField] AudioClip deadSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayChaseSound()
    {
        audioSource.PlayOneShot(movement, 0.5f);
    }

    public void PlayAttack1SFX()
    {
        audioSource.PlayOneShot(attack1, 0.3f);
    }

    public void PlayDeadSFX()
    {
        audioSource.PlayOneShot(deadSound, 0.5f);
    }
}

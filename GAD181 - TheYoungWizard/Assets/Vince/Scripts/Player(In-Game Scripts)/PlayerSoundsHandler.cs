using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsHandler : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] float spellVolume = 0.5f;
    AudioSource audioSource;
    [Header("Movement")]
    [SerializeField] AudioClip footStep;
    [SerializeField] AudioClip rolling;
    [Header("Attack")]
    [SerializeField] AudioClip basicAttack;
    [Header("Spells")]
    [SerializeField] AudioClip spellCircle;
    [SerializeField] AudioClip iceSpell;
    [SerializeField] AudioClip fireBall;
    [SerializeField] AudioClip windGust;
    [SerializeField] AudioClip luminous;
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

    void PlaySpellCircleSound()
    {
        audioSource.PlayOneShot(spellCircle);
    }

    void PlayIceSpellSFX()
    {
        audioSource.PlayOneShot(iceSpell, spellVolume);
    }

    void PlayFireBallSound()
    {
        audioSource.PlayOneShot(fireBall, 0.08f);
    }

    void PlayWindGustSound()
    {
        audioSource.PlayOneShot(windGust, spellVolume);
    }

    void PlayLuminous()
    {
        audioSource.PlayOneShot(luminous, spellVolume);
    }

    void PlayRollingSound()
    {
        audioSource.PlayOneShot(rolling, 0.09f);
    }
}

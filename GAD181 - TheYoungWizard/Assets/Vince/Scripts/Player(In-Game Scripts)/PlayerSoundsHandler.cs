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
        audioSource.PlayOneShot(iceSpell);
    }

    void PlayFireBallSound()
    {
        audioSource.PlayOneShot(fireBall);
    }

    void PlayWindGustSound()
    {
        audioSource.PlayOneShot(windGust);
    }

    void PlayLuminous()
    {
        audioSource.PlayOneShot(luminous);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    [Header("Shield")]
    [SerializeField] AudioClip characterShield;
    [Header("Consumables")]
    [SerializeField] AudioClip healSfx;
    [SerializeField] AudioClip manaSfx;
    [Header("Death")]
    [SerializeField] AudioClip defeatSfx;
    public bool defeatedSfxPlayed;
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
        audioSource.PlayOneShot(basicAttack, 0.1f);
    }

    void PlaySpellCircleSound()
    {
        audioSource.PlayOneShot(spellCircle, 0.3f);
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

    void PlayCharacterShield()
    {
        audioSource.PlayOneShot(characterShield, 0.1f);
    }

    public void PlayhealSfx()
    {
        audioSource.PlayOneShot(healSfx, 0.3f);
    }

    public void PlayManaSfx()
    {
        audioSource.PlayOneShot(manaSfx, 0.3f);
    }

    public void PlayDefeatSfx()
    {
        if (!defeatedSfxPlayed)
        {
            defeatedSfxPlayed = true;
            audioSource.PlayOneShot(defeatSfx, 0.7f);
        }
    }
}

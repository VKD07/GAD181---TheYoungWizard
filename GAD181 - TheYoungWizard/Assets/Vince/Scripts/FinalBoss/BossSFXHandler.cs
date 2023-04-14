using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSFXHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip fireballProjectile;
    [SerializeField] AudioClip fireballCast;
    [SerializeField] AudioClip chargingSound;
    [SerializeField] AudioClip jumpLaunch;
    [SerializeField] AudioClip groundImpact;
    [SerializeField] AudioClip darkness;
    [SerializeField] AudioClip summon;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFireBallProjectile()
    {
        audioSource.PlayOneShot(fireballProjectile, 0.2f);
    }

    public void PlayFireBallCast()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(fireballCast, 1f);
    }

    public void PlayChargingSound()
    {
        audioSource.PlayOneShot(chargingSound, 0.5f);
    }

    public void PlayJumpLaunch()
    {
        audioSource.PlayOneShot(jumpLaunch, 0.7f);
    }

    public void PlayGroundImpact()
    {
        audioSource.PlayOneShot(groundImpact, 0.7f);
    }

    public void PlayDarkness()
    {
        audioSource.PlayOneShot(darkness, 0.5f);
    }

    public void PlaySumoon()
    {
        audioSource.PlayOneShot(summon, 0.5f);
    }
}

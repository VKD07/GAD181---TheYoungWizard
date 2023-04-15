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
    [SerializeField] AudioClip explodingShield;
    [SerializeField] AudioClip activateShield;
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
        audioSource.PlayOneShot(fireballCast, 0.2f);
    }

    public void PlayChargingSound()
    {
        audioSource.PlayOneShot(chargingSound, 0.2f);
    }

    public void PlayJumpLaunch()
    {
        audioSource.PlayOneShot(jumpLaunch, 0.3f);
    }

    public void PlayGroundImpact()
    {
        audioSource.PlayOneShot(groundImpact, 0.3f);
    }

    public void PlayDarkness()
    {
        audioSource.PlayOneShot(darkness, 0.2f);
    }

    public void PlaySumoon()
    {
        audioSource.PlayOneShot(summon, 0.2f);
    }

    public void ExplodingShieldSound()
    {
        audioSource.PlayOneShot(explodingShield, 0.5f);
    }

    public void ActivateShieldSfx()
    {
        audioSource.PlayOneShot(activateShield, 0.4f);
    }
}

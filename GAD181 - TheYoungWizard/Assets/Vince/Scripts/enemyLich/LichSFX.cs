using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichSFX : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip shieldSfx;
    [SerializeField] AudioClip brokenShieldSfx;
    [SerializeField] AudioClip deadSfx;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShieldSFX()
    {
        audioSource.PlayOneShot(shieldSfx, 0.3f);
    }

    public void PlayBrokenShieldSFX()
    {
        audioSource.PlayOneShot(brokenShieldSfx, 0.5f);
    }

    public void PlayDeadSfx()
    {
        audioSource.PlayOneShot(deadSfx, 0.3f);
    }

}

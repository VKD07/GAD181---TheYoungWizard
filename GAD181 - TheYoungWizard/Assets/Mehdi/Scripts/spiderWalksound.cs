using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderWalksound : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] AudioClip spiderWalk;
    [SerializeField] AudioClip spiderDeath;
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }
    public void play_spiderWalk()
    {
        aud.PlayOneShot(spiderWalk);
    }

    public void PlaySpiderDeath()
    {
        aud.PlayOneShot(spiderDeath, 0.5f);
    }
}

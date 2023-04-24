using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderDeathSound : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] AudioClip deathSound;
    void Start()
    {
        aud = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play_audio()
    {
        aud.PlayOneShot(deathSound);

    }

}

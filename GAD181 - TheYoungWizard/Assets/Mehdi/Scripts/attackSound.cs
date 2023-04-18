using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackSound : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] AudioClip slashSound;
    void Start()
    {
        aud=GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play_audio()
    {
        aud.PlayOneShot(slashSound);
    }
}

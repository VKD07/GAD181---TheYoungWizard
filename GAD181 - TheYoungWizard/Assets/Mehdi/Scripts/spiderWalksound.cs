using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderWalksound : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] AudioClip spiderWalk;
    void Start()
    {
        aud = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play_spiderWalk()
    {
        aud.PlayOneShot(spiderWalk);
    }
}

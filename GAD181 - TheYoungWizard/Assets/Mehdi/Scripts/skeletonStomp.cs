using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonStomp : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] AudioClip stompSound;
    void Start()
    {
        aud = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void play_stomp()
    {
        aud.PlayOneShot(stompSound);
    }
}

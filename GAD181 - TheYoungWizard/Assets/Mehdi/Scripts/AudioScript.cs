using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] AudioClip miniBossMusic;
    AudioSource audioSpeaker;

    private void Start()
    {
       audioSpeaker = GetComponent<AudioSource>();
    }

    public void PlayMiniBossMusic()
    {
        audioSpeaker.PlayOneShot(miniBossMusic);
    }
}

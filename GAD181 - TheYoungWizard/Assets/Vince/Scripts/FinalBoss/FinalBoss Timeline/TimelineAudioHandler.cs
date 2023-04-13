using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineAudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip musicSound1;
    [SerializeField] AudioClip beamMusic;
    [SerializeField] AudioClip endingMusic;
    [SerializeField] float audioMaxVolume = 0.1f;
    public bool startFading;
    public bool musicIsPlaying;
    bool endingMusicIsPlaying;
    public bool playBeamSound;
    public float fadingRate = 0.1f;

    private void Start()
    {
    }

    private void Update()
    {
        if (startFading)
        {
            if (audioSource.volume > 0)
            {
                audioSource.volume -= fadingRate * Time.deltaTime;

            }
            else
            {
                startFading = false;
                audioSource.volume = 0;
            }
        }

        if (playBeamSound)
        {
            if(!musicIsPlaying)
            {
                audioSource.volume = audioMaxVolume;
                audioSource.Stop();
                audioSource.PlayOneShot(beamMusic);
                musicIsPlaying = true;
            }
          
        }

    }

    public void PlayEndingMusic()
    {
        if(!endingMusicIsPlaying)
        {
            audioSource.Stop();
            endingMusicIsPlaying = true;
            audioSource.volume = audioMaxVolume;
            audioSource.PlayOneShot(endingMusic);
        }
    }

    public IEnumerator FadeIn(AudioClip audio, float fadeTime)
    {
        audioSource.volume = 0f;
        audioSource.PlayOneShot(audio);

        while (audioSource.volume < audioMaxVolume)
        {
            audioSource.volume += Time.deltaTime * fadeTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut(AudioClip audio, float fadeTime)
    {
        audioSource.volume = 0f;
        audioSource.PlayOneShot(audio);

        while (audioSource.volume > 0.2f)
        {
            audioSource.volume -= Time.deltaTime * 0.5f / fadeTime;
            yield return null;
        }
    }
}

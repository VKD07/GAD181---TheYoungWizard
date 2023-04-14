using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineAudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioClip musicSound1;
    [SerializeField] AudioClip beamMusic;
    [SerializeField] AudioClip endingMusic;
    [SerializeField] AudioClip catCharge;
    [SerializeField] AudioClip playerCharge;
    [SerializeField] AudioClip beamExplosion;
    [SerializeField] float audioMaxVolume = 0.1f;
    public bool startFading;
    public bool musicIsPlaying;
    bool catChargePlaying;
    bool beamExplosionPlaying;
    bool beamExplosionPlaying2;
    bool playerChargePlaying;
    bool beamCollidedIsPlaying;
    bool endingMusicIsPlaying;
    public bool playBeamSound;
    public float fadingRate = 0.3f;
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
    public void PlayCatCharge()
    {
        if (!catChargePlaying)
        {
            catChargePlaying = true;
            audioSource.PlayOneShot(catCharge);
        }
    }

    public void PlayPlayerCharge()
    {
        if (!playerChargePlaying)
        {
            playerChargePlaying = true;
            audioSource.PlayOneShot(playerCharge);
        }
    }
    public void PlayBeamCollidedCharge()
    {
        if(!beamCollidedIsPlaying)
        {
            beamCollidedIsPlaying = true;
            audioSource2.clip = playerCharge;
            audioSource2.loop = true;
            audioSource2.Play();
        }
    }


    public void PlayBeamExplosion()
    {
        if (!beamExplosionPlaying)
        {
            beamExplosionPlaying = true;
            audioSource2.Stop();
            audioSource.PlayOneShot(beamExplosion);
        }
    }

    public void PlayBeamExplosion2()
    {
        if (!beamExplosionPlaying2)
        {
            audioSource.PlayOneShot(beamExplosion, 0.5f);
            beamExplosionPlaying2 = true;
        }
    }

    public void PlayEndingMusic()
    {
        if(!endingMusicIsPlaying)
        {
            audioSource.volume = audioMaxVolume;
            audioSource.Stop();
            audioSource.clip = endingMusic;
            audioSource.loop = true;
            audioSource.Play();
            endingMusicIsPlaying = true;
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

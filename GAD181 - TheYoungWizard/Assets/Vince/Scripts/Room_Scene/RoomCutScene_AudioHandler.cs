using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCutScene_AudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip portalSceneMusic;
    [SerializeField] AudioClip catMeow;
    [SerializeField] AudioClip redPortalOpen;
    [SerializeField] AudioClip whoosh;
    [SerializeField] AudioClip skipSoundEffect;
    [SerializeField] AudioClip skipSlider;
    bool portalMusicIsPlaying;
    bool catMeowPlayed;
    bool redPortalPlayed;
    bool whooshPlayed;
    bool skipSoundEffectIsPlaying;
    public bool skipSliderPlaying;
    public bool skipped;


    public void PlayPortalSceneMusic()
    {
        if (!portalMusicIsPlaying)
        {
            portalMusicIsPlaying = true;
            audioSource.Stop();
            audioSource.clip = portalSceneMusic;

            if (skipSoundEffectIsPlaying)
            {
                audioSource.PlayOneShot(skipSoundEffect, 1f);
            }
            audioSource.Play();
        }
    }

    public void PlayCatSound()
    {
        if (!catMeowPlayed)
        {
            catMeowPlayed = true;
            audioSource.PlayOneShot(catMeow, 0.4f);
        }
    }

    public void PlayRedPortal()
    {
        if (!redPortalPlayed)
        {
            redPortalPlayed = true;
            audioSource.PlayOneShot(redPortalOpen, 0.8f);
        }
    }

    public void PlayWhooshSFX(float time)
    {
        StartCoroutine(sfxDelay(time));
    }

    public void SkipSFX()
    {
        if(!skipSoundEffectIsPlaying)
        {
            skipSoundEffectIsPlaying = true;
            audioSource.PlayOneShot(skipSoundEffect, 1f);
        }
    }

    public void PlaySkipSlider()
    {
        if (!skipSliderPlaying && !skipped)
        {
            skipSliderPlaying = true;
            audioSource.PlayOneShot(skipSlider);
        }
    }

    IEnumerator sfxDelay(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.PlayOneShot(whoosh, 1f);
    }
}

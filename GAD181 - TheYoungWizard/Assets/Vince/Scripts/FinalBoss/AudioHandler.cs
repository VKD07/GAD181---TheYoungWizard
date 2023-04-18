using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip enemyEncounter;
    [SerializeField] AudioClip bossRevealMusic;
    [SerializeField] AudioClip bossFightMusic;
    public bool musicIsPlaying;
    bool bossRevealMusicIsPlaying;
    bool bossFightMusicIsPlaying;
    public void PlayEnemyEncounterMusic()
    {
        if (!musicIsPlaying)
        {
            musicIsPlaying = true;
            StartCoroutine(FadeIn(enemyEncounter, 5f));
        }
    }

    public IEnumerator FadeIn(AudioClip audio, float fadeTime)
    {
        audioSource.volume = 0f;
        audioSource.PlayOneShot(audio);

        while(audioSource.volume < 0.2f)
        {
            audioSource.volume += Time.deltaTime * 0.5f / fadeTime;
            yield return null;
        }
    }

    public void PlayBossReveal()
    {
        if (!bossRevealMusicIsPlaying)
        {
            audioSource.volume = 0.3f;
            bossRevealMusicIsPlaying = true;
            audioSource.Stop();
            audioSource.clip = bossRevealMusic;
            audioSource.Play();
        }
    }

    public void PlayBossFightMusic()
    {
        if (!bossFightMusicIsPlaying)
        {
            audioSource.volume = 0.3f;
            bossFightMusicIsPlaying = true;
            audioSource.Stop();
            audioSource.clip = bossFightMusic;
            audioSource.Play();
        }
    }
}

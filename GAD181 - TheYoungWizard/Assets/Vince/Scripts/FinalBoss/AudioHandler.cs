using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip enemyEncounter;
    public bool musicIsPlaying;
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

        while (audioSource.volume < 0.2f)
        {
            audioSource.volume += Time.deltaTime * 0.5f / fadeTime;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CampFire : MonoBehaviour
{
    [SerializeField] ParticleSystem campFire;
    [SerializeField] GameObject fireSpanwer;
    [SerializeField] AudioClip campfireLitSfx;
    [SerializeField] CanvasGroup uiCanvas;
    [SerializeField] RespawnPointHandler respawnPointHandler;
    [SerializeField] ParticleSystem explosionVfx;
    [SerializeField] Transform playerSpawnLocation;
    playerCombat pc;
    public bool campFireLit;
    bool fadeOut;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        respawnPointHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnPointHandler>();
    }

    private void Update()
    {
        FadeInText();
    }

    private void FadeInText()
    {
        if (campFireLit && !fadeOut)
        {
            if (uiCanvas.alpha < 1)
            {
                uiCanvas.alpha += Time.deltaTime * 1f;
            }
            else
            {
                StartCoroutine(FadeOutText(2));
            }
        }
    }

    IEnumerator FadeOutText(float time)
    {
        yield return new WaitForSeconds(time);
        fadeOut = true;
        if (uiCanvas.alpha > 0)
        {
            uiCanvas.alpha -= Time.deltaTime * 1f;
        }
        else
        {
            uiCanvas.alpha = 0;
            uiCanvas.gameObject.SetActive(false);
        }
    }
    public void PlayFire()
    {
        if (!campFireLit)
        {
            respawnPointHandler.SetRespawnPoint(playerSpawnLocation.transform.position);
            audioSource.PlayOneShot(campfireLitSfx, 1f);
            campFireLit = true;
            explosionVfx.Play();
            campFire.Play();
        }
    }
}

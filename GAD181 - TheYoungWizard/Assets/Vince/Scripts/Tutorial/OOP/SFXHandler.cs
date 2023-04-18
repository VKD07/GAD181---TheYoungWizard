using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]AudioClip sucessSfx;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySuccessSfx()
    {
        audioSource.PlayOneShot(sucessSfx);
    }
    
}

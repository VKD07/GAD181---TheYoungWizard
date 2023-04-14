using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionVFx : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip explosionSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(explosionSound);
    }


}

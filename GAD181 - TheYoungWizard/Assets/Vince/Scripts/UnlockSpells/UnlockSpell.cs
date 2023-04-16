using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSpell : MonoBehaviour
{
    [SerializeField] Transform rayCastPosition;
    [SerializeField] LayerMask stoneSpellLayer;
    [SerializeField] float rayCastRange;
    [SerializeField] KeyCode interactKey = KeyCode.F;
    [SerializeField] ParticleSystem levelUpVfx;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip levelUpSound;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectStoneRune();
    }

    private void DetectStoneRune()
    {
        if (Physics.Raycast(rayCastPosition.position, rayCastPosition.transform.forward, out hit, rayCastRange, stoneSpellLayer))
        {
            if (Input.GetKeyDown(interactKey))
            {
                GameObject stoneRune = hit.rigidbody.gameObject;
                if(stoneRune.GetComponent<StoneSpell>().unlocked == false)
                {
                    stoneRune.GetComponent<StoneSpell>().unlocked = true;
                    stoneRune.GetComponent<StoneSpell>().HideRuneStone();
                    audioSource.PlayOneShot(levelUpSound, 0.3f);
                    levelUpVfx.Play();
                }
            }
        }
    }
}

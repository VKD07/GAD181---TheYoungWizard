using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOpenChest : MonoBehaviour
{
    [SerializeField] Transform rayCastPosition;
    [SerializeField] LayerMask chestLayer;
    [SerializeField] float rayCastRange;
    [SerializeField] KeyCode openChestKey = KeyCode.F;
    [SerializeField] ParticleSystem playerBeam;
    RaycastHit hit;

    private void Update()
    {
        DetectChest();
    }

    private void DetectChest()
    {
        if(Physics.Raycast(rayCastPosition.position, rayCastPosition.transform.forward, out hit, rayCastRange,chestLayer))
        {
            hit.rigidbody.GetComponent<chestScript>().SendMessage("ShowText");
            if (Input.GetKeyDown(openChestKey))
            {
                GameObject chest = hit.rigidbody.gameObject;
                chest.GetComponent<Animator>().SetTrigger("OpenChest");
                chest.GetComponent<chestScript>().playChestParticle();
            }
        }
    }

    public void PlayBeam()
    {
        if(playerBeam != null)
        {
            playerBeam.Play();
        }
    }
}

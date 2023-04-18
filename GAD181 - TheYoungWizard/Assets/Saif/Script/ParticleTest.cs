using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    [SerializeField] ParticleSystem healParticles;
    
    [SerializeField] ParticleSystem manaParticles;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            healParticles.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            manaParticles.Play();
        }
    }

}

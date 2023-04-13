using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsMarker : MonoBehaviour
{
    [SerializeField] ParticleSystem leftFoot;
    [SerializeField] ParticleSystem rightFoot;

    void PlayLeftDustParticle()
    {
            leftFoot.Play();
    }
    void PlayRightDustParticle()
    {

            rightFoot.Play();

    }


}

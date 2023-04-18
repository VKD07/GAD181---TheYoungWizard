using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprintEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem fastStep;



    public void PlayFastSteps()
    {
        fastStep.Play();
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] ParticleSystem campFire;



    public void PlayFire()
    {
        campFire.Play();
    }
            


}
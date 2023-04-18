using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CampFire : MonoBehaviour
{
    [SerializeField] ParticleSystem campFire;
    [SerializeField] GameObject fireSpanwer;
    public Vector3 firePlacement;
    
   public void PlayFire()
   {
        campFire.Play();
   }


    private void Start()
    {
        firePlacement = new Vector3(fireSpanwer.transform.position.x, fireSpanwer.transform.position.y, fireSpanwer.transform.position.z);
 
    }

}

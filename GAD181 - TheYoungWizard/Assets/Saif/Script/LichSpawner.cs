using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichSpawner : MonoBehaviour
{
    [SerializeField]GameObject theLich;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        { 
        Instantiate(theLich);
        GetComponent<BoxCollider>().enabled = false;
    
        }
    }
}

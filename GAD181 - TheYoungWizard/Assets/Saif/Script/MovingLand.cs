using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLand : MonoBehaviour
{
    Animator movingAnimation;

    private void Start()
    {
        movingAnimation = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(transform);
            movingAnimation.SetTrigger("PlayerIsOn");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}

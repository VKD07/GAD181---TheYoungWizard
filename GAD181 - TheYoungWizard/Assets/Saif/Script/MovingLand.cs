using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLand : MonoBehaviour
{
    Animator movingAnimation;
    [SerializeField] playerCombat pc;

    private void Start()
    {
        movingAnimation = GetComponent<Animator>();
        pc = FindObjectOfType<playerCombat>();

    }

    private void Update()
    {
        if (pc.GetPlayerHealth() <= 0)
        {
            movingAnimation.SetBool("Move", false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            movingAnimation.SetBool("Move",true);
        }
    }
    
}

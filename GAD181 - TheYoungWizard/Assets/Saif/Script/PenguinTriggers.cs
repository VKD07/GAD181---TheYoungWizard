using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinTriggers : MonoBehaviour
{
    [SerializeField] Collider[] triggers;
    [SerializeField] Collider playerCollider;
    [SerializeField] GameObject penguin; 

    // Update is called once per frame
    void Update()
    {
        FindPlayer();

        EnableDisablePenguins();
    }

    private void EnableDisablePenguins()
    {
        if (playerCollider != null && penguin != null)
        {
            if (triggers[0].bounds.Intersects(playerCollider.bounds))
            {
                penguin.SetActive(true);
            }
            else if (triggers[1].bounds.Intersects(playerCollider.bounds))
            {
                Destroy(penguin);
                penguin.SetActive(false);
            }
        }
    }

    private void FindPlayer()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }
}

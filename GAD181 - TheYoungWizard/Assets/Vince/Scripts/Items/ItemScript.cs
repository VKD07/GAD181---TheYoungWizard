using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] Sprite itemIcon;
    [SerializeField] ItemManager itemManager;
    bool playerDetected = false;
    playerCombat playerScript;
    Player_Movement playerMovement;

    private void Update()
    {
        //find the item manager game object
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        pickUpAnim();

    }
    private void pickUpAnim()
    {
        //if player is detected and player picks it up
        if (playerScript != null && playerDetected == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerMovement.rolled == false)
            {
                playerDetected = false;
                for (int i = 0; i < itemManager.isFull.Length; i++)
                {
                    //iterating the slots if its full and if 1 slot is not full
                    if (itemManager.isFull[i] == false)
                    {
                        //trigger animation
                        playerScript.anim.SetTrigger("pickUp");

                        itemManager.isFull[i] = true;

                        itemManager.itemSlots[i].sprite = itemIcon;

                        Destroy(gameObject, 0.5f);

                        break;

                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if item collided with the player and player picks up
        if (other.tag == "Player")
        {
            playerMovement = other.gameObject.GetComponent<Player_Movement>();
            playerDetected = true;
            playerScript = other.gameObject.GetComponent<playerCombat>();
            print("Item detected");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement = null;
            playerDetected = false;
            playerScript = null;

        }
    }
}

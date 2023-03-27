using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    [SerializeField] ParticleSystem openChestParticle;
    [Header("Items Inside")]
    [SerializeField] int numberOfHeathPotion;
    [SerializeField] int numberOfManaPotion;

    [Header("Chest Components")]
    [SerializeField] Sprite[] itemIcons;
    [SerializeField] ItemManager itemManager;
    bool chestIsOpen;
    bool itemsTaken;

    private void Update()
    {
        //find the item manager game object
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        TakeItemsInside();
    }
    private void TakeItemsInside()
    {
        if (chestIsOpen && !itemsTaken)
        {
            itemsTaken = true;
            if (numberOfHeathPotion > 0)
            {
                itemManager.numberOfHealthP += numberOfHeathPotion;

                itemManager.itemSlots[0].sprite = itemIcons[0];
            }
            if (numberOfManaPotion > 0)
            {
                itemManager.numberOfManaP += numberOfManaPotion;

                itemManager.itemSlots[1].sprite = itemIcons[1];
            }
        }
    }


    public void playChestParticle()
    {
        if (!chestIsOpen) {
            openChestParticle.Play();
            chestIsOpen = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    [SerializeField] ParticleSystem openChestParticle;
    [Header("Items Inside")]
    [SerializeField] int numberOfHeathPotion;
    [SerializeField] int numberOfManaPotion;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI manaText;

    [Header("Chest Components")]
    [SerializeField] Sprite[] itemIcons;
    [SerializeField] ItemManager itemManager;
    bool chestIsOpen;
    bool itemsTaken;

    [Header("Audio")]
    AudioSource audioSource;
    [SerializeField] AudioClip openChestSfx;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
                healthText.SetText($"+{numberOfHeathPotion}");
                itemManager.numberOfHealthP += numberOfHeathPotion;

                itemManager.itemSlots[0].sprite = itemIcons[0];
            }
            if (numberOfManaPotion > 0)
            {
                manaText.SetText($"+{numberOfManaPotion}");
                itemManager.numberOfManaP += numberOfManaPotion;
                itemManager.itemSlots[1].sprite = itemIcons[1];
            }
        }
    }


    public void playChestParticle()
    {
        if (!chestIsOpen) {
            audioSource.PlayOneShot(openChestSfx, 0.1f);
            openChestParticle.Play();
            chestIsOpen = true;
        }
    }
}

using System;
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
    [SerializeField] public GameObject guideUI;
    [SerializeField] float distanceToShowText = 2f;
    public bool chestIsOpen;
    bool itemsTaken;
    float distanceToPlayer;


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
        itemManager = GameObject.Find("ItemManager")?.GetComponent<ItemManager>();
        if (itemManager == null)
        {
            return;
        }

        TakeItemsInside();
        ShowText();
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

    public void ShowText()
    {
        distanceToPlayer = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

        if (distanceToPlayer <= distanceToShowText && !chestIsOpen)
        {
            guideUI.SetActive(true);
        }
        else
        {
            guideUI.SetActive(false);
        }
    }

    public void playChestParticle()
    {
        if (!chestIsOpen)
        {
            audioSource.PlayOneShot(openChestSfx, 0.1f);
            openChestParticle.Play();
            chestIsOpen = true;
        }
    }
}

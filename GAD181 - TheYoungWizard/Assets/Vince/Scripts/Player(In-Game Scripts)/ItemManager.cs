using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public Image[] itemSlots;
    [SerializeField] public bool[] isFull;
    [SerializeField] TextMeshProUGUI healthItemQuantity;
    [SerializeField] public int numberOfHealthP;
    [SerializeField] public int numberOfManaP;
    [SerializeField] TextMeshProUGUI manaItemQuantity;
    public Color colorTransparent;

    private void Start()
    {
        HideItemSlots();
    }

    private void Update()
    {
        ShowItemSlot();
        healthItemQuantity.SetText(numberOfHealthP.ToString());
        manaItemQuantity.SetText(numberOfManaP.ToString());
    }

    private void ShowItemSlot()
    {
        if (numberOfHealthP > 0)
        {
            colorTransparent.a = 1f;
            itemSlots[0].color = colorTransparent;
        }
        else
        {
            colorTransparent.a = 0f;
            itemSlots[0].color = colorTransparent;
        }
        if (numberOfManaP > 0)
        {
            colorTransparent.a = 1f;
            itemSlots[1].color = colorTransparent;
        }
        else
        {
            colorTransparent.a = 0f;
            itemSlots[1].color = colorTransparent;
        }
    }

    private void HideItemSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            colorTransparent = itemSlots[i].color;
            colorTransparent.a = 0f;
            itemSlots[i].color = colorTransparent;
        }
    }

    public void HideItemSlot(int value)
    {
        colorTransparent = itemSlots[value].color;
        colorTransparent.a = 0f;
        itemSlots[value].color = colorTransparent;
    }
}

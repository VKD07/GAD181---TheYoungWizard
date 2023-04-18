using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAndManaTutorial : MonoBehaviour
{
    ItemManager itemManager;
    bool taskOne;

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerHealed();
    }

    private void CheckIfPlayerHealed()
    {
        itemManager = FindObjectOfType<ItemManager>();
        if(itemManager != null)
        {
            itemManager.numberOfHealthP = 1;

            if (itemManager.numberOfHealthP <= 0)
            {
                taskOne = true;
            }
        }
    }

    public bool HealDone()
    {
        return taskOne;
    }
}

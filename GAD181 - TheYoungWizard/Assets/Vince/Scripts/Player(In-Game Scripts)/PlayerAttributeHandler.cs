using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributeHandler : MonoBehaviour
{
    [SerializeField] Slider HealthSlider;
    [SerializeField] Slider ManaSlider;
    [SerializeField] float manaReloadRate = 1f;

    [SerializeField]playerCombat playerStats;

    void Start()
    {
        playerStats = FindObjectOfType<playerCombat>();

        //initializing player mana
        HealthSlider.maxValue = playerStats.GetPlayerHealth();
        ManaSlider.maxValue = playerStats.GetPlayerMana();
        HealthSlider.value = playerStats.GetPlayerHealth();
        ManaSlider.value = playerStats.GetPlayerMana();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHealth();
        UpdatePlayerMana();
    }

    private void UpdatePlayerHealth()
    {
        HealthSlider.value = playerStats.GetPlayerHealth();
    }

    private void UpdatePlayerMana()
    {
        //mana increases by time
        if(playerStats.GetPlayerMana() < 100)
        {
            playerStats.SetPlayerMana(manaReloadRate * Time.deltaTime);
        }

        ManaSlider.value = playerStats.GetPlayerMana();
    }
}

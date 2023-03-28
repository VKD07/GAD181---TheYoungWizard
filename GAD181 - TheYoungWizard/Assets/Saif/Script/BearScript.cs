using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BearScript : MainEnemyScript
{
    [Header("Bear Attributes")]
    [SerializeField] float bearHealth;
    [SerializeField] float bearSpeed;
    [SerializeField] float bearDamage;

    [Header("Bear Slider")]
    [SerializeField] Slider slider;

    private void Start()
    {
        enemyHealth = bearHealth;
        enemySpeed = bearSpeed;
        enemyDamage = bearDamage;
        slider.maxValue = bearHealth;
    }

    private void Update()
    {
        UpdateBearHealth();
        if(bearHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateBearHealth()
    {
        slider.value = bearHealth;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Chase()
    {
    
    throw new System.NotImplementedException();
    }
    public override void Death()
    {
        throw new System.NotImplementedException();
    }

    public override void DamageEnemy(float playerDamage)
    {
        bearHealth -= playerDamage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LichAttributes : LichMainScript
{
    [SerializeField] float health = 100f;
    [SerializeField] Slider healthSlider;
    [SerializeField] public ForceFieldScript forceFieldScript;
    void Start()
    {
        anim = GetComponent<Animator>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        Execute();
    }

    public override void Execute()
    {
        healthSlider.value = health;

        if(health <= 0)
        {
            anim.SetTrigger("Dead");
            gameObject.GetComponent<LichAttack>().enabled = false;
            gameObject.GetComponent<LichChase>().enabled = false;
            forceFieldScript.enabled = false;
            Destroy(gameObject, 3f);
        }
    }

    public void DamageEnemy(float damage)
    {
        if(forceFieldScript.activateShield == false)
        {
            health -= damage;
        }
    }
    
    void LuminousDamage(float value)
    {
        health -= value;
    }

    void Freeze()
    {
        this.gameObject.GetComponent<LichAttack>().enabled = false;
        this.gameObject.GetComponent<LichChase>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
    }

    void UnFreeze()
    {
        this.gameObject.GetComponent<LichAttack>().enabled = true;
        this.gameObject.GetComponent<LichChase>().enabled = true;
        this.gameObject.GetComponent<Animator>().enabled = true;
    }
}

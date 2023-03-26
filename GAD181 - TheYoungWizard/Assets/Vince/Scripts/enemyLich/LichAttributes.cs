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
            Destroy(gameObject, 3f);
        }
    }

    public void DamageLich(float damage)
    {
        if(forceFieldScript.activateShield == false)
        {
            health -= damage;
        }
    }


}

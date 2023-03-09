using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DummyScript : MonoBehaviour
{
    Animator animator;
    [Header("Spell Components")]
    [SerializeField] GameObject spellImageUI;
    [SerializeField] Image spellSlot;
    [SerializeField] int[] spellIDs;
    [SerializeField] Sprite[] spellIcons;
    int spellChosenIndex;

    [Header("Dummy Setinngs")]
    GameObject bullet;
    [SerializeField] Transform player;
    [SerializeField] GameObject[] spellBulets;
    [SerializeField] Transform bulletSpawner;



    private void Start()
    {
        animator = GetComponent<Animator>();
    
        StartCoroutine(chooseASpell());
    }

    IEnumerator chooseASpell()
    {
       int randomStartTime = Random.Range(4, 7);

        while (true)
        {
            yield return new WaitForSeconds(randomStartTime);

            spellImageUI.SetActive(true);

            //choose a spell
            spellChosenIndex = Random.Range(0, spellIDs.Length);
            spellSlot.sprite = spellIcons[spellChosenIndex];
            //make the dummy stand
            animator.SetBool("Start", true);
        }
    }

    private void Update()
    {
        //dummy looking at the player always
        transform.LookAt(player.position);

        Attack();

    }

    private void Attack()
    {
        if(spellImageUI.activeSelf == true)
        {
            animator.SetBool("Attacking", true);
        }
    }


    public void bulletHandler()
    {
        bullet = Instantiate(spellBulets[0], bulletSpawner.position, Quaternion.identity);

        //if fireball then red
        if (spellChosenIndex == 0)
        {
            setBulletProperties(Color.red, "fireBall");
        }
        //if wind then green
        else if (spellChosenIndex == 1)
        {
            setBulletProperties(Color.green, "windGust");
        }
        //if ice then blue
        else if (spellChosenIndex == 2)
        {
            setBulletProperties(Color.blue, "frostWall");
        }
        //if light then yellow
        else if (spellChosenIndex == 3)
        {
            setBulletProperties(Color.yellow, "light");
        }
      
    }

    void setBulletProperties(Color color, string spellname)
    {
        bullet.GetComponent<Renderer>().material.SetColor("_BaseColor", color);
        bullet.GetComponent<dummyBullet>().setBulletSpell(spellname);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "playerBullet" || collision.gameObject.tag == "Fireball")
        {
            animator.SetTrigger("dummyHit");
        }
    }
}

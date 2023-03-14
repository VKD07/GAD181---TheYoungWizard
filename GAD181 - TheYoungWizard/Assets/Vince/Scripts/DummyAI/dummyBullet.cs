using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyBullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] Renderer render;
    public string currentSpell;


   

    void Update()
    {
        Transform playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, bulletSpeed * Time.deltaTime);
    }

    public void setBulletSpell(string spell)
    {
        currentSpell = spell;
    }

  

    private void onTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }

        //spells

        if(currentSpell == "fireBall" && collision.tag == "Fireball")
        {
            Destroy(gameObject);
        }else if(currentSpell == "frostWall" && collision.tag == "frostWall")
        {
            Destroy(gameObject);
        }
        else if (currentSpell == "windGust" && collision.tag == "windGust")
        {
            Destroy(gameObject);
        }
        else if (currentSpell == "light" && collision.tag == "luminous")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }

        //spells

        if (currentSpell == "fireBall" && collision.tag == "Fireball")
        {
            Destroy(gameObject);
        }
        else if (currentSpell == "frostWall" && collision.tag == "frostWall")
        {
            Destroy(gameObject);
        }
        else if (currentSpell == "windGust" && collision.tag == "windGust")
        {
            Player_SpellCast playerSpellcast = GameObject.Find("KaelModel").GetComponent<Player_SpellCast>();
            if(playerSpellcast.releaseWind == true)
            {
                Destroy(gameObject);
            }
            
        }
        else if (currentSpell == "light" && collision.tag == "luminous")
        {
            Destroy(gameObject);
        }   
    }


}

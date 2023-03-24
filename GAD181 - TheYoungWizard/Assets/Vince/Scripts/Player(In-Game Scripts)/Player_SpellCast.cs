using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_SpellCast : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CastModeManager castModeManager;
    [SerializeField] SpellSlot spellManager;
    [SerializeField] Animator playerAnimation;
    [SerializeField] playerCombat combatScript;

    [Header("Fireball Spell")]
    [SerializeField] GameObject fireball;
    [SerializeField] float fireBallSpeed;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] LayerMask layerMask;
    GameObject fireBallObj;

    [Header("Ice Spell")]
    [SerializeField] GameObject ice;
    [SerializeField] Transform iceSpawn;
    [SerializeField] float iceWallDuration;
    GameObject [] boss;
    GameObject[] iceWalls;


    [Header("Wind Gust Spell")]
    [SerializeField] GameObject windGustVfx;
    [SerializeField] float windGustDamage = 10f;
    [SerializeField] SphereCollider sphere;
    [SerializeField] float windRange = 4f;
    [SerializeField] List<GameObject> enemiesInRange;
    [SerializeField] float knockBackForce = 5f;
    [SerializeField] List <string> enemyTagsAffected;
    public bool releaseWind = false;

    [Header("Luminous Spell")]
    [SerializeField] GameObject luminousLight;
    [SerializeField] Transform luminousLightSpawn;
    [SerializeField] float lightDuration;

    private void Start()
    {
        //adjust the wind sphere
        sphere.radius = windRange;
    }

    public void enableMovement()
    {
        combatScript.castingSpell = false;
    }
    public void ReleaseIce()
    {
        Quaternion spawnRot = Quaternion.LookRotation(-transform.forward ,transform.up );
        spawnRot *= Quaternion.Euler(0, -180, 0);
        GameObject iceObj = Instantiate(ice, iceSpawn.position, spawnRot);
        Destroy(iceObj, iceWallDuration);
    }

    private void fixingIceWallBug()
    {
        boss = GameObject.FindGameObjectsWithTag("Boss");
        iceWalls = GameObject.FindGameObjectsWithTag("frostWall");
        if(iceWalls.Length <= 0)
        {
            for (int i = 0; i < boss.Length; i++)
            {
                if (boss[i].GetComponent<Animator>().enabled == false
                && boss[i].GetComponent<NavMeshAgent>().enabled == false
                &&boss[i].GetComponent<BossScript>().enabled == false)
                {
                    boss[i].GetComponent<Animator>().enabled = true;
                    boss[i].GetComponent<NavMeshAgent>().enabled = true;
                    boss[i].GetComponent<BossScript>().enabled = true;
                }
            }
        }
    }

    private void ReleaseFireBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (combatScript.targetMode == true && Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // Calculate the direction to fire the bullet
            Vector3 direction = (hit.point - bulletSpawn.position).normalized;

            // Instantiate the fireball prefab
            fireBallObj = Instantiate(fireball, bulletSpawn.position, Quaternion.identity);

            // Set the initial velocity of the bullet
            fireBallObj.GetComponent<Rigidbody>().velocity = direction * fireBallSpeed * Time.deltaTime;
        }
        else if (combatScript.targetMode == false)
        {
            // Instantiate the fireball prefab
            fireBallObj = Instantiate(fireball, bulletSpawn.position, Quaternion.identity);

            // Set the initial velocity of the bullet
            fireBallObj.GetComponent<Rigidbody>().velocity = transform.forward * fireBallSpeed * Time.deltaTime;
        }
        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red);

    }

    public void CastLuminous()
    {
       GameObject lightObj = Instantiate(luminousLight, luminousLightSpawn.position, Quaternion.identity);
        Destroy(lightObj, lightDuration);
    }

    #region WindGust spell

    public void ReleaseWindGust()
    {
        releaseWind = true;
        GameObject windVfx = Instantiate(windGustVfx, transform.position + transform.up, Quaternion.identity);
        Destroy(windVfx, 2f);
    }

    public void disableWindgust()
    {
        releaseWind = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyTagsAffected.Contains(other.tag))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enemyTagsAffected.Contains(other.tag))
        {
            if(other.gameObject.GetComponent<BossScript>() != null && releaseWind == true)
            {
                other.gameObject.GetComponent<BossScript>().DamageBoss(windGustDamage);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (enemyTagsAffected.Contains(other.tag))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
    
    private void Update()
    {
        fixingIceWallBug();

        if (releaseWind == true)
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                Vector3 knowckBackDirection = enemy.transform.position - transform.position;
                knowckBackDirection.y = 0;
                enemyRb.AddForce(knowckBackDirection * knockBackForce, ForceMode.Impulse);
            }
            enemiesInRange.Clear();
        }
    }


    #endregion
}

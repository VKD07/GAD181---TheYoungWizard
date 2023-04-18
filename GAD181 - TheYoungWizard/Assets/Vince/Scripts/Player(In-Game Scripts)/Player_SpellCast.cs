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
    [SerializeField] Player_Movement playerMovement;
    int spell;

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
    GameObject[] boss;
    GameObject[] iceWalls;


    [Header("Wind Gust Spell")]
    [SerializeField] GameObject windGustVfx;
    [SerializeField] Transform windGustPostion;
    [SerializeField] float windGustDamage = 10f;
    [SerializeField] float totalSpeed = 8f;
    [SerializeField] SphereCollider sphere;
    [SerializeField] float windRange = 4f;
    [SerializeField] List<GameObject> enemiesInRange;
    [SerializeField] float knockBackForce = 5f;
    [SerializeField] List <string> enemyTagsAffected;
    [SerializeField] float speedBoostDuration = 5f;
    public bool releaseWind = false;
    public bool enableSpeedBoost;
    int otherLayer;
    string otherTag;
    public float speedBoostTime;
    float playerWalkingSpeed;
    float playerRunningSpeed;

   [Header("Luminous Spell")]
    [SerializeField] GameObject luminousLight;
    [SerializeField] Transform luminousLightSpawn;
    [SerializeField] float lightDuration;

    [Header("Vfx circles")]
    [SerializeField] GameObject[] spellCircles; 

    private void Start()
    {
        //adjust the wind sphere
        sphere.radius = windRange;
        //initializing player movement for wind gust spell
        playerWalkingSpeed = playerMovement.walkingSpeed;
        playerRunningSpeed = playerMovement.runSpeed;
    }

    public void enableMovement()
    {
        combatScript.castingSpell = false;
    }

    #region iceSpell
    public void ReleaseIce()
    {
        Quaternion spawnRot = Quaternion.LookRotation(-transform.forward ,transform.up );
        spawnRot *= Quaternion.Euler(0, -180, 0);
        GameObject iceObj = Instantiate(ice, iceSpawn.position, spawnRot);
        Destroy(iceObj, iceWallDuration);
    }

    void enableIceCircleSpell()
    {
        spellCircles[0].SetActive(true);
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
    #endregion

    #region fireball Spell
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

    void enableFireCircle()
    {
        spellCircles[3].SetActive(true);
    }

    #endregion

    #region luminousSpell
    public void CastLuminous()
    {
       GameObject lightObj = Instantiate(luminousLight, luminousLightSpawn.position, Quaternion.identity);
        Destroy(lightObj, lightDuration);
    }

    void enableLuminousCircle()
    {
        spellCircles[1].SetActive(true);
    }
    #endregion
    #region WindGust spell
    void enableWindCircle()
    {
        spellCircles[2].SetActive(true);
    }
    public void ReleaseWindGust()
    {
        releaseWind = true;
    }

    public void disableWindgust()
    {
        releaseWind = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        otherLayer = other.gameObject.layer;
        otherTag = LayerMask.LayerToName(otherLayer);
        if (enemyTagsAffected.Contains(otherTag))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        otherLayer = other.gameObject.layer;
        otherTag = LayerMask.LayerToName(otherLayer);
        if (enemyTagsAffected.Contains(otherTag))
        {
            if(other.gameObject.GetComponent<BossScript>() != null && releaseWind == true)
            {
                other.gameObject.GetComponent<BossScript>().DamageEnemy(windGustDamage);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        otherLayer = other.gameObject.layer;
        otherTag = LayerMask.LayerToName(otherLayer);
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
                if (enemy == null) { return; }
                Vector3 directionToEnemy = enemy.transform.position - transform.position;
                directionToEnemy.y = 0f; // Set the y direction to zero to prevent enemies from being pushed upwards
                enemy.transform.position += directionToEnemy.normalized * knockBackForce * Time.deltaTime;
                enemy.SendMessage("DamageEnemy", windGustDamage);
            }
            enemiesInRange.Clear();
            enableSpeedBoost = true;
        }

       // speedBoost();
    }

   /* private void speedBoost()
    {
        if(enableSpeedBoost && speedBoostTime < speedBoostDuration)
        {
            playerMovement.runSpeed = totalSpeed;
            playerMovement.walkingSpeed = totalSpeed;
            speedBoostTime += Time.deltaTime;
        }
        else
        {
            playerMovement.runSpeed = playerRunningSpeed;
            playerMovement.walkingSpeed = playerWalkingSpeed;
            enableSpeedBoost = false;
            speedBoostTime = 0f;
        }
    }*/

    void EnableWindGustVfx()
    {
        GameObject windVfx = Instantiate(windGustVfx, windGustPostion.position, Quaternion.identity);
        Destroy(windVfx, 2f);
    }

    #endregion

    #region spellCircleVfx
  
    void disableVfxCircle()
    {
        for (int i = 0; i < spellCircles.Length; i++)
        {
            spellCircles[i].SetActive(false);
        }
    }
    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Ice Spell")]
    [SerializeField] GameObject ice;
    [SerializeField] Transform iceSpawn;
    [SerializeField] float iceWallDuration;

    [Header("Wind Gust Spell")]
    [SerializeField] SphereCollider sphere;
    [SerializeField] float windRange = 4f;
    [SerializeField] List<GameObject> enemiesInRange;
    [SerializeField] float knockBackForce = 5f;
    public bool releaseWind = false;

    [Header("Luminous Spell")]
    [SerializeField] GameObject luminousLight;
    [SerializeField] Transform luminousLightSpawn;
    [SerializeField] float lightDuration = 5f;


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
        Quaternion spawnRot = Quaternion.LookRotation(transform.up, -transform.forward);
        GameObject iceObj = Instantiate(ice, iceSpawn.position, spawnRot);
        Destroy(iceObj, iceWallDuration);
    }

    private void ReleaseFireBall()
    {
        GameObject fireBallObj = Instantiate(fireball, bulletSpawn.position, Quaternion.identity);
        fireBallObj.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * fireBallSpeed * Time.deltaTime;
 
    }

    public void CastLuminous()
    {
        GameObject luminous = Instantiate(luminousLight, luminousLightSpawn.position, Quaternion.identity);

        Destroy(luminous, lightDuration);
    }

    #region WindGust spell

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
        if (other.tag == "Enemy")
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        
            foreach (GameObject enemy in enemiesInRange)
            {
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                Vector3 knowckBackDirection = enemy.transform.position - transform.position;
                knowckBackDirection.y = 0;
                enemyRb.AddForce(knowckBackDirection * knockBackForce, ForceMode.Impulse);
            }

            enemiesInRange.Clear();

        if (releaseWind == true)
        {
            //finding all enemies withing the range and adding it to the list
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, windRange);
            foreach (Collider enemy in hitColliders)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemiesInRange.Add(enemy.gameObject);
                }
            }
        }
        
    }

    #endregion
}

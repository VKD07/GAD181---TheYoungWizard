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
}

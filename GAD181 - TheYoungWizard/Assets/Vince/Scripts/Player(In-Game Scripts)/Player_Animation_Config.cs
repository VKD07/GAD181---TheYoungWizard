using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player_Animation_Config : MonoBehaviour
{
    //[SerializeField] PlayerScript player;
    [SerializeField] Player_Movement pm;
    [SerializeField] public float xAxisMouseSensitivity = 80f;

    [Header("Player Attack Settings")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float bulletMaxDamage = 10f;
    [SerializeField] float bulletMinimumDamage = 2f;
    public bool targetAvailable;
    float currentDamage;
    public bool enemyDetected;
    bool checkPointDetected;
    Vector3 direction;
    RaycastHit hit;
    public float distanceToPlayer;

    [Header("Player Roll")]
    [SerializeField] CharacterController cr;
    [SerializeField] Rigidbody rb;
    [SerializeField] CinemachineFreeLook cam;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] Transform player;

    private void Update()
    {
        DetectEnemy();
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            bulletMaxDamage = 300f;
            bulletMinimumDamage = 300f;
        }
    }

    private void DetectEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            targetAvailable = true;
            direction = (hit.point - bulletSpawn.position).normalized;
           
            if (hit.rigidbody != null && hit.rigidbody.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemyDetected = true;
            }
            if (hit.rigidbody != null && hit.rigidbody.gameObject.layer == LayerMask.NameToLayer("CheckPoint"))
            {
                checkPointDetected = true;
            }
            distanceToPlayer = Vector3.Distance(hit.point, transform.position);
            Debug.DrawLine(ray.origin, ray.direction * Mathf.Infinity, Color.red);
        }
        else
        {
            enemyDetected = false;
            checkPointDetected = false;
            targetAvailable = false;
        }
    }

    public void SpawnBullet()
    {
        if (targetAvailable)
        {
            currentDamage = UnityEngine.Random.Range(bulletMinimumDamage,bulletMaxDamage+1);
            GameObject bulletObj = Instantiate(bullet, bulletSpawn.position, Quaternion.LookRotation(direction, Vector3.up));

            Rigidbody bulletRigidbody = bulletObj.GetComponent<Rigidbody>();

            bulletRigidbody.velocity = direction * bulletSpeed;
            if (enemyDetected && hit.rigidbody != null)
            {
                hit.rigidbody.gameObject.SendMessage("DamageEnemy", currentDamage);
            }
            
            if(checkPointDetected && hit.rigidbody != null)
            {
                hit.rigidbody.gameObject.GetComponent<CampFire>().PlayFire();
            }
        }
    }

    //public void CharacterFall()
    //{
    //    pm.fall = true;
    //}
    void enableRoll()
    {
        pm.rolling = true;
    }
    public void DisableRoll()
    {
        // cr.enabled = true;
        //player.position = new Vector3(player.position.x, -4.947f, player.position.z);
        // rb.velocity = Vector3.zero;
        // rb.isKinematic = true;
        //pm.rolled = false;
        //pm.notRollingForward = false;
        // pm.rolling = false;
        //playerCombat.rolled = false;
        pm.rolling = false;
        //cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
        //giving mouse controll again
        cam.m_YAxis.m_MaxSpeed = 2;
        cam.m_XAxis.m_MaxSpeed = pm.xMouseSensitivity;
    }

    //disabling cr if picking up item;
    public void disableCharacterController()
    {
        pm.characterController.enabled = false;
    }

    public void enableCharacterController()
    {
        pm.characterController.enabled = true;
    }

    public void TriggerAttackCameraShake()
    {
        if (distanceToPlayer <= 5)
        {
            CameraShake.instance.ShakeCamera(0.5f, 0.8f);
        }
    }
}

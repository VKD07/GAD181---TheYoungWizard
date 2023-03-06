using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Config : MonoBehaviour
{
    //responsible for firing bullets
    //[SerializeField] PlayerScript player;
    [SerializeField] Player_Movement pm;

    //Attack
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletSpeed;

    //Roll
    [SerializeField] float rollSpeed = 6f;
    [SerializeField] CharacterController cr;
    [SerializeField] Rigidbody rb;
    [SerializeField] CinemachineFreeLook cam;



    public void SpawnBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the direction to fire the bullet
            Vector3 direction = (hit.point - bulletSpawn.position).normalized;

            // Instantiate the bullet prefab
            GameObject bulletObj = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);

            // Set the initial velocity of the bullet
            Rigidbody bulletRigidbody = bulletObj.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

    public void SpawnBigBullet()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    // Calculate the direction to fire the bullet
        //    Vector3 direction = (hit.point - bulletSpawn.position).normalized;

        //    // Instantiate the bullet prefab
        //    GameObject bulletObj = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);

        //    // Set the initial velocity of the bullet
        //    Rigidbody bulletRigidbody = bulletObj.GetComponent<Rigidbody>();
        //    bulletRigidbody.velocity = direction * bulletSpeed;
        //}
    }

    public void DisableRoll()
    {
        cr.enabled = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        pm.rolled = false;
        pm.notRollingForward = false;
        pm.rolling = false;
        playerCombat.rolled = false;
        //giving mouse controll again
        cam.m_YAxis.m_MaxSpeed = 2;
        cam.m_XAxis.m_MaxSpeed = 200;
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
}

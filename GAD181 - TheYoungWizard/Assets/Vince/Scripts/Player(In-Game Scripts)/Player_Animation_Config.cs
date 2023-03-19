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
    [SerializeField] LayerMask layerMask;



    //Roll
    [SerializeField] CharacterController cr;
    [SerializeField] Rigidbody rb;
    [SerializeField] CinemachineFreeLook cam;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] Transform player;

    private void Update()
    {
        
    }

    public void SpawnBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
     
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 direction = (hit.point - bulletSpawn.position).normalized;

            GameObject bulletObj = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);

            Rigidbody bulletRigidbody = bulletObj.GetComponent<Rigidbody>();

            bulletRigidbody.velocity = direction * bulletSpeed;
        }
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
    }

    //public void CharacterFall()
    //{
    //    pm.fall = true;
    //}

    public void DisableRoll()
    {
        cr.enabled = true;
        player.position = new Vector3(player.position.x, -4.947f, player.position.z);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        pm.rolled = false;
        pm.notRollingForward = false;
        pm.rolling = false;
        playerCombat.rolled = false;
        cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
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

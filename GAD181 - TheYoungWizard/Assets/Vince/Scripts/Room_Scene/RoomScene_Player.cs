using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScene_Player : MonoBehaviour
{

    CharacterController characterController;

    [Header("Player Movement Settings")]
    [SerializeField] Animator characterAnim;
    [SerializeField] float speed;
    [SerializeField] float gravity = 9.81f;



    //for the rotation of camera
    [Header("Camera Rotation Settings")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] float velocity;
    [SerializeField] float smoothTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //setting the cursor to visible and lock to the center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        //PlayerMovement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 newPos = new Vector3(horizontal, 0f, vertical).normalized;


      

        if (newPos.magnitude > 0.1f)
        {
            
            //player rotation
            float targetAngle = Mathf.Atan2(newPos.x, newPos.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            //player rotate to the target angle with smoothness
            float newAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocity, smoothTime);
            //apply the new rotation to the character
            transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
            //character moves according to where the camera is facing
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle , 0f) * Vector3.forward;

            //applying gravity
          

            //move character
            characterController.Move(moveDir * speed * Time.deltaTime);

            //play running annimation
            characterAnim.SetBool("Move", true);
        }
        else
        {
            characterAnim.SetBool("Move", false);
        }

        //applying gravity
        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down * gravity * Time.deltaTime);
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player_Movement : MonoBehaviour
{

    CharacterController characterController;

    [Header("Player Movement Settings")]
    //[SerializeField] Animator characterAnim;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float currentspeed;
    [SerializeField] float walkingSpeed;
    [SerializeField] float runSpeed;

    [Header("Player Animation")]
    [SerializeField] Animator anim;


    //for the rotation of camera
    [Header("Camera Rotation Settings")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] float velocity;
    [SerializeField] float smoothTime;



    void Start()
    {
        //setting the cursor to visible and lock to the center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        characterMovement();
        characterAimMode();
    }

  
    private void characterMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 newPos = new Vector3(horizontalInput, 0f, verticalInput).normalized;


        if (newPos.magnitude > 0.1f)
        {

            //character animation and sprint
            //sprinting
            if (Input.GetKey(KeyCode.LeftShift))
            {


                currentspeed = runSpeed;

            }

            //walkings
            else
            {
                currentspeed = walkingSpeed;
            }

            //player rotation
            float targetAngle = Mathf.Atan2(newPos.x, newPos.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            //player rotate to the target angle with smoothness
            float newAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocity, smoothTime);
            //apply the new rotation to the character
            transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
            //character moves according to where the camera is facing
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //move character
            if (characterController.enabled == true)
            {
                characterController.Move(moveDir * currentspeed * Time.deltaTime);
            }

            characterRoll();
        }

        //applying gravity
        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down * gravity * Time.deltaTime);
        }

        //movement anim
        MovementAnimation(newPos);
    }

    private void characterAimMode()
    {
        //sight adjust character rotation
        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.LookAt(transform.position + mainCamera.transform.forward);

            float xRotation = Mathf.Clamp(transform.eulerAngles.x, -5f, 12f);

            transform.rotation = Quaternion.Euler(xRotation, transform.eulerAngles.y, transform.eulerAngles.z);

        }
    }


    private void characterRoll()
    {
        ////roll forward
        //if (rolled == true)
        //{
        //    characterController.enabled = false;
        //    rb.isKinematic = false;
        //    rb.velocity = transform.forward * forceStrength;

        //    //roll backwards direction
        //}
        //if (rolled == true && Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.S))
        //{
        //    characterController.enabled = false;
        //    rb.isKinematic = false;
        //    rb.velocity = -transform.forward * forceStrength;
        //}
        ////roll right
        //if (rolled == true && Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.D))
        //{
        //    characterController.enabled = false;
        //    rb.isKinematic = false;
        //    rb.velocity = transform.right * forceStrength;
        //}
        ////roll left
        //if (rolled == true && Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.A))
        //{
        //    characterController.enabled = false;
        //    rb.isKinematic = false;
        //    rb.velocity = -transform.right * forceStrength;
        //}



        ////character roll
        //if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Mouse1) && rolled == false)
        //{
        //    rolled = true;
        //    anim.SetTrigger("Roll");

        //}

        ////chracter roll target mode
        //if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space) && rolled == false)
        //{
        //    rolled = true;
        //    anim.SetTrigger("RollLeft");
        //}

        //if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space) && rolled == false)
        //{
        //    rolled = true;
        //    anim.SetTrigger("RollRight");
        //}

        //if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space) && rolled == false)
        //{
        //    rolled = true;
        //    anim.SetTrigger("Roll");
        //}

        //if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space) && rolled == false)
        //{
        //    rolled = true;
        //    anim.SetTrigger("RollBack");
        //}
    }

    private void MovementAnimation(Vector3 newPos)
    {
        //Animation -----------------------------------


        if (newPos.magnitude > 0.1f)
        {
            //running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Moving", true);
                anim.SetBool("run", true);
                anim.SetBool("Walk", false);

            }
            //walkings
            else
            {

                anim.SetBool("Moving", true);
                anim.SetBool("run", false);
                anim.SetBool("Walk", true);
            }

            //target mode Animation

            //run left
            if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.A)
                || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.Mouse1))
            {

                anim.SetBool("Moving", true);
                anim.SetBool("runLeft", true);
                anim.SetBool("runRight", false);
                anim.SetBool("walkBack", false);
                anim.SetBool("run", false);
                anim.SetBool("Walk", false);
                anim.SetBool("walkBack", false);

            }



            //run right
            if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.D)
                || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Mouse1))
            {


                anim.SetBool("Moving", true);
                anim.SetBool("runRight", true);
                anim.SetBool("runLeft", false);
                anim.SetBool("run", false);
                anim.SetBool("Walk", false);
                anim.SetBool("walkBack", false);


            }


            // Walk back

            if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.S)
                || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Mouse1))
            {

                anim.SetBool("Moving", true);
                anim.SetBool("walkBack", true);

                anim.SetBool("runRight", false);
                anim.SetBool("runLeft", false);
                anim.SetBool("run", false);
                anim.SetBool("Walk", false);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                anim.SetBool("walkBack", false);
            }


        }
        else
        {
            anim.SetBool("Moving", false);
            anim.SetBool("run", false);
            anim.SetBool("Walk", false);
            anim.SetBool("runRight", false);
            anim.SetBool("runLeft", false);
            anim.SetBool("walkBack", false);
        }
    }
}

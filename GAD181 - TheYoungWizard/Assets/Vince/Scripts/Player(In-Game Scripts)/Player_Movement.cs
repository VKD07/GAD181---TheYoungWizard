using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player_Movement : MonoBehaviour
{

    [HideInInspector] public CharacterController characterController;

    [Header("Player Movement Settings")]
    //[SerializeField] Animator characterAnim;
    [SerializeField] float gravity = 3f;
    [SerializeField] float currentspeed;
    [SerializeField] float walkingSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] KeyCode rollKey = KeyCode.LeftControl;
    public bool isMoving = false;
    CapsuleCollider capsuleCollider;

    [Header("Player Jump Settings")]
    [SerializeField] float maxHeightJump = 2f;
    [SerializeField] float speedToReachMaxHeight = 2f;
    [SerializeField] float jumpVelocity = 0.2f;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    public bool jumped = false;
    public float currentHeight;
    public bool fall = false;

    [Header("Player Animation")]
    [SerializeField] Animator anim;

    //for the rotation of camera
    [Header("Camera Rotation Settings")]
    [SerializeField] GameObject mainCamera;
    [SerializeField] float velocity;
    [SerializeField] float smoothTime;
    [SerializeField] GameObject thirdPersonCamera;

    [Header("Character Roll")]
    [SerializeField] float rollForce = 8f;
    [SerializeField]float rollDuration = 0.5f;
    public bool rolling;
    playerCombat pc;
    float rollCurrentTime;

    [SerializeField] float forceStrength = 10f;
    Rigidbody rb;

    

    void Start()
    {
        //setting the cursor to visible and lock to the center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<playerCombat>();

        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        characterMovement();
        characterAimMode();
        characterRoll();
        // characterJump();
      

    }

    private void characterMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 newPos = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (newPos.magnitude > 0.1f && pc.attacking == false)
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


            if (characterController.enabled == true && pc.castingSpell == false)
            {
                //player rotation
                float targetAngle = Mathf.Atan2(newPos.x, newPos.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
                //player rotate to the target angle with smoothness
                float newAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocity, smoothTime);
                //apply the new rotation to the character
                transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
                //character moves according to where the camera is facing
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                //move character
                characterController.Move(moveDir * currentspeed * Time.deltaTime);

            }
            isMoving = true;
        }
        else
        {
            isMoving = false; //The purpose of this is to set the weight of the animation base layer
        }


        //applying gravity
        if (!characterController.isGrounded && rolling == false)
        {
            characterController.Move(Vector3.down * gravity * Time.deltaTime);
        }

        //movement anim
        MovementAnimation(newPos);
    }

    private void characterJump()
    {

        if (Input.GetKeyDown(jumpKey) && characterController.isGrounded == true)
        {
            print("Jumped");
            jumped = true;
            anim.SetBool("StandingJump", true);
        }


        if (Input.GetKey(jumpKey) && jumped == true && currentHeight < maxHeightJump)
        {
            currentHeight += speedToReachMaxHeight * Time.deltaTime;
            characterController.Move(Vector3.up * jumpVelocity * Time.deltaTime);
        }
        else
        {
            characterController.Move(Vector3.down * gravity * Time.deltaTime);
        }

        if (Input.GetKeyUp(jumpKey) || currentHeight >= maxHeightJump)
        {
            anim.SetBool("StandingJump", false);
        }

        if (characterController.isGrounded == true)
        {
            print("playerIsGrounded");
            fall = false;
            currentHeight = 0;
            jumped = false;
        }


    }

    private void characterAimMode()
    {
        //sight adjust character rotation
        if (Input.GetKey(KeyCode.Mouse1) && rolling == false)
        {
            transform.LookAt(transform.position + mainCamera.transform.forward);

            float xRotation = Mathf.Clamp(transform.eulerAngles.x, -5f, 12f);

            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }


    private void characterRoll()
    {

        //if (rolled == true && rolling == false)
        //{
        //    rolling = true;
        //    //characterController.enabled = false;
        //    // rb.isKinematic = false;
        //    // rb.velocity = transform.forward * forceStrength;
        //    characterController.Move(transform.forward * 10f * Time.deltaTime);
        //   cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        //    pc.RollCamera();//disabling camera controll
        //    disableOtherAnimations();
        //}
        ////roll animation
        //if (rolled == false && Input.GetKey(rollKey) && pc.castingSpell == false)
        //{
        //    pc.RollCamera();
        //    rolled = true;
        //    anim.SetTrigger("Roll");

        //}


        if (Input.GetKeyDown(rollKey) && rolling == false)
        {
            anim.SetTrigger("Roll");
            pc.RollCamera();
        }

        if (rolling == true && rollCurrentTime < rollDuration)
        {
            rollCurrentTime += Time.deltaTime;
            characterController.Move(transform.forward * rollForce * Time.deltaTime);
        }
        if (rollCurrentTime >= rollDuration)
        {
            rollCurrentTime = 0;
            rolling = false;
        }
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

    void disableOtherAnimations()
    {
        anim.SetBool("Moving", false);
        anim.SetBool("run", false);
        anim.SetBool("Walk", false);
        anim.SetBool("runRight", false);
        anim.SetBool("runLeft", false);
        anim.SetBool("walkBack", false);
    }

}

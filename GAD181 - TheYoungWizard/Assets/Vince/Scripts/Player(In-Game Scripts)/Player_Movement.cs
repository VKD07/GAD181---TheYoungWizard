using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;

public class Player_Movement : MonoBehaviour
{

    [HideInInspector] public CharacterController characterController;

    [Header("Player Movement Settings")]
    //[SerializeField] Animator characterAnim;
    [SerializeField] public float xMouseSensitivity = 150;
    [SerializeField] float gravity = 3f;
    [SerializeField] public float currentspeed;
    [SerializeField] public float walkingSpeed;
    [SerializeField] public float runSpeed;
    [SerializeField] public KeyCode rollKey = KeyCode.LeftControl;
    [SerializeField] bool enableOneTimeRoll;
    [SerializeField] bool disableAimMode;
    public bool isMoving = false;
    CapsuleCollider capsuleCollider;
    Vector3 newPos;
    public bool stopMoving;

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
    [SerializeField] float adsCharacterRot = 5f;

    [Header("Character Roll")]
    [SerializeField] float rollForce = 8f;
    [SerializeField] float rollDuration = 0.5f;
    public bool rolling;
    playerCombat pc;
    float rollCurrentTime;

    void Start()
    {
        //setting the cursor to visible and lock to the center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterController = GetComponent<CharacterController>();
        pc = GetComponent<playerCombat>();

        capsuleCollider = GetComponent<CapsuleCollider>();
        thirdPersonCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = xMouseSensitivity;
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

        newPos = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (newPos.magnitude > 0.1f && pc.attacking == false && stopMoving == false)
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
        if (!disableAimMode)
        {
            if (Input.GetKey(KeyCode.Mouse1) && rolling == false)
            {
                Vector3 targetDirection = mainCamera.transform.forward;
                targetDirection.y = 0f;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                float xRotation = Mathf.Clamp(transform.eulerAngles.x, -5f, 12f);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * adsCharacterRot);
            }
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


        if (Input.GetKeyDown(rollKey) && rolling == false && pc.castingSpell == false && rollCurrentTime < rollDuration)
        {
            anim.SetTrigger("Roll");
            pc.RollCamera();
        }

        if (rolling == true && rollCurrentTime < rollDuration)
        {
            rollCurrentTime += Time.deltaTime;
            Vector3 moveDirection = transform.forward * rollForce + Vector3.down * gravity;
            characterController.Move(moveDirection * Time.deltaTime);
        }
        else if (rollCurrentTime >= rollDuration)
        {
            rollCurrentTime = 0;
            rolling = false;

            if (enableOneTimeRoll)
            {
                rollKey = KeyCode.PageUp;
            }
        }
    }

    private void MovementAnimation(Vector3 newPos)
    {
        //Animation -----------------------------------
        if (newPos.magnitude > 0.1f && stopMoving == false)
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

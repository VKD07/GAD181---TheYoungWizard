using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] int playerHealth;

    [Header("Character Animation")]
    [SerializeField] public Animator anim;
    [SerializeField] float lookEnemyRotationSpeed;
    bool paused = false;
    public Transform target;

    // cast mode

   // [SerializeField] GameObject castUI;

    //target mode
    CinemachineComposer midRig;
    [SerializeField] CinemachineFreeLook cam;
    float yAxisValue;
    float xAxisValue;
    [SerializeField] GameObject targetSight;
    bool targetMode = false;

    //Item manager
    //[SerializeField] ItemManager itemManager;

    //Awareness 
   // [SerializeField] GameObject awarenessUI;

    public bool dodge = false;
    public float shieldDuration = 3f;







    void Start()
    {

        //getting the cinemachinefree look mid rig
        midRig = cam.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        //disabling sight at first
        //targetSight.SetActive(false);
        //instantiating midrig values
        cam.m_Lens.FieldOfView = 33f;
        midRig.m_TrackedObjectOffset.x = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {

        print(playerHealth);

        //pause
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            paused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            paused = false;
            Time.timeScale = 1;
        }


        //focusing on targeting the enemy
        targetEnemy();
      //  attack();
        //castMode();

        //Item handler
        //ItemHandler();

        Dodge();


    }

    private void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dodge = true;
        }

        if (dodge == true && shieldDuration > 0)
        {
            shieldDuration -= 0.3f * Time.deltaTime;

            if (shieldDuration <= 0)
            {
                dodge = false;
                shieldDuration = 0.2f;
            }
        }
    }

    //private void castMode()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        if (castUI.activeSelf == false)
    //        {
    //            //activate cast mode UI
    //            castUI.SetActive(true);
    //            //slow down time
    //            Time.timeScale = 0.1f;
    //        }


    //    }
    //}

    private void targetEnemy()
    {
        //sight activated
        if (Input.GetKey(KeyCode.Mouse1))
        {
            targetMode = true;
            targetSight.SetActive(true);

            if (midRig.m_TrackedObjectOffset.x < 0.95f)
            {
                midRig.m_TrackedObjectOffset.x += 5f * Time.deltaTime;
            }

            if (cam.m_Lens.FieldOfView > 20)
            {
                cam.m_Lens.FieldOfView -= 60f * Time.deltaTime;
            }


        }
        else
        {
            targetMode = false;
            targetSight.SetActive(false);



            if (midRig.m_TrackedObjectOffset.x > 0.2371475f)
            {
                midRig.m_TrackedObjectOffset.x -= 5f * Time.deltaTime;
            }



            if (cam.m_Lens.FieldOfView < 30)
            {
                cam.m_Lens.FieldOfView += 60f * Time.deltaTime;
            }

        }

    }

    //public void attack()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0) && targetMode == true)
    //    {
    //        anim.SetTrigger("Attack");


    //    }
    //}

    //private void ItemHandler()
    //{
    //    //if slot 1 is full and player wanted to use it
    //    if (itemManager.isFull[0] == true && Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        itemManager.isFull[0] = false;
    //        itemManager.itemSlots[0].sprite = null;

    //        //if slot 2 is full and player wants to use it
    //    }
    //    else if (itemManager.isFull[1] == true && Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        itemManager.isFull[1] = false;
    //        itemManager.itemSlots[1].sprite = null;
    //    }

    //}

    //take damage from enemy
    public void damagePlayer(int damage)
    {
        if (dodge == false)
        {
            playerHealth -= damage;
        }

    }


    //public void enableSenses()
    //{
    //    awarenessUI.SetActive(true);
    //}

    //public void disableSenses()
    //{
    //    awarenessUI.SetActive(false);
    //}

    //collisions handler


}

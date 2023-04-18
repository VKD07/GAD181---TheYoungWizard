using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TutorialScene : MonoBehaviour
{
    [Header("CutScene Components")]
    [SerializeField] bool[] tutorialSequence;
    [SerializeField] GameObject timeLine;
    [SerializeField] GameObject sceneCamera;
    [Header("Dialog Box")]
    [SerializeField] string[] dialogs;
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] KeyCode nextBtn = KeyCode.Mouse0;
    [Header("Objective Box")]
    [SerializeField] bool[] objectiveSequence;
    [SerializeField] string[] objectives;
    [SerializeField] TextMeshProUGUI objectiveDesc;
    [SerializeField] GameObject objectiveBox;
    public int objNum;
    [Header("Player Components")]
    [SerializeField] Player_Movement pm;
    [SerializeField] playerCombat pCombat;
    [SerializeField] GameObject thirdPersonCam;
    [SerializeField] GameObject castMode;
    [SerializeField] GameObject guideUI;
    [SerializeField] GameObject playerAttrib;


    public int dialogNum;

    [Header("Check Movement")]
    [SerializeField] bool[] keyboardMovements;
    public int keyPressed;
    public bool keyboardMovementDone;

    [Header("Basic Attack Objective")]
    [SerializeField] GameObject movingDummy;
    [SerializeField] Transform dummySpawner;
    bool dummySpawned;


    //timer 
    float transitionDuration;
    float currentTime;
    void Start()
    {
        timeLine.SetActive(true);
        sceneCamera.SetActive(true);
        dialogBox.SetActive(false);
        objectiveBox.SetActive(false);
        thirdPersonCam.SetActive(false);
        pm.enabled = false;
        pCombat.enabled = false;
        //timer for the cutscene
        Invoke("DisableTimeLine", 11f);
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateDialogAndObjectiveText();
        MovementTutorial();
        BasicPracticeTutorial();
        SpawnMovingDummy();
    }

    private void UpdateDialogAndObjectiveText()
    {
        dialogText.SetText(dialogs[dialogNum]);
        objectiveDesc.SetText(objectives[objNum]);
    }

    private void MovementTutorial()
    {
        //dialog box 1
        if (tutorialSequence[0] == true)
        {
            dialogBox.SetActive(true);
            dialogText.SetText(dialogs[0]);
            if (Input.GetKeyDown(nextBtn))
            {
                dialogText.SetText(dialogs[1]);
                objectiveSequence[0] = true;
                thirdPersonCam.SetActive(true);
                DisableCamera();
            }
        }
        //objective 1
        if (objectiveSequence[0] == true)
        {
            objectiveBox.SetActive(true);
            tutorialSequence[0] = false;
            pm.enabled = true;
            objectiveDesc.SetText(objectives[0]);
            CheckIfPlayerMoves();
        }
    }
    //checks if all keys requred have been pressed
    void CheckIfPlayerMoves()
    {
        if (Input.GetKeyDown(KeyCode.W) && !keyboardMovements[0])
        {
            dialogBox.SetActive(false);
            keyboardMovements[0] = true;
            keyPressed++;
        }

        if (Input.GetKeyDown(KeyCode.A) && !keyboardMovements[1])
        {
            dialogBox.SetActive(false);
            keyboardMovements[1] = true;
            keyPressed++;
        }

        if (Input.GetKeyDown(KeyCode.S) && !keyboardMovements[2])
        {
            dialogBox.SetActive(false);
            keyboardMovements[2] = true;
            keyPressed++;
        }

        if (Input.GetKeyDown(KeyCode.D) && !keyboardMovements[3])
        {
            dialogBox.SetActive(false);
            keyboardMovements[3] = true;
            keyPressed++;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !keyboardMovements[4])
        {
            dialogBox.SetActive(false);
            keyboardMovements[4] = true;
            keyPressed++;
        }

        if (keyPressed == 5)
        {
            keyboardMovementDone = true;
            tutorialSequence[1] = true;
        }

    }
    //Starts the Basic attack practice tutorial
    private void BasicPracticeTutorial()
    {
        if (tutorialSequence[1] == true && objectiveSequence[1] == false)
        {
            dialogBox.SetActive(true);
            objectiveBox.SetActive(false);
            dialogText.SetText(dialogs[2]);
            pCombat.enabled = true;
        }

        if (pCombat.enabled == true)
        {
            CheckIfDummyIsDestroyed();
        }
    }

    void CheckIfDummyIsDestroyed()
    {
        GameObject dummy = GameObject.FindGameObjectWithTag("tutorialDummy");
        objectiveBox.SetActive(true);
        objectiveDesc.SetText(objectives[1]);

        if (dummy == null && objectiveSequence[1] == false)
        {
            dialogText.SetText(dialogs[3]);
            dialogBox.SetActive(true);
            objectiveSequence[1] = true;
        }
    }

    void SpawnMovingDummy()
    {
        if (objectiveSequence[1] == true)
        {
            objectiveBox.SetActive(false);
            if (Input.GetKeyDown(nextBtn))
            {
                dialogText.SetText(dialogs[4]);
                tutorialSequence[1] = false;
                tutorialSequence[2] = true;
            }
        }

        if (tutorialSequence[2] == true)
        {
            if (!dummySpawned)
            {
                dummySpawned = true;
                GameObject dummyObj = Instantiate(movingDummy, dummySpawner.position, Quaternion.Euler(0f,-90f,0f));
            }

            if(dummySpawned)
            {
                GameObject findDummy = GameObject.FindGameObjectWithTag("tutorialDummy");

                if(findDummy == null)
                {   
                    dialogText.SetText(dialogs[4]);
                }
            }
        }
    }

    void DisableTimeLine()
    {
        timeLine.SetActive(false);
        tutorialSequence[0] = true;
    }

    void DisableCamera()
    {
        sceneCamera.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class SpellCastingTutorial : MonoBehaviour
{
    [SerializeField] GameObject spellBookMainUI;
    [SerializeField] GameObject spellBookGuide;
    [SerializeField] SphereCollider dummyForceField;
   
    ObjectiveBox objectiveBox;
    public CastModeManager castModeManager;
    public playerCombat pc;
    public MovingDummy tutorialDummy;
    public bool startTaskOne;
    public bool startTaskTwo;
    public bool startTaskThree;
    public bool startTaskFour;
    public bool startTaskFive;
    public bool startTaskSix;
    bool taskOne;
    bool taskTwo;
    bool taskThree;
    bool taskFour;
    bool taskFive;
    bool taskSix;
    public int shieldBroken = 0;

    float shieldSiwtchInterval = 1f;
    public float currentTime;

    [Header("Task Six")]
    [SerializeField] int totalShieldsBrokenRequired;
    [Header("Video Tutorial")]
    [SerializeField] GameObject videoTutorial;
    bool videoOpened;
    bool enableDeactivate;
    float currentVideoTime;
    bool objectiveSet;

    void Start()
    {
        tutorialDummy = FindObjectOfType<MovingDummy>();
        objectiveBox = GetComponent<ObjectiveBox>();
        EnableDummyForceField(false);
    }

    // Update is called once per frame
    void Update()
    {
        TaskOne();
        TaskTwo();
        TaskThree();
        TaskFour();
        TaskFive();
        TaskSix();
    }

    private void TaskSix()
    {
        if (startTaskSix)
        {
            if(tutorialDummy.forceField.activateShield == false && tutorialDummy.forceField.numberOfBrokenShields <= totalShieldsBrokenRequired)
            {
                //updating objective text
                if (!objectiveSet)
                {
                    objectiveSet = true;
                    objectiveBox.SetObjectiveTextNum(9, "");
                }

                if (currentTime < shieldSiwtchInterval)
                {
                    currentTime += Time.deltaTime;
                }
                else if(currentTime >= shieldSiwtchInterval)
                {
                    tutorialDummy.forceField.activateShield = true;
                    currentTime = 0f;
                }
            }

            if(tutorialDummy.forceField.numberOfBrokenShields >= totalShieldsBrokenRequired)
            {
                taskSix = true;
                startTaskSix = false;
            }
        }
    }

    private void TaskFive()
    {
        if (startTaskFive)
        {
            if (tutorialDummy.forceField.activateShield == false)
            {
                taskFive = true;
            }
        }
    }

    private void TaskFour()
    {
        if (startTaskFour)
        {
            if (Input.GetKeyDown(KeyCode.E) && taskThree)
            {
                taskFour = true;
            }
        }
    }

    private void TaskThree()
    {
        if (startTaskThree)
        {
            if (!videoOpened)
            {
                videoOpened = true;
                videoTutorial.SetActive(true);
                Time.timeScale = 0f;
            }
            DisableVideo(3);

            if (videoTutorial.activeSelf && enableDeactivate)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Time.timeScale = 1f;
                    videoTutorial.SetActive(false);
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    UnityEngine.Cursor.visible = false;
                }
            }


            if (castModeManager != null)
            {
                if (castModeManager.correctCombination)
                {
                    taskThree = true;
                }
            }
        }
    }

    private void TaskTwo()
    {
        if (startTaskTwo)
        {
            if (spellBookMainUI.activeSelf == true)
            {
                taskTwo = true;
            }
        }
    }

    private void TaskOne()
    {
        if (startTaskOne)
        {
            tutorialDummy.startShielding = true;
        }

        if (tutorialDummy.forceField.activateShield == true)
        {
            taskOne = true;
        }
    }

    public bool TaskOneDone()
    {
        return taskOne;
    }

    public bool TaskTwoDone()
    {
        return taskTwo;
    }

    public bool TaskThreeDone()
    {
        return taskThree;
    }

    public bool TaskFourDone()
    {
        return taskFour;
    }

    public bool TaskFiveDone()
    {
        return taskFive;
    }

    public bool TaskSixDone()
    {
        return taskSix;
    }

    public void EnableDummyForceField(bool value)
    {
        if (value)
        {
            dummyForceField.enabled = true;
        }
        else
        {
            dummyForceField.enabled = false;
        }
    }

    void DisableVideo(float time)
    {
        if (currentVideoTime < time)
        {
            currentVideoTime += Time.unscaledDeltaTime;
        }
        else
        {
            enableDeactivate = true;
            currentVideoTime = 0;
        }
    }
}

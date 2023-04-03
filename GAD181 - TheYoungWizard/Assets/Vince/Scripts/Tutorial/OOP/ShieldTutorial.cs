using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTutorial : MonoBehaviour
{
    public bool slowDownTime;
    [SerializeField] GameObject playerForceField;
    [SerializeField] GameObject spaceBtnUI;
    [SerializeField] ObjectiveBox objectiveBox;
    MovingDummy tutorialDummy;
    int shieldBlock;
    playerCombat pc;
    public bool startTaskTwo;
    bool taskOne;
    bool taskTwo;

    public float attackingDuration = 5f;
    public float currentTime;
    float cutSceneFireBallDuration = 2.5f;
    public float currentCutSceneTime;
    [Header("Blocking fireball task")]
    [SerializeField] int totalBlockedFireBallsrequired;
    public int currentFireBallsBlocked;

    void Start()
    {
        pc = FindObjectOfType<playerCombat>();
        tutorialDummy = FindAnyObjectByType<MovingDummy>();

    }

    // Update is called once per frame
    void Update()
    {
        TaskOne();
        TaskTwo();
    }

    private void TaskOne()
    {
        if (slowDownTime)
        {
            if(currentCutSceneTime < cutSceneFireBallDuration)
            {
                Time.timeScale = 0.2f;
                pc.enableSenses();
                spaceBtnUI.SetActive(true);
                currentCutSceneTime += Time.deltaTime * 4f;
            }
            else
            {
                Time.timeScale = 0f;
            }
           
        }

        if (slowDownTime && playerForceField.activeSelf == true)
        {
            taskOne = true;
            Time.timeScale = 1f;
            slowDownTime = false;
            pc.disableSenses();
            spaceBtnUI.SetActive(false);
        }
    }

    void TaskTwo()
    {
        if (startTaskTwo)
        {
            if (currentFireBallsBlocked < totalBlockedFireBallsrequired)
            {
                tutorialDummy.shieldTask = true;
                tutorialDummy.startAttack = true;
                //Updating the objective text
                objectiveBox.SetObjectiveTextNum(4, $"{currentFireBallsBlocked} / {totalBlockedFireBallsrequired}");

            }
            else
            {
                tutorialDummy.startAttack = false;
                startTaskTwo = false;
                taskTwo = true;
            }
        }
    }

    public void DisableDummy(bool value)
    {
        tutorialDummy.dontDamage = value;
    }

    public bool shieldTask1Done()
    {
        return taskOne;
    }

    public bool BlockFireBallTaskDone()
    {
        return taskTwo;
    }
}

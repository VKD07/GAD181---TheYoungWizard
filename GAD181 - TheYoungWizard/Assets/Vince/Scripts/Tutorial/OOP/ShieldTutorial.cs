using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTutorial : MonoBehaviour
{
    public bool slowDownTime;
    [SerializeField] GameObject playerForceField;
    int shieldBlock;
    playerCombat pc;
    bool taskOne;
    void Start()
    {
        pc = FindObjectOfType<playerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        TaskOne();
    }

    private void TaskOne()
    {
        if (slowDownTime)
        {
            Time.timeScale = 0.2f;
            pc.enableSenses();
        }

        if(slowDownTime && playerForceField.activeSelf == true)
        {
            taskOne = true;
            Time.timeScale = 1f;
            slowDownTime = false;
            pc.disableSenses();
        }
    }


    public bool shieldTask1Done()
    {
        return taskOne;
    }

}

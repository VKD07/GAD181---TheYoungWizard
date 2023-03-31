using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor.Rendering;
using UnityEngine;

public class BasicAttackTutorial : MonoBehaviour
{
    [SerializeField] GameObject movingDummy;
    [SerializeField] Transform dummySpawner;
    MovingDummy dummyScript;
    bool firstTask;
    public bool secondTask;
    public bool spawnMovingDummy;

    // Update is called once per frame
    void Update()
    {
        CheckIfDummyIsDown();
    }

    private void CheckIfDummyIsDown()
    {
        dummyScript = GameObject.FindGameObjectWithTag("tutorialDummy").GetComponent<MovingDummy>();

        if (dummyScript != null)
        {
            if (dummyScript.isDead == true)
            {
                firstTask = true;
            }
        }
    }

    public void resetDummy()
    {
        dummyScript.ResetDummy();
    }

    public void MoveDummy(bool value)
    {
        dummyScript.startMoving = value;
    }

    //private void CheckIfDummyIsDead()
    //{

    //    movingObj = GameObject.FindGameObjectWithTag("movingDummy");

    //    if (dummy == null)
    //    {
    //        dummyDestroyed = true;
    //    }

    //    if (spawnMovingDummy && movingObj == null)
    //    {
    //        movingDummyDestroyed = true;
    //    }
    //}

    //public void SpawnMovingDummy()
    //{
    //    if (!spawnMovingDummy)
    //    {
    //        spawnMovingDummy = true;
    //        Instantiate(movingDummy, dummySpawner.position, Quaternion.Euler(0, 90f, 0));
    //    }
    //}


    public bool FirstTaskDone()
    {
        return firstTask;
    }

    public bool SecondTaskDone()
    {
        return secondTask;
    }
}

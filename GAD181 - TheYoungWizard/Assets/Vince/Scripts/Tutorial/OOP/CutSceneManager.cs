using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutSceneManager : MonoBehaviour
{
    [Header("CutScene Components")]
    [SerializeField] bool[] tutorialSequence;
    [SerializeField] GameObject timeLine;
    [SerializeField] PlayableAsset[] clips;
    [SerializeField] GameObject sceneCamera;
    [SerializeField] KeyCode nextBtn = KeyCode.Mouse0;
    public bool textIsUpdated;
    PlayerComponentsHandler playerComponentsHandler;
    DialogBox dialogBox;
    ObjectiveBox objectiveBox;

    [Header("Scene Vfx")]
    [SerializeField] GameObject fireBall1;
    [SerializeField] GameObject fireBall2;
    [SerializeField] Transform fireBallSpawner;
    bool fireBallIsSpawned;

    [Header("Task References")]
    PlayerMovementTutorial pmTutorial;
    BasicAttackTutorial basicAttackTutorial;
    ShieldTutorial shieldTutorial;

    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        objectiveBox = FindObjectOfType<ObjectiveBox>();
        playerComponentsHandler = FindObjectOfType<PlayerComponentsHandler>();
        pmTutorial = FindObjectOfType<PlayerMovementTutorial>();
        basicAttackTutorial = FindObjectOfType<BasicAttackTutorial>();
        shieldTutorial = FindObjectOfType<ShieldTutorial>();

        //disabling text boxes in the beginning
        fireBall1.SetActive(false);
        dialogBox.EnableDialogBox(false);
        objectiveBox.EnableObjectiveBox(false);
        Invoke("DisableTimeLine", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        #region MovementTutorial
        if (tutorialSequence[0] && !textIsUpdated)
        {
            tutorialSequence[1] = true;
            textIsUpdated = true;
            dialogBox.EnableDialogBox(true);
            //setting the dialog
            dialogBox.SetDialogTextNum(0);
        }

        if (tutorialSequence[1] && Input.GetKeyDown(nextBtn))
        {
            tutorialSequence[1] = false;
            tutorialSequence[0] = false;
            textIsUpdated = false;
            dialogBox.nextLine();
            //enabling objective box
            objectiveBox.EnableObjectiveBox(true);
            objectiveBox.SetDialogTextNum(0);
            //enabling player control
            sceneCamera.SetActive(false);
            playerComponentsHandler.EnablePlayerMovement(true);
            tutorialSequence[2] = true;
        }

        if (tutorialSequence[2])
        {
            pmTutorial.CheckIfPlayerMoves();

            if (pmTutorial.KeyboardMovementDone())
            {
                if (!textIsUpdated)
                {
                    dialogBox.EnableDialogBox(true);
                    textIsUpdated = false;
                    dialogBox.nextLine();
                    tutorialSequence[2] = false;
                    tutorialSequence[3] = true;
                }
            }
        }
        #endregion

        #region Basic Attack Tutorial
        if (tutorialSequence[3])
        {
            objectiveBox.SetDialogTextNum(1);
            playerComponentsHandler.EnablePlayerCombat();

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                dialogBox.EnableDialogBox(false);
            }
            if (basicAttackTutorial.FirstTaskDone())
            {
                if (!textIsUpdated)
                {
                    textIsUpdated = true;
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine();
                    tutorialSequence[3] = false;
                    tutorialSequence[4] = true;
                }
            }
        }

        if (tutorialSequence[4])
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                textIsUpdated = false;
                basicAttackTutorial.resetDummy();
                basicAttackTutorial.MoveDummy(true);
                dialogBox.EnableDialogBox(false);
            }

            if (basicAttackTutorial.SecondTaskDone())
            {
                if (!textIsUpdated)
                {
                    textIsUpdated = true;
                    objectiveBox.EnableObjectiveBox(false);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.SetDialogTextNum(2);
                    tutorialSequence[4] = false;
                    tutorialSequence[5] = true;
                    textIsUpdated = false;
                }
            }
        }
        #endregion

        #region Shield Tutorial
        if (tutorialSequence[5])
        {
            if (Input.GetKeyDown(nextBtn))
            {
                basicAttackTutorial.resetDummy();
                dialogBox.EnableDialogBox(false);
                fireBall1.SetActive(true);
                sceneCamera.SetActive(true);
                timeLine.SetActive(true);
                timeLine.GetComponent<PlayableDirector>().playableAsset = clips[0];
                timeLine.GetComponent<PlayableDirector>().Play();
                StartCoroutine(DisableScenes(5, 2.5f));
            }
        }

        if (tutorialSequence[6])
        {
            //if (Input.GetKeyDown(nextBtn))
            //{
            //    dialogBox.EnableDialogBox(true);
            //    if (!textIsUpdated)
            //    {
            //        textIsUpdated = false;
            //        dialogBox.nextLine();
            //    }
            //}

            if (!fireBallIsSpawned)
            {
                fireBallIsSpawned = true;
                Instantiate(fireBall2, fireBallSpawner.position, Quaternion.identity);
                shieldTutorial.slowDownTime = true;
                objectiveBox.EnableObjectiveBox(true);
                objectiveBox.nextLine();
            }

            if(shieldTutorial.shieldTask1Done())
            {
                if (!textIsUpdated)
                {
                    objectiveBox.EnableObjectiveBox(false);
                    dialogBox.EnableDialogBox(false);
                    textIsUpdated = true;
                    dialogBox.nextLine();
                }
            }
        }

        #endregion
    }

    void DisableTimeLine()
    {
        timeLine.SetActive(false);
        tutorialSequence[0] = true;
    }

    IEnumerator DisableScenes(int index, float time)
    {
        yield return new WaitForSeconds(time);
       // fireBall.SetActive(false);
        timeLine.SetActive(false);
        sceneCamera.SetActive(false);
        tutorialSequence[index] = false;
        tutorialSequence[index + 1] = true;
    }

    void UpdateTextOfDialog(int index)
    {

    }
}

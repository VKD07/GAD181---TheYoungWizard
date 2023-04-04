using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class CutSceneManager : MonoBehaviour
{
    [Header("CutScene Components")]
    [SerializeField] bool[] tutorialSequence;
    [SerializeField] GameObject timeLine;
    [SerializeField] PlayableAsset[] clips;
    [SerializeField] GameObject sceneCamera;
    [SerializeField] GameObject sceneCamera2;
    [SerializeField] KeyCode nextBtn = KeyCode.Mouse0;
    [SerializeField] GameObject dummy;
    public bool textIsUpdated;
    PlayerComponentsHandler playerComponentsHandler;
    DialogBox dialogBox;
    ObjectiveBox objectiveBox;
    bool healing;

    [Header("Scene Vfx")]
    [SerializeField] GameObject fireBall1;
    [SerializeField] GameObject fireBall2;
    [SerializeField] Transform fireBallSpawner;
    [SerializeField] ParticleSystem playerHealVfx;
    bool fireBallIsSpawned;
    bool ScenePlaying;

    [Header("Task References")]
    [SerializeField] GameObject tutorialCollider;
    [SerializeField] GameObject spellBookMainUI;
    PlayerMovementTutorial pmTutorial;
    BasicAttackTutorial basicAttackTutorial;
    ShieldTutorial shieldTutorial;
    HealAndManaTutorial healandManaTutorial;
    SpellCastingTutorial spellCastingTutorial;
    ExitScne exitScene;
    GuideUiScript spellBook;

    //timers
    public float typingDuration = 2f;
    public float currentTime;
    public bool countingDownNext;



    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        objectiveBox = FindObjectOfType<ObjectiveBox>();
        playerComponentsHandler = FindObjectOfType<PlayerComponentsHandler>();
        pmTutorial = FindObjectOfType<PlayerMovementTutorial>();
        basicAttackTutorial = FindObjectOfType<BasicAttackTutorial>();
        shieldTutorial = FindObjectOfType<ShieldTutorial>();
        healandManaTutorial = FindAnyObjectByType<HealAndManaTutorial>();
        spellCastingTutorial = FindObjectOfType<SpellCastingTutorial>();
        spellBook = FindObjectOfType<GuideUiScript>();
        exitScene = FindObjectOfType<ExitScne>();
        //disabling text boxes in the beginning
        fireBall1.SetActive(false);
        dialogBox.EnableDialogBox(false);
        objectiveBox.EnableObjectiveBox(false);
        playerComponentsHandler.DisableCastMode(true);
        Invoke("DisableTimeLine", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        typing();
        #region MovementTutorial
        if (tutorialSequence[0])
        {
            if (!countingDownNext)
            {
                dialogBox.EnableDialogBox(true);
                dialogBox.SetDialogTextNum(0);
                tutorialSequence[0] = false;
                tutorialSequence[1] = true;
                countingDownNext = true;
            }
        }

        else if (tutorialSequence[1] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.nextLine(1);
                    //enabling objective box
                    objectiveBox.EnableObjectiveBox(true);
                    objectiveBox.SetObjectiveTextNum(0, "");
                    //enabling player control
                    sceneCamera2.SetActive(false);
                    playerComponentsHandler.EnablePlayerMovement(true);
                    tutorialSequence[1] = false;
                    tutorialSequence[2] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[2] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                pmTutorial.CheckIfPlayerMoves();

                if (pmTutorial.KeyboardMovementDone())
                {
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(2);
                    tutorialSequence[2] = false;
                    tutorialSequence[3] = true;
                    countingDownNext = true;
                }
            }
        }
        #endregion

        #region Basic Attack Tutorial
        else if (tutorialSequence[3] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                objectiveBox.SetObjectiveTextNum(1, "");
                playerComponentsHandler.EnablePlayerCombat();

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    dialogBox.EnableDialogBox(false);
                }
                if (basicAttackTutorial.FirstTaskDone())
                {
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(3);
                    tutorialSequence[3] = false;
                    tutorialSequence[4] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[4] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    basicAttackTutorial.resetDummy();
                    basicAttackTutorial.MoveDummy(true);
                    dialogBox.EnableDialogBox(false);
                }

                if (basicAttackTutorial.SecondTaskDone())
                {
                    objectiveBox.EnableObjectiveBox(false);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(4);
                    tutorialSequence[4] = false;
                    tutorialSequence[5] = true;
                    countingDownNext = true;
                }
            }
        }
        #endregion

        #region Shield Tutorial
        else if (tutorialSequence[5] && !dialogBox.isTyping)
        {
            shieldTutorial.DisableDummy(true);
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    nextBtn = KeyCode.Clear;
                    basicAttackTutorial.resetDummy();
                    dialogBox.EnableDialogBox(false);
                    fireBall1.SetActive(true);
                    sceneCamera.SetActive(true);
                    timeLine.SetActive(true);
                    dummy.transform.position = new Vector3(677.22f, 0.74f, 350.14f);
                    PlayCutScene();
                    StartCoroutine(DisableScenes(5, 3f));

                }
            }
        }

        else if (tutorialSequence[6] && !dialogBox.isTyping)
        {
            shieldTutorial.slowDownTime = true;
            if (!countingDownNext)
            {
                if (!fireBallIsSpawned)
                {
                    fireBallIsSpawned = true;
                    if (fireBallSpawner != null)
                    {
                        Instantiate(fireBall2, fireBallSpawner.position, Quaternion.identity);
                    }
                    objectiveBox.EnableObjectiveBox(true);
                    objectiveBox.SetObjectiveTextNum(2, "");
                }

                if (shieldTutorial.shieldTask1Done())
                {
                    shieldTutorial.slowDownTime = false;
                    playerComponentsHandler.EnablePlayerAttrib(false);
                    objectiveBox.EnableObjectiveBox(false);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(5);
                    tutorialSequence[6] = false;
                    tutorialSequence[7] = true;
                    countingDownNext = true;

                }

            }
        }

        else if (tutorialSequence[7] && !dialogBox.isTyping)
        {
           

            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    shieldTutorial.taskOne = false;
                    dialogBox.nextLine(6);
                    tutorialSequence[7] = false;
                    tutorialSequence[8] = true;
                    countingDownNext = true;
                }
            }
        }


        ///add one more line here fireball Gave damage

        else if (tutorialSequence[8] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.EnableDialogBox(false);
                    objectiveBox.EnableObjectiveBox(true);
                    objectiveBox.SetObjectiveTextNum(3, "");
                    playerComponentsHandler.EnablePlayerAttrib(true);

                }
                if (Input.GetKeyDown(KeyCode.Alpha1)) //-------------------------------- healing
                {
                    playerHealVfx.Play();
                    objectiveBox.EnableObjectiveBox(false);
                    dialogBox.EnableDialogBox(true);
                    playerComponentsHandler.EnablePlayerAttrib(false);
                    dialogBox.nextLine(7);
                    tutorialSequence[8] = false;
                    tutorialSequence[9] = true;
                    countingDownNext = true;
                }
            }

        }

    


        else if (tutorialSequence[9] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.nextLine(8);
                    tutorialSequence[9] = false;
                    tutorialSequence[10] = true;
                    countingDownNext = true;
                }
            }
        }


        else if (tutorialSequence[10] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.EnableDialogBox(false);
                    objectiveBox.EnableObjectiveBox(true);
                    shieldTutorial.startTaskTwo = true;
                }

                if (shieldTutorial.BlockFireBallTaskDone())
                {
                    tutorialSequence[10] = false;
                    tutorialSequence[11] = true;
                    objectiveBox.SetObjectiveTextNum(5, "");
                    countingDownNext = true;
                }
            }
        }

        #endregion
        ////------------------ tutorial shield continue. Shield 3 fireballs to move to next task. which is resitance bbreak 

        #region spellCast tutorial
        else if (tutorialSequence[11] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                spellCastingTutorial.startTaskOne = true;
                if (spellCastingTutorial.TaskOneDone())
                {
                    playerComponentsHandler.EnablePlayerAttrib(false);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(9);
                    objectiveBox.EnableObjectiveBox(false);
                    tutorialSequence[11] = false;
                    tutorialSequence[12] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[12] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.nextLine(10);
                    tutorialSequence[12] = false;
                    tutorialSequence[13] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[13] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.nextLine(11);
                    tutorialSequence[13] = false;
                    tutorialSequence[14] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[14] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.EnableDialogBox(false);
                    playerComponentsHandler.EnableSpellBook(true);
                    objectiveBox.EnableObjectiveBox(true);
                    objectiveBox.SetObjectiveTextNum(6, "");
                    spellCastingTutorial.startTaskTwo = true;
                    countingDownNext = true;
                }

                if (spellCastingTutorial.TaskTwoDone())
                {
                    dialogBox.EnableDialogBox(true);
                    objectiveBox.EnableObjectiveBox(false);
                    dialogBox.nextLine(12);
                    tutorialSequence[14] = false;
                    tutorialSequence[15] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[15] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    spellBook.spellBookOpened = false;
                    spellBookMainUI.SetActive(false);
                    dialogBox.nextLine(13);
                    tutorialSequence[15] = false;
                    tutorialSequence[16] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[16] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    tutorialCollider.gameObject.SetActive(false);
                    playerComponentsHandler.DisableCastMode(false);
                    playerComponentsHandler.EnableSpellCastUI(true);
                    dialogBox.EnableDialogBox(false);
                    objectiveBox.EnableObjectiveBox(true);
                    objectiveBox.SetObjectiveTextNum(7, "");
                    spellCastingTutorial.startTaskThree = true;

                }

                if (spellCastingTutorial.TaskThreeDone())
                {
                    dialogBox.EnableDialogBox(true);
                    objectiveBox.EnableObjectiveBox(false);
                    dialogBox.nextLine(14);
                    tutorialSequence[16] = false;
                    tutorialSequence[17] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[17] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.nextLine(15);
                    tutorialSequence[17] = false;
                    tutorialSequence[18] = true;
                    countingDownNext = true;
                }
            }
        }


        else if (tutorialSequence[18] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.EnableDialogBox(false);
                    objectiveBox.EnableObjectiveBox(true);
                    objectiveBox.SetObjectiveTextNum(8, "");
                    spellCastingTutorial.startTaskFour = true;
                }

                if (spellCastingTutorial.TaskFourDone())
                {
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(16);
                    objectiveBox.EnableObjectiveBox(false);
                    tutorialSequence[18] = false;
                    tutorialSequence[19] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[19] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.nextLine(17);
                    tutorialSequence[19] = false;
                    tutorialSequence[20] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[20] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    spellCastingTutorial.EnableDummyForceField(true);
                    dialogBox.EnableDialogBox(false);
                    objectiveBox.EnableObjectiveBox(true);
                    objectiveBox.SetObjectiveTextNum(9, "");
                    spellCastingTutorial.startTaskFive = true;
                }
                if (spellCastingTutorial.TaskFiveDone())
                {
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(18);
                    objectiveBox.EnableObjectiveBox(false);
                    tutorialSequence[20] = false;
                    tutorialSequence[21] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[21] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    dialogBox.EnableDialogBox(false);
                    objectiveBox.EnableObjectiveBox(true);
                    // objectiveBox.SetObjectiveTextNum(9, "");
                    spellCastingTutorial.startTaskSix = true;
                }

                if (spellCastingTutorial.TaskSixDone())
                {
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(19);
                    objectiveBox.EnableObjectiveBox(false);
                    tutorialSequence[21] = false;
                    tutorialSequence[22] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[22] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextBtn))
                {
                    playerComponentsHandler.EnableSpellCastUI(false);
                    playerComponentsHandler.EnableSpellBook(false);
                    dialogBox.nextLine(20);
                    tutorialSequence[22] = false;
                    tutorialSequence[23] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (tutorialSequence[23] && !dialogBox.isTyping)
        {
            if (Input.GetKeyDown(nextBtn))
            {
                if (!countingDownNext)
                {
                   // dialogBox.nextLine(21);
                    tutorialSequence[23] = false;
                    tutorialSequence[24] = true;
                    countingDownNext = true;
                }
            }
        }
        #endregion

        #region Exit
        else if (tutorialSequence[24] && !dialogBox.isTyping)
        {
            if (!countingDownNext)
            {
                exitScene.startFade = true;
                playerComponentsHandler.EnableSpellBook(false);
                playerComponentsHandler.DisablePlayerAnimation();
                dialogBox.EnableDialogBox(false);
                tutorialSequence[24] = false;
            }
        }
        #endregion
    }

    void DisableTimeLine()
    {
        timeLine.SetActive(false);
        tutorialSequence[0] = true;
        countingDownNext = true;
    }

    void PlayCutScene()
    {
        if (!ScenePlaying)
        {
            ScenePlaying = true;
            timeLine.GetComponent<PlayableDirector>().playableAsset = clips[0];
            timeLine.GetComponent<PlayableDirector>().Play();
        }
    }

    IEnumerator DisableScenes(int index, float time)
    {
        yield return new WaitForSeconds(time);
        countingDownNext = true;
        tutorialSequence[6] = true;
        nextBtn = KeyCode.Mouse0;
        sceneCamera2.SetActive(false);
        sceneCamera.SetActive(false);
        tutorialSequence[index] = false;
    }

    void typing()
    {
        if (countingDownNext && currentTime < typingDuration)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            countingDownNext = false;
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneHandler : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] playerCombat pc;
    [SerializeField] Player_Movement pm;
    [SerializeField] Player_SpellCast ps;
    [SerializeField] Player_Animation_Config config;
    [SerializeField] PlayerForceField pf;
    [SerializeField] GameObject thirdPersonCamera;
    [SerializeField] GameObject castModeUI;
    [SerializeField] CanvasGroup playerAttribUI;
    [SerializeField] GameObject guideUI;
    [SerializeField] GameObject aimCanvas;
    [SerializeField] Animator playerAnim;
    [SerializeField] Transform playerTransform;
    [SerializeField] StoneSpell stoneSpell;
    [SerializeField] AudioSource playerAudioSource;

    [Header("Dialog Box")]
    [SerializeField] GameObject dialogCanvas;
    [SerializeField] DialogBox dialogBox;
    [SerializeField] KeyCode nextKey = KeyCode.Mouse0;

    [Header("Sequence")]
    [SerializeField] bool[] sequence;
    public float typingDuration = 2f;
    public float currentTime;
    public bool countingDownNext;

    [Header("Colliders")]
    [SerializeField] Collider[] dialogCollider;
    [SerializeField] Collider playerCollider;
    [SerializeField] EnemyHandler enemyHandler;

    [Header("Cameras")]
    [SerializeField] GameObject[] sceneCamera;
    float cameraTimer = 5f;
    float currentCameraTimer;

    [Header("BossComponents")]
    [SerializeField] BossScript bossScript;
    [SerializeField] GameObject bossHealth;
    [SerializeField] BossHealthHandler bossHealthHandler;

    [Header("Audio")]
    [SerializeField] AudioHandler audioHandler;

    void Awake()
    {
        LockCursor();
        EnablePlayerComponents(false);
        FirstDialog();
        StartCoroutine(EnableSecondDialog(1));
    }
    // Update is called once per frame
    void Update()
    {
        typing();
        SecondDialog();
        ThirdDialog();
        LightenUpTheRoom();
        BossMeetsKael();
    }

    private void FirstDialog()
    {
        dialogBox.EnableDialogBox(true);
        dialogBox.SetDialogTextNum(0);
    }

    private void SecondDialog()
    {
        if (sequence[0])
        {
            if (!countingDownNext && !dialogBox.isTyping)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(1);
                    sequence[0] = false;
                    sequence[1] = true;
                    countingDownNext = true;
                }
            }
        }
        else if (sequence[1])
        {
            if (!countingDownNext && !dialogBox.isTyping) //enabling player controls after second dialog
            {
                if (Input.GetKeyDown(nextKey))
                {
                    EnablePlayerControl();
                    sequence[1] = false;
                    sequence[2] = true;
                    countingDownNext = true;
                }
            }
        }

        if (sequence[2])
        {
            if (!countingDownNext)
            {
                if (dialogCollider[0].bounds.Intersects(playerCollider.bounds))
                {
                    EnablePlayerComponents(false);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(2);
                    countingDownNext = true;
                    sequence[2] = false;
                    sequence[3] = true;
                }
            }
        }
        else if (sequence[3])
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    EnablePlayerControl();
                    dialogBox.EnableDialogBox(false);
                    countingDownNext = true;
                    sequence[3] = false;
                    sequence[4] = true;
                }
            }
        }
    }

    void ThirdDialog()
    {
        if (sequence[4])
        {
            if (stoneSpell.unlocked)
            {
                if (!countingDownNext)
                {
                    EnablePlayerComponents(false);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(4);
                    countingDownNext = true;
                    sequence[4] = false;
                    sequence[5] = true;
                }
            }
        }
        else if (sequence[5])
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(5);
                    countingDownNext = true;
                    sequence[5] = false;
                    sequence[6] = true;
                }
            }
        }
        else if (sequence[6])
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    countingDownNext = true;
                    sequence[6] = false;
                    sequence[7] = true;
                }
            }
        }
    }
    void LightenUpTheRoom()
    {
        if (enemyHandler.finalStage)
        {
            if (sequence[7])
            {
                if (!countingDownNext)
                {
                    EnablePlayerComponents(false);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(6);
                    countingDownNext = true;
                    sequence[7] = false;
                    sequence[8] = true;
                }
            }
            else if (sequence[8])
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        dialogBox.nextLine(7);
                        countingDownNext = true;
                        sequence[8] = false;
                        sequence[9] = true;
                    }
                }
            }
            else if (sequence[9])
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        dialogBox.EnableDialogBox(false);
                        EnablePlayerControl();
                        countingDownNext = true;
                        sequence[9] = false;
                        sequence[10] = true;
                    }
                }
            }
        }
    }

    void BossMeetsKael()
    {
        if (enemyHandler.spawnBoss)
        {
            if (sequence[10])
            {
                if (currentCameraTimer < cameraTimer)
                {
                    audioHandler.PlayBossReveal();
                    playerTransform.position = new Vector3(-25.62f, -4.473627f, -29.39f);
                    thirdPersonCamera.SetActive(false);
                    currentCameraTimer += Time.deltaTime;
                    EnablePlayerComponents(false);
                    playerAnim.enabled = true;
                    EnableBossComponents(false);
                    sceneCamera[0].SetActive(true);
                }
                else
                {
                    currentCameraTimer = 0;
                    sequence[10] = false;
                    sequence[11] = true;
                }

            }

            else if (sequence[11]) //kael talks to furball
            {
                sceneCamera[0].SetActive(false);
                sceneCamera[1].SetActive(true);

                if (!countingDownNext && !dialogBox.isTyping)
                {
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(8);
                    sequence[11] = false;
                    sequence[12] = true;
                    countingDownNext = true;
                }
            }
            else if (sequence[12])
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        dialogBox.EnableDialogBox(true);
                        dialogBox.nextLine(9);
                        sequence[12] = false;
                        sequence[13] = true;
                        countingDownNext = true;
                    }
                }
            }
            else if (sequence[13])
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        sceneCamera[1].SetActive(false);
                        sceneCamera[2].SetActive(true);
                        dialogBox.nextLine(10);
                        sequence[13] = false;
                        sequence[14] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[14])
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        dialogBox.nextLine(11);
                        sequence[14] = false;
                        sequence[15] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[15]) //Line: I dont understand...
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        sceneCamera[1].SetActive(true);
                        sceneCamera[2].SetActive(false);
                        dialogBox.nextLine(12);
                        sequence[15] = false;
                        sequence[16] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[16]) //Line: Furball: I was the reason why your spells are scattered. 
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        sceneCamera[1].SetActive(false);
                        sceneCamera[2].SetActive(true);
                        dialogBox.nextLine(13);
                        sequence[16] = false;
                        sequence[17] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[17]) //Line: Furball:I was the one who turned on the portal.
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        dialogBox.nextLine(14);
                        sequence[17] = false;
                        sequence[18] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[18]) //Line: Furball: You we're too busy with your training, so I have to do it.
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        dialogBox.nextLine(15);
                        sequence[18] = false;
                        sequence[19] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[19]) //Line: Furball: Now that you have collected all of your spells, I won't allow you to go back home!
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        dialogBox.nextLine(16);
                        sequence[19] = false;
                        sequence[20] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[20]) //Line: KAEL: I still dont understand. Let's just go home.
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        sceneCamera[1].SetActive(true);
                        sceneCamera[2].SetActive(false);
                        dialogBox.nextLine(17);
                        sequence[20] = false;
                        sequence[21] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[21]) //Line:Furball: You have to kill me first.
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        sceneCamera[1].SetActive(false);
                        sceneCamera[2].SetActive(true);
                        dialogBox.nextLine(18);
                        sequence[21] = false;
                        sequence[22] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[22]) //Line:KAEL: You don't need to do this.
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        sceneCamera[1].SetActive(true);
                        sceneCamera[2].SetActive(false);
                        dialogBox.nextLine(19);
                        sequence[22] = false;
                        sequence[23] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[23]) //Begins to battle
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        audioHandler.PlayBossFightMusic();
                        sceneCamera[1].SetActive(false);
                        sceneCamera[2].SetActive(false);
                        dialogBox.EnableDialogBox(false);
                        thirdPersonCamera.SetActive(true);
                        EnableBossComponents(true);
                        bossHealth.SetActive(true);
                        EnablePlayerControl();
                        sequence[23] = false;
                        sequence[24] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (sequence[24])
            {
                if (bossHealthHandler.firstRockShattered)
                {
                    if (currentCameraTimer < cameraTimer)
                    {
                        currentCameraTimer += Time.deltaTime;
                        sceneCamera[3].SetActive(true);
                        EnableBossComponents(false);
                        bossHealth.SetActive(false);
                        thirdPersonCamera.SetActive(false);
                        EnablePlayerComponents(false);
                    }
                    else
                    {
                        currentCameraTimer = 0;

                        sequence[24] = false;
                        sequence[25] = true;
                    }
                }
            }
            else if (sequence[25])
            {
                if (!countingDownNext)
                {
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(20);
                    sequence[25] = false;
                    sequence[26] = true;
                    countingDownNext = true;
                }
            }
            else if (sequence[26])
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        sceneCamera[3].SetActive(false);
                        dialogBox.EnableDialogBox(false);
                        thirdPersonCamera.SetActive(true);
                        EnableBossComponents(true);
                        bossHealth.SetActive(true);
                        EnablePlayerControl();
                        sequence[26] = false;
                        sequence[27] = true;
                        countingDownNext = true;
                    }
                }
            }
        }
    }


    IEnumerator EnableSecondDialog(float time)
    {
        yield return new WaitForSeconds(time);
        sequence[0] = true;
        countingDownNext = true;
    }

    void EnablePlayerComponents(bool value)
    {
        castModeUI.SetActive(value);
        guideUI.SetActive(value);
        aimCanvas.SetActive(value);
        pm.enabled = value;
        ps.enabled = value;
        config.enabled = value;
        pf.enabled = value;
        pc.enabled = value;
        playerAudioSource.enabled = value;
        playerAnim.SetBool("dialogIdle", !value);

        if (value)
        {
            playerAttribUI.alpha = 1;
        }
        else
        {
            playerAttribUI.alpha = 0;
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void EnablePlayerControl()
    {
        EnablePlayerComponents(true);
        dialogBox.EnableDialogBox(false);
    }

    void EnableBossComponents(bool value)
    {
        bossScript.enabled = value;
        //Disable boss componenets
        // turn off lighting before boss fight
        // add more camera for dialogs between kael and boss
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

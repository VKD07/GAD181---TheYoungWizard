using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestLevelCutScene : MonoBehaviour
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

    [Header("Environment Components")]
    [SerializeField] EnemyDeathCounter enemyDeathHandler;
    [SerializeField] GameObject chestBlocker;
    [SerializeField] chestScript chest;
    [SerializeField] CampFire campFire;
    [SerializeField] GameObject campfireBlocker;
    [SerializeField] Collider bossGateTrigger;
    [SerializeField] BossDoor bossDoor;
    [SerializeField] StoneSpell stoneSpell;

    [Header("Dialog Box")]
    [SerializeField] GameObject dialogCanvas;
    [SerializeField] DialogBox dialogBox;
    [SerializeField] KeyCode nextKey = KeyCode.Mouse0;

    [Header("Sequence")]
    [SerializeField] bool[] sequence;
    public float typingDuration = 2f;
    public float currentTime;
    public bool countingDownNext;


    [Header("Cameras")]
    [SerializeField] GameObject[] sceneCamera;
    float cameraTimer = 5f;
    float currentCameraTimer;

    [Header("Audio")]
    [SerializeField] AudioHandler audioHandler;

    void Awake()
    {
        campFire.enabled = false;
        LockCursor();
        FirstDialog();
        StartCoroutine(EnableSecondDialog(1));
    }
    // Update is called once per frame
    void Update()
    {
        typing();
        SecondDialog();
    }

    private void FirstDialog()
    {
        dialogBox.EnableDialogBox(true);
        dialogBox.SetDialogTextNum(0);
        EnablePlayerComponents(false);
    }

    private void SecondDialog()
    {
        if (sequence[0])
        {
            if (!countingDownNext)
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
            if (!countingDownNext) //enabling player controls after second dialog
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

        else if (sequence[2])
        {
            if (enemyDeathHandler.EnemyDeath == 2) // KAEL: These spiders are everywhere. I hope there aren't too many more of them.
            {
                campfireBlocker.SetActive(false);
                dialogBox.EnableDialogBox(true);
                EnablePlayerComponents(false);
                dialogBox.nextLine(2);
                sequence[2] = false;
                sequence[3] = true;
                countingDownNext = true;
            }
        }
        else if (sequence[3])
        {
            if (!countingDownNext) //KAEL: Oh look! a chest!
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    thirdPersonCamera.SetActive(false);
                    sceneCamera[0].SetActive(true);
                    dialogBox.nextLine(3);
                    sequence[3] = false;
                    sequence[4] = true;
                    countingDownNext = true;
                }
            }
        }
        else if (sequence[4])
        {
            if (!countingDownNext) //Enable Player Control
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    thirdPersonCamera.SetActive(true);
                    sceneCamera[0].SetActive(false);
                    sequence[4] = false;
                    sequence[5] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[5])
        {
            if (!countingDownNext) //Nice! A healing potion and a mana potion. These will come in handy.
            {
                if (chest.chestIsOpen)
                {
                    dialogBox.EnableDialogBox(true);
                    EnablePlayerComponents(false);
                    dialogBox.nextLine(4);
                    sequence[5] = false;
                    sequence[6] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[6])
        {
            if (!countingDownNext) //KAEL: But hold on...I can feel a sense of magic emanating from this campfire.
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(5);
                    sequence[6] = false;
                    sequence[7] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[7])
        {
            if (!countingDownNext) //KAEL: Hmm, a campfire with a magical aura. Maybe I can activate it somehow?
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(6);
                    sequence[7] = false;
                    sequence[8] = true;
                    countingDownNext = true;
                }
            }
        }


        else if (sequence[8])
        {
            if (!countingDownNext) //enabling player controls after second dialog
            {
                if (Input.GetKeyDown(nextKey))
                {
                    campFire.enabled = true;
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[8] = false;
                    sequence[9] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[9]) //KAEL: Ha! Got it. This campfire is now burning bright.
        {
            if (campFire.campFireLit)
            {
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(7);
                EnablePlayerComponents(false);
                sequence[9] = false;
                sequence[10] = true;
                countingDownNext = true;
            }
        }

        else if (sequence[10]) //KAEL: I've heard of magical campfires that can grant second chances.
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.nextLine(8);
                    sequence[10] = false;
                    sequence[11] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[11]) //KAEL: Could this be one of them? If I fall, I'll rise again here.
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.nextLine(9);
                    sequence[11] = false;
                    sequence[12] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[12])
        {
            if (!countingDownNext) //enabling player controls
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    Destroy(chestBlocker);
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[12] = false;
                    sequence[13] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[13])
        {
            if (bossGateTrigger.bounds.Intersects(playerTransform.gameObject.GetComponent<Collider>().bounds)) //KAEL: This gate is massive,. I wonder what's on the other side?
            {
                Destroy(bossGateTrigger.gameObject);
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(10);
                EnablePlayerComponents(false);
                sequence[13] = false;
                sequence[14] = true;
                countingDownNext = true;
            }
        }

        else if (sequence[14]) //KAEL: There's gotta be a way to destroy it.
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.nextLine(11);
                    sequence[14] = false;
                    sequence[15] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[15])
        {
            if (!countingDownNext) //enabling player controls
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[15] = false;
                    sequence[16] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[16]) //KAEL: It looks like a cementery. I gotta check it out!
        {
            if (bossDoor.destroyGate) 
            {
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(12);
                EnablePlayerComponents(false);
                sequence[16] = false;
                sequence[17] = true;
                countingDownNext = true;
            }
        }

        else if (sequence[17])
        {
            if (!countingDownNext) //enabling player controls
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[17] = false;
                    sequence[18] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[18]) //KAEL: Whew, that was close.
        {
            if(enemyDeathHandler.EnemyDeath == 6)
            {
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(13);
                EnablePlayerComponents(false);
                sequence[18] = false;
                sequence[19] = true;
                countingDownNext = true;
            }
        }


        else if (sequence[19])
        {
            if (!countingDownNext) //KAEL: Ah, there it is! The spell we've been looking for!
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.nextLine(14);
                    sequence[19] = false;
                    sequence[20] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[20])
        {
            if (!countingDownNext) //enabling player controls
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[20] = false;
                    sequence[21] = true;
                    countingDownNext = true;
                }
            }
        }
        else if (sequence[21])
        {
            if (stoneSpell.unlocked)//KAEL: Excellent, I found the first spell. Now I just need to find the remaining two.
            {
                EnablePlayerComponents(false);
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(15);
                sequence[21] = false;
                sequence[22] = true;
                countingDownNext = true;
            }
        }

        else if (sequence[22])
        {
            if (!countingDownNext) //KAEL: Let's head back and continue our search in the other locations!
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    dialogBox.nextLine(16);
                    sequence[22] = false;
                    sequence[23] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[23])
        {
            if (!countingDownNext) //enabling player controls
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    stoneSpell.headBackToRoom = true;
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[23] = false;
                    sequence[24] = true;
                    countingDownNext = true;
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
        playerAnim.enabled = value;
        pm.enabled = value;
        ps.enabled = value;
        config.enabled = value;
        pf.enabled = value;
        pc.enabled = value;

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

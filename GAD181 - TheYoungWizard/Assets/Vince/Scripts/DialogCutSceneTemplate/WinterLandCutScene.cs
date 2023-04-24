using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterLandCutScene : MonoBehaviour
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
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] Collider playerCollider;

    [Header("Environment Components")]
    [SerializeField] StoneSpell stoneSpell;
    [SerializeField] Collider[] dialogTrigger;
    [SerializeField] GameObject bug;

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
            if (!countingDownNext && !dialogBox.isTyping) //KAEL: I have a feeling danger is lurking in the shadows. I hope they don't have bears here...
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

        else if (sequence[2])
        {
            bug = GameObject.Find("Bug");
            if (bug == null)
            {
                if (dialogTrigger[0].bounds.Intersects(playerCollider.bounds))//KAEL: Looks like these rocks are obstructing the path.
                {
                    Destroy(dialogTrigger[0].gameObject);
                    EnablePlayerComponents(false);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(2);
                    sequence[2] = false;
                    sequence[3] = true;
                    countingDownNext = true;
                }
            }
        }

        if (sequence[3])
        {
            if (!countingDownNext && !dialogBox.isTyping) //KAEL: I wonder if I can push these rocks out of the way somehow.
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(3);
                    sequence[3] = false;
                    sequence[4] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[4])
        {
            if (!countingDownNext && !dialogBox.isTyping) //enabling player controls
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[4] = false;
                    sequence[5] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[5])
        {
            if (dialogTrigger[1].bounds.Intersects(playerCollider.bounds))//KAEL: It feels like this platform is moving.
            {
                Destroy(dialogTrigger[1].gameObject);
                EnablePlayerComponents(false);
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(4);
                sequence[5] = false;
                sequence[6] = true;
                countingDownNext = true;
            }
        }

        else if (sequence[6])
        {
            if (!countingDownNext && !dialogBox.isTyping) //enabling player controls
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[6] = false;
                    sequence[7] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[7])
        {
            if (stoneSpell.unlocked)//KAEL: Two down, one to go. The final island awaits!
            {
                EnablePlayerComponents(false);
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(5);
                sequence[7] = false;
                sequence[8] = true;
                countingDownNext = true;
            }
        }

        else if (sequence[8])
        {
            if (!countingDownNext) //enabling player controls
            {
                if (Input.GetKeyDown(nextKey) && !dialogBox.isTyping)
                {
                    stoneSpell.headBackToRoom = true;
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerControl();
                    sequence[8] = false;
                    sequence[9] = true;
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

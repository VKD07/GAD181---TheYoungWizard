using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScene_CutsceneManager : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] RoomScene_Player playerScript;
    [SerializeField] GameObject thirdPersonCamera;
    [SerializeField] Transform playerTransform;
    [SerializeField] MapScript mapScript;
    [SerializeField] Animator playerAnim;
    [SerializeField] GameObject furBall;
    [SerializeField] Transform portal;
    [SerializeField] MainMenu menu;

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
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] GameObject[] sceneCamera;
    [SerializeField] GameObject mainMenuCamera;
    float cameraTimer = 5f;
    float currentCameraTimer;

    [Header("Audio")]
    [SerializeField] AudioHandler audioHandler;

    [Header("VFX")]
    [SerializeField] GameObject windParticle;
    [SerializeField] Animator scrollAnimation;

    void Awake()
    {
        LockCursor();
        EnablePlayerComponents(false);
        mainMenuCamera.SetActive(false);
        sceneCamera[0].SetActive(true);
        StartCoroutine(FirstDialog(5f));
    }
    // Update is called once per frame
    void Update()
    {
        typing();
        KaelDialog();
    }

    IEnumerator FirstDialog(float time) //kael starts talking
    {
        yield return new WaitForSeconds(time);
        dialogBox.EnableDialogBox(true);
        dialogBox.SetDialogTextNum(0);
        sequence[0] = true;
        countingDownNext = true;
    }

    private void KaelDialog()
    {
        if (sequence[0]) // Line: Sigh
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
        else if (sequence[1]) // Line: I've been practicing for months, but people still make fun of me whenever I mess up.
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                    sceneCamera[0].SetActive(false);
                    sceneCamera[1].SetActive(true);
                    dialogBox.nextLine(2);
                    sequence[1] = false;
                    sequence[2] = true;
                    countingDownNext = true;
                }
            }
        }
        else if (sequence[2]) // Line: KAEL: My wizard exams are coming up soon.
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(3);
                    sequence[2] = false;
                    sequence[3] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[3]) // Line:KAEL: I need to train even harder if I want to prove to everyone that I can be the greatest wizard of them all!
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(4);
                    sequence[3] = false;
                    sequence[4] = true;
                    countingDownNext = true;
                }
            }
        }
        else if (sequence[4]) // Line: KAEL: Right Furball?
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(5);
                    sequence[4] = false;
                    sequence[5] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[5]) // Line: FURBALL: Meow!
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    sceneCamera[2].SetActive(true);
                    sceneCamera[1].SetActive(false);
                    dialogBox.nextLine(6);
                    sequence[5] = false;
                    sequence[6] = true;
                    countingDownNext = true;
                }
            }
        }
        else if (sequence[6]) // Line: Alright then, let's keep training.
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    sceneCamera[2].SetActive(false);
                    sceneCamera[0].SetActive(true);
                    dialogBox.nextLine(7);
                    sequence[6] = false;
                    sequence[7] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[7]) // Shows portal start shaking. Portal turns on.
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    StartCoroutine(ActivateRedPortal(3f));
                    dialogBox.EnableDialogBox(false);
                    sceneCamera[0].SetActive(false);
                    sceneCamera[3].SetActive(true);
                }
            }
        }

        else if (sequence[8]) // Notes are being sucked in
        {
            if (!countingDownNext)
            {
                furBall.SetActive(false);
                windParticle.SetActive(true);
                scrollAnimation.SetTrigger("TakeScrolls");
                sceneCamera[3].SetActive(false);
                sceneCamera[4].SetActive(true);
                countingDownNext = true;
                sequence[8] = false;
                sequence[9] = true;
            }
        }

        else if (sequence[9]) //Line: KAEL:  No! My spells! 
        {
            if (!countingDownNext)
            {
                playerAnim.SetBool("Suprised", true);
                playerTransform.position = new Vector3(-8.268f, -1.247f, -7.917f);
                playerTransform.LookAt(portal.position);
                sceneCamera[4].SetActive(false);
                sceneCamera[5].SetActive(true);
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(8);
                countingDownNext = true;
                sequence[9] = false;
                sequence[10] = true;
            }
        }

        else if (sequence[10]) // KAEL: I haven't even had the chance to memorize all of the spells yet!
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(9);
                    sequence[10] = false;
                    sequence[11] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[11]) // KAEL: How am I supposed to pass my exams without them?
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(10);
                    sequence[11] = false;
                    sequence[12] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[12]) // KAEL:  I have to find them before it's too late!
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(11);
                    sequence[12] = false;
                    sequence[13] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[13]) // KAEL: I don't even know why I have a portal in my room!
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    dialogBox.nextLine(12);
                    sequence[13] = false;
                    sequence[14] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[14]) // Give access to player control
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseIn;
                    dialogBox.EnableDialogBox(false);
                    sceneCamera[5].SetActive(false);
                    playerAnim.SetBool("Suprised", false);
                    menu.cameraControl = true;
                    mapScript.portalVfx.SetActive(false);
                    windParticle.SetActive(false);
                    EnablePlayerComponents(true);
                    sequence[14] = false;
                    sequence[15] = true;
                    countingDownNext = true;
                }
            }
        }

        //player interacts with the map
        // Kael: seems like my spells are scattered in this 3 places. I have to go there and find them!


    }

    void EnablePlayerComponents(bool value)
    {
        playerScript.enabled = value;
        thirdPersonCamera.SetActive(value);
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

    IEnumerator ActivateRedPortal(float time)
    {
        yield return new WaitForSeconds(time);
        mapScript.RedPortal();
        sequence[7] = false;
        sequence[8] = true;
        countingDownNext = true;
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

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomScene_CutsceneManager : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] RoomScene_Player playerScript;
    [SerializeField] GameObject thirdPersonCamera;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator playerAnim;
    bool playerControl;

    [Header("Environment Components")]
    [SerializeField] MapScript mapScript;
    [SerializeField] GameObject furBall;
    [SerializeField] Transform portal;
    [SerializeField] MainMenu menu;
    [SerializeField] GameObject portalParticle;
    [SerializeField] GameObject mapYellowMark;
    [SerializeField] GameObject mapUI;
    [SerializeField] GameObject scrolls;
    [SerializeField] GameObject windVfx;
    [SerializeField] MainMenu mainMenuScript;
    [SerializeField] GameObject exclamationMark;
    bool portalDisabled;

    [Header("Dialog Box")]
    [SerializeField] GameObject dialogCanvas;
    [SerializeField] DialogBox dialogBox;
    [SerializeField] KeyCode nextKey = KeyCode.Mouse0;

    [Header("Objective Box")]
    [SerializeField] ObjectiveBox objBox;

    [Header("Sequence")]
    [SerializeField] bool[] sequence;
    public float typingDuration = 2f;
    public float currentTime;
    public bool countingDownNext;

    [Header("Skip settings")]
    [SerializeField] KeyCode skipKey = KeyCode.F;
    [SerializeField] GameObject skipUI;
    [SerializeField] float skipDuration = 5f;
    CanvasGroup skipSliderCanvas;
    Slider skipSlider;
    public bool skipNarrative;
    bool playerPositioned;
    bool disableSkip;


    [Header("Cameras")]
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] GameObject[] sceneCamera;
    [SerializeField] GameObject mainMenuCamera;
    float cameraTimer = 5f;
    float currentCameraTimer;

    [Header("Audio")]
    [SerializeField] RoomCutScene_AudioHandler audioHandler;

    [Header("VFX")]
    [SerializeField] GameObject windParticle;
    [SerializeField] Animator scrollAnimation;

    void Awake()
    {
        skipSlider = skipUI.GetComponent<Slider>();
        skipSliderCanvas = skipUI.GetComponent<CanvasGroup>();
        StartCoroutine(EnableSkipNarrative(5));
        LockCursor(true);
        mapYellowMark.SetActive(false);
        EnablePlayerComponents(false);
        mainMenuCamera.SetActive(false);
        audioHandler.PlayWhooshSFX(2);
        sceneCamera[0].SetActive(true);
        StartCoroutine(FirstDialog(5f));
    }
    // Update is called once per frame
    void Update()
    {
        typing();
        KaelDialog();
        SkipNarrativeSlider();
    }

    IEnumerator FirstDialog(float time) //kael starts talking
    {
        playerTransform.position = new Vector3(-9.279f, -1.247f, -7.826f);
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
                    audioHandler.PlayCatSound();
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
                    CameraShake.instance.ShakeVirtualCamera(2, 1);
                    audioHandler.PlayPortalSceneMusic();
                    StartCoroutine(ActivateRedPortal(2f));
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
                CameraShake.instance.ShakeVirtualCamera(2, 1);
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

        else if (sequence[14])
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    disableSkip = true;
                    mapYellowMark.SetActive(true);
                    dialogBox.EnableDialogBox(false);
                    cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseIn;
                    cinemachineBrain.m_DefaultBlend.m_Time = 2f;
                    sceneCamera[5].SetActive(false);
                    sceneCamera[6].SetActive(true);
                    playerAnim.SetBool("Suprised", false);
                    mapScript.portalVfx.SetActive(false);
                    windParticle.SetActive(false);
                    sequence[14] = false;
                    sequence[15] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[15]) // goes to the back of kael. dialog: Kael: That map over there can help me access the portal!
        {
            if (!countingDownNext)
            {
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(13);
                sequence[15] = false;
                sequence[16] = true;
                countingDownNext = true;
            }
        }

        else if (sequence[16]) // Give back player controls 
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    objBox.EnableObjectiveBox(true);
                    objBox.SetObjectiveTextNum(0, "");
                    skipUI.SetActive(false);
                    playerTransform.position = new Vector3(-8.268f, -1.247f, -7.917f);
                    audioHandler.PlayWhooshSFX(0);
                    dialogBox.EnableDialogBox(false);
                    EnablePlayerComponents(true);
                    sceneCamera[6].SetActive(false);
                    menu.cameraControl = true;
                    sequence[16] = false;
                    sequence[17] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[17]) // KAEL: Looks like my spells have been scattered across these three locations. 
        {
            if (!countingDownNext)
            {
                if (mapUI.activeSelf)
                {
                    objBox.ObjectiveCompleted(true);
                    exclamationMark.SetActive(false);
                    mapScript.disableMapClosing = true;
                    LockCursor(true);
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(14);
                    sequence[17] = false;
                    sequence[18] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[18])
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey)) //KAEL: I'll need to go there and collect them all back!  
                {
                    objBox.EnableObjectiveBox(false);
                    dialogBox.nextLine(15);
                    sequence[18] = false;
                    sequence[19] = true;
                    countingDownNext = true;
                }
            }
        }

        else if (sequence[19]) // give back player controls
        {
            if (!countingDownNext)
            {
                if (Input.GetKeyDown(nextKey))
                {
                    objBox.EnableObjectiveBox(true);
                    objBox.SetObjectiveTextNum(1, "");
                    mapScript.disableMapClosing = false;
                    LockCursor(false);
                    dialogBox.EnableDialogBox(false);
                    sequence[19] = false;
                    sequence[20] = true;
                    countingDownNext = true;
                }
            }
        }

        Skipped();
    }

    private void Skipped()
    {
        //if the narrative is skipped
        if (skipNarrative)
        {
            if (!objBox.objectiveSequence[0])
            {
                objBox.EnableObjectiveBox(true);
                objBox.SetObjectiveTextNum(0, "");
                dialogBox.EnableDialogBox(false);

                if (mapUI.activeSelf)
                {
                    dialogBox.EnableDialogBox(true);
                    dialogBox.nextLine(14);
                    objBox.ObjectiveCompleted(true);
                    exclamationMark.SetActive(false);
                    mapScript.disableMapClosing = true;
                    LockCursor(true);
                    objBox.objectiveSequence[0] = true;
                    objBox.objectiveSequence[1] = true;
                    countingDownNext = true;
                }
            }

            else if (objBox.objectiveSequence[1])
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey)) //KAEL: I'll need to go there and collect them all back!  
                    {
                        objBox.EnableObjectiveBox(false);
                        dialogBox.nextLine(15);
                        objBox.objectiveSequence[1] = false;
                        objBox.objectiveSequence[2] = true;
                        countingDownNext = true;
                    }
                }
            }

            else if (objBox.objectiveSequence[2]) // give back player controls
            {
                if (!countingDownNext)
                {
                    if (Input.GetKeyDown(nextKey))
                    {
                        objBox.EnableObjectiveBox(true);
                        objBox.SetObjectiveTextNum(1, "");
                        mapScript.disableMapClosing = false;
                        LockCursor(false);
                        dialogBox.EnableDialogBox(false);
                        objBox.objectiveSequence[2] = false;
                        objBox.objectiveSequence[3] = true;
                        countingDownNext = true;
                    }
                }
            }
        }
    }

    void EnablePlayerComponents(bool value)
    {
        playerScript.enabled = value;
        if (mapUI.activeSelf)
        {
            thirdPersonCamera.SetActive(!value);
        }
        else
        {
            thirdPersonCamera.SetActive(value);
        }
    }

    void LockCursor(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        Cursor.visible = !value;
    }

    void EnablePlayerControl()
    {
        EnablePlayerComponents(true);
        dialogBox.EnableDialogBox(false);
    }

    IEnumerator ActivateRedPortal(float time)
    {
        yield return new WaitForSeconds(time);
        audioHandler.PlayRedPortal();
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

    void SkipNarrativeSlider()
    {
        StartCoroutine(EnableSkipNarrative(5));

        if (Input.GetKey(skipKey) && skipSliderCanvas.alpha >= 1 && !disableSkip)
        {
            if (skipSlider.value < skipDuration)
            {
                audioHandler.PlaySkipSlider();
                skipSlider.value += Time.deltaTime * 3f;
            }
            else
            {
                audioHandler.skipped = true;
                audioHandler.SkipSFX();
                skipNarrative = true;
                skipUI.SetActive(false);
            }
        }
        else if (Input.GetKeyUp((skipKey)))
        {
            audioHandler.skipSliderPlaying = false;
            skipSlider.value = 0;
        }

        if (skipNarrative)
        {
            //cinemachine
            cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseIn;
            cinemachineBrain.m_DefaultBlend.m_Time = 1f;
            mainMenuScript.cameraControl = true;
            //player control
            EnablePlayerComponents(true);
            playerAnim.SetBool("Suprised", false);
            //environment disable
            furBall.SetActive(false);
            mapYellowMark.SetActive(true);
            scrolls.SetActive(false);
            if (!portalDisabled)
            {
                portalDisabled = true;
                portalParticle.SetActive(false);
            }
            windVfx.SetActive(false);
            //audio
            audioHandler.PlayPortalSceneMusic();
            audioHandler.skipped = true;

            if (!playerPositioned)
            {
                playerPositioned = true;
                playerTransform.position = new Vector3(-6.73f, -1.247f, -7.89f);
                playerTransform.rotation = Quaternion.Euler(0f, -626.796f, 0f);
            }

            for (int i = 0; i < sequence.Length; i++)
            {
                sequence[i] = false;
            }

            for (int i = 0; i < sceneCamera.Length; i++)
            {
                sceneCamera[i].SetActive(false);
            }
        }
    }

    IEnumerator EnableSkipNarrative(float time)
    {
        yield return new WaitForSeconds(time);

        if (skipSliderCanvas.alpha < 1)
        {
            skipSliderCanvas.alpha += Time.deltaTime;
        }
        else
        {
            skipSliderCanvas.alpha = 1f;
        }
    }
}

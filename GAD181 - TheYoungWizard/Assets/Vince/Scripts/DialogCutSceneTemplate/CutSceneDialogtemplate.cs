using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneDialogtemplate : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] RoomScene_Player playerScript;
    [SerializeField] GameObject thirdPersonCamera;

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
        EnablePlayerComponents(false);
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

    }




    IEnumerator EnableSecondDialog(float time)
    {
        yield return new WaitForSeconds(time);
        sequence[0] = true;
        countingDownNext = true;
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

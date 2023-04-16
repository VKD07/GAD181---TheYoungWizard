using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FInalBossTimeLine : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] GameObject[] cameras;
    [SerializeField] bool[] sequence;

    [Header("Director")]
    [SerializeField] GameObject director;
    [SerializeField] PlayableAsset[] scene;
    PlayableDirector sceneDirector;

    [Header("Challenges script")]
    [SerializeField] FirstChallenge firstChallenge;
    [SerializeField] EndingScene endingScene;

    [Header("Dialog")]
    [SerializeField] GameObject dialogBoxObj;
    DialogBox dialogBox;

    [Header("Character Components")]
    [SerializeField] BossScript bossScript;
    [SerializeField] Animator bossAnim;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator playerAnim;
    [SerializeField] Transform bossPos;
    [SerializeField] GameObject playerCanvas;
    [SerializeField] BossHealthHandler bossHealth;

    [SerializeField] playerCombat pc;
    [SerializeField] Player_Movement pm;
    [SerializeField] PlayerForceField playerForceField;

    [Header("VFX")]
    [SerializeField] GameObject playerCharge;
    [SerializeField] GameObject playerBeam;
    [SerializeField] GameObject bossBeam;
    [SerializeField] Animator catAnim;
    [SerializeField] GameObject flashBangUI;
    [SerializeField] float flashRate = 200f;
    Image flashBangImg;
    float imgAlpha = 1f;

    [Header("Sound")]
    [SerializeField] TimelineAudioHandler audioSource;
    [SerializeField] AudioSource beamBlockerAudioSource;


    //timers
    float pounceDuration = 2f;
    float currentPounceTime;
    float delayDuration = 1.5f;
    public float currentDelayTime;
    public float typingDuration = 2f;
    public float currentTime;
    public bool countingDownNext;
    void Start()
    {
        LockMouse();

        //character scripts
        pc.enabled = false;
        pm.enabled = false;
        playerForceField.enabled = false;

        dialogBox = GetComponent<DialogBox>();
        playerCanvas.SetActive(false);
        flashBangImg = flashBangUI.GetComponent<Image>();
        sceneDirector = director.GetComponent<PlayableDirector>();
        StartCoroutine(DisableCamera(0, 2f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        typing();
        if (sequence[0])
        {
            sequence[0] = false;
            StartCoroutine(DisableCamera(1, 3f, 1));

        }
        else if (sequence[1]) //----------- ice shard challenge
        {
            pc.enabled = true;
            playerCanvas.SetActive(true);
            director.SetActive(false);
            cameras[2].SetActive(true);
            Time.timeScale = 0.05f;
            firstChallenge.startChallenge = true;
            if (firstChallenge.challengeDone)
            {
                Time.timeScale = 1f;
                pc.enabled = false;
                playerCanvas.SetActive(false);
                sequence[1] = false;
                StartCoroutine(DisableCamera(2, 2f, 2));
            }
        }
        else if (sequence[2])
        {
            bossScript.enabled = true;
            StartCoroutine(DisableBossScript(5f)); //-------------- Rolling challenge

            if (currentPounceTime < pounceDuration)
            {
                currentPounceTime += Time.deltaTime;
            }
            else
            {
                firstChallenge.startChallenge2 = true;
                currentPounceTime = 0;
                cameras[4].SetActive(true);
                Time.timeScale = 0.4f;
            }

            if (firstChallenge.challenge2Done)
            {
                cameras[5].SetActive(true);
                cameras[4].SetActive(false);
                sequence[2] = false;
                sequence[3] = true;
                bossScript.setStompSpeedAndAttackNumber(8, 0);
            }

        }
        else if (sequence[3])
        {
            if (currentDelayTime < delayDuration)
            {
                currentDelayTime += Time.deltaTime;
            }
            else
            {
                pm.enabled = false;
                cameras[5].SetActive(false);
                cameras[6].SetActive(true);
                currentDelayTime = 0;
                sequence[3] = false;
                sequence[4] = true;
            }
        }

        if (sequence[4]) //--After jumpung
        {
            bossAnim.SetTrigger("Fall2");
            // bossPos.position = new Vector3(-27.7f, -5.289988f, -43.77f);
            if (currentDelayTime < 2)
            {
                currentDelayTime += Time.deltaTime;
            }
            else //-- cat starts charging power
            {
                audioSource.PlayCatCharge();
                currentDelayTime = 0;
                bossAnim.SetBool("FinalCharge", true);
                playerTransform.position = new Vector3(-28.25f, -4.479403f, -54.2f);
                playerTransform.rotation = Quaternion.Euler(0f, 348.836f, 0f);//make the character face the player
                sequence[4] = false;
                sequence[5] = true;
            }
        }

        else if (sequence[5]) //-- player starts charging power
        {
            if (currentDelayTime < 2)
            {
                currentDelayTime += Time.deltaTime;
            }
            else
            {
                audioSource.PlayCharacterCharge();
                audioSource.PlayChargeSound();
                pc.enabled = true;
                playerCanvas.SetActive(true);
                playerCharge.SetActive(true);
                playerAnim.SetBool("PowerCharge", true);
                cameras[6].SetActive(false);
                cameras[7].SetActive(true);
                currentDelayTime = 0;
                firstChallenge.startChallenge3 = true;
            }

            if (firstChallenge.challenge3Done)
            {
                pc.enabled = false;
                playerCanvas.SetActive(false);
                sequence[5] = false;
                sequence[6] = true;
            }
        }
        else if (sequence[6])
        {
            if (currentDelayTime < 4)
            {
                currentDelayTime += Time.deltaTime;
            }
            else
            {
                audioSource.PlayBeamCast();
                cameras[7].SetActive(false);
                cameras[8].SetActive(true);
                bossPos.position = new Vector3(-27.7f, -5.289988f, -39f);
                sequence[6] = false;
                sequence[7] = true;
                currentDelayTime = 0f;
            }
        }

        else if (sequence[7])
        {
            if (currentDelayTime < 1) //---------- cat and player beam collides
            {
                currentDelayTime += Time.deltaTime;
            }
            else if (!firstChallenge.challenge3Failed)
            {
                Time.timeScale = 0.2f;
                currentDelayTime = 0;
                StartCoroutine(PlayBeamExplosionSound(0.3f));
                catAnim.SetBool("ReleaseCharge", true);
                catAnim.SetBool("FinalCharge", false);
                playerAnim.SetBool("PowerCharge", false);
                playerAnim.SetBool("FinalBeam", true);
                sequence[7] = false;
                sequence[8] = true;
            }
            else
            {
                //Reset scene -------------------------------------
                firstChallenge.DisableBeamBox();
                Time.timeScale = 0.2f;
                currentDelayTime = 0;
                catAnim.SetBool("ReleaseCharge", true);
                catAnim.SetBool("FinalCharge", false);
                StartCoroutine(ResetScene(0.3f));
            }
        }
        else if (sequence[8]) //--------------- tap challenge
        {
            if (currentDelayTime < 1)
            {
                audioSource.playBeamSound = true;
                currentDelayTime += Time.deltaTime;
            }
            else
            {
                audioSource.PlayBeamCollidedCharge();
                firstChallenge.startFinalChallenge = true;
                Time.timeScale = 1f;
                currentDelayTime = 0;
            }

            if (firstChallenge.finalChallengeDone)
            {
                sequence[8] = false;
                sequence[9] = true;
            }
        }
        else if (sequence[9]) //--------------- FlashBang Effect
        {
            if (currentDelayTime < 7)
            {
                audioSource.startFading = true;
                audioSource.PlayBeamExplosion();
                audioSource.fadingRate = 0.05f;
                bossAnim.SetTrigger("Dead");
                currentDelayTime += Time.deltaTime;
                flashBangUI.SetActive(true);
                flashBangUI.transform.localScale += Vector3.one * flashRate * Time.deltaTime;
                flashBangUI.transform.localScale = Vector3.Min(flashBangUI.transform.localScale, new Vector3(500f, 500f, 500f));
            }
            else
            {
                playerBeam.SetActive(false);
                playerAnim.SetBool("FinalBeam", false);
                bossAnim.SetBool("ReleaseCharge", false);
                playerCharge.SetActive(false);
                bossBeam.SetActive(false);
                currentDelayTime = 0;
                sequence[9] = false;
                sequence[10] = true;
            }
        }
        else if (sequence[10])
        {
            if (currentDelayTime < 5)
            {
                audioSource.startFading = false;
                audioSource.PlayEndingMusic();
                currentDelayTime += Time.deltaTime;
                imgAlpha -= 0.5f * Time.deltaTime;
                flashBangImg.color = new Color(flashBangImg.color.r, flashBangImg.color.g, flashBangImg.color.b, imgAlpha);
                bossHealth.ShatteredRock(4);
                cameras[8].SetActive(false);
                cameras[9].SetActive(true);
            }
            else
            {
                sequence[10] = false;
                sequence[11] = true;
                currentDelayTime = 0;
            }
        }

        else if (sequence[11])
        {
            if (currentDelayTime < 2)
            {
                currentDelayTime += Time.deltaTime;
            }
            else
            {
                currentDelayTime = 0;
                cameras[9].SetActive(false);
                cameras[10].SetActive(true);
                sequence[11] = false;
                sequence[12] = true;
            }
        }

        else if (sequence[12])
        {
            if (currentDelayTime < 5)
            {
                currentDelayTime += Time.deltaTime;
            }
            else
            {
                currentDelayTime = 0;
                sequence[12] = false;
                sequence[13] = true;
            }
        }

        else if (sequence[13])
        {
            if (currentDelayTime < 4)
            {
                currentDelayTime += Time.deltaTime;
                playerTransform.position = new Vector3(-26.4f, -4.49f, -42.32f);
                cameras[10].SetActive(false);
                cameras[11].SetActive(true);
            }
            else
            {
                currentDelayTime = 0;
                sequence[13] = false;
                sequence[14] = true;
            }
        }
        else if (sequence[14])
        {
            cameras[11].SetActive(false);
            cameras[12].SetActive(true);
            if (!countingDownNext)
            {
                countingDownNext = true;
                dialogBox.EnableDialogBox(true);
                dialogBox.nextLine(0);
                sequence[14] = false;
                sequence[15] = true;
            }
        }

        else if (sequence[15])
        {
            if (!countingDownNext)
            {
                countingDownNext = true;
                dialogBox.nextLine(1);
                sequence[15] = false;
                sequence[16] = true;
            }
        }

        else if (sequence[16])
        {
            if (!countingDownNext)
            {
                countingDownNext = true;
                dialogBox.nextLine(2);
                sequence[16] = false;
                sequence[17] = true;
            }
        }

        else if (sequence[17])
        {
            if (!countingDownNext)
            {
                dialogBox.EnableDialogBox(false);
                sequence[17] = false;
                sequence[18] = true;
                countingDownNext = true;
            }
        }

        else if (sequence[18])
        {
            cameras[12].SetActive(false);
            cameras[13].SetActive(true);
            endingScene.startEndScene = true;
        }
    }

    private void LockMouse()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator DisableCamera(int cameraNum, float time, int sequenceNum)
    {
        yield return new WaitForSeconds(time);
        cameras[cameraNum].SetActive(false);
        cameras[cameraNum + 1].SetActive(true);
        sequence[sequenceNum] = true;

    }

    IEnumerator DisableBossScript(float time)
    {
        yield return new WaitForSeconds(time);
        bossScript.enabled = false;
        Time.timeScale = 1f;
    }

    IEnumerator ResetScene(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
        currentDelayTime += Time.deltaTime;
        flashBangUI.SetActive(true);
        flashBangUI.transform.localScale = new Vector3(500f, 500f, 500f);
        SceneManager.LoadScene(4);
    }

    IEnumerator PlayBeamExplosionSound(float time)
    {
        yield return new WaitForSeconds(time);
        beamBlockerAudioSource.Play();
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

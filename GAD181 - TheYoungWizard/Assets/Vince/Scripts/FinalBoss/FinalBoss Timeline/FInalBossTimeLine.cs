using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

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

    [Header("Character Components")]
    [SerializeField] BossScript bossScript;
    [SerializeField] Animator bossAnim;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator playerAnim;
    [SerializeField] Transform bossPos;

    [Header("VFX")]
    [SerializeField] GameObject playerCharge;
    [SerializeField] Animator catAnim;


    //timers
    float pounceDuration = 2f;
    float currentPounceTime;
    float delayDuration = 1.5f;
    float currentDelayTime;
    void Start()
    {
        sceneDirector = director.GetComponent<PlayableDirector>();
        StartCoroutine(DisableCamera(0, 2f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (sequence[0])
        {
            sequence[0] = false;
            StartCoroutine(DisableCamera(1, 3f, 1));

        }
        else if (sequence[1])
        {
            director.SetActive(false);
            cameras[2].SetActive(true);
            Time.timeScale = 0.1f;
            firstChallenge.startChallenge = true;
            if (firstChallenge.challengeDone)
            {
                sequence[1] = false;
                Time.timeScale = 1f;
                StartCoroutine(DisableCamera(2, 2f, 2));
            }
        }
        else if (sequence[2])
        {
            bossScript.enabled = true;
            StartCoroutine(DisableBossScript(4.5f));

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
                cameras[5].SetActive(false);
                cameras[6].SetActive(true);
                currentDelayTime = 0;
                sequence[3] = false;
                sequence[4] = true;
            }
        }

        if (sequence[4]) //--After jumpung
        {
            if (currentDelayTime < 2)
            {
                currentDelayTime += Time.deltaTime;
            }
            else //-- cat starts charging power
            {
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
                cameras[6].SetActive(false);
                cameras[7].SetActive(true);
                sequence[5] = false;
                sequence[6] = true;
                currentDelayTime = 0;
                playerAnim.SetBool("PowerCharge", true);
                playerCharge.SetActive(true);
            }
        }
        else if (sequence[6])
        {
            if(currentDelayTime < 6)
            {
                currentDelayTime += Time.deltaTime;
            }
            else
            {
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
            if(currentDelayTime < 1) //---------- cat and player beam collides
            {
                currentDelayTime += Time.deltaTime;
            }
            else
            {
                Time.timeScale = 0.2f;
                currentDelayTime = 0;
                catAnim.SetBool("ReleaseCharge", true);
                catAnim.SetBool("FinalCharge", false);
                playerAnim.SetBool("PowerCharge", false);
                playerAnim.SetBool("FinalBeam", true);
                sequence[7] = false;
            }
        }
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
}

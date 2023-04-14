using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstChallenge : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] float pounceSceneTimerRate;
    [SerializeField] float spellsChallengeTimerRate;
    [SerializeField] GameObject sceneTimer;
    [SerializeField] Slider timerSlider;
    [SerializeField] float timerMaxDuration = 10f;
    [SerializeField] GameObject deathScreen;
    RectTransform sceneTimerPos;
    [SerializeField] GameObject keyboardUI;
    [SerializeField] Animator beamUIAnim;

    [Header("Challenges")]
    [Header("Challenge 1")]
    [SerializeField] float firstChallengeTimeRate = 20f;
    public bool startChallenge;
    public bool challengeDone;
    bool iceShard;

    [Header("Challenge 2")]
    public bool startChallenge2;
    public bool challenge2Done;
    [SerializeField] float secondChallengeTimeRate = 10f;
    [Header("Challenge 3")]
    public bool startChallenge3;
    public bool challenge3Done;
    public bool challenge3Failed;
    [SerializeField] int[] spellNumbers;
    [SerializeField] int numberOfSpellsToComplete = 4;
    [SerializeField] int currentSpellsDone;
    [SerializeField] int spellChosen;
    [SerializeField] Image spellImage;
    public int randomSpell;
    bool spellIsChosen;
    int previousSpell;

    [Header("Final Challenge")]
    public bool startFinalChallenge;
    public bool finalChallengeDone;
    [SerializeField] GameObject beamSliderUI;
    [SerializeField] float bossPower;
    [SerializeField] float playerPower;
    [SerializeField] KeyCode beamChallengeBtn = KeyCode.U;
    [SerializeField] Transform beamBlocker;
    [SerializeField] float bossBeamPowerRate;
    [SerializeField] float playerBeamPowerRate;
    Slider beamSlider;
    float shakeMaxTime;
    float currentTimeShake;
    bool shakeTheUI;

    [Header("Character Components")]
    public CastModeManager spellCast;
    [SerializeField] BossScript bossScript;
    [SerializeField] Player_Movement playerMovement;
    [SerializeField] Animator playerAnim;
    [SerializeField] playerCombat pc;

    private void Start()
    {
        sceneTimerPos = sceneTimer.GetComponent<RectTransform>();
        beamSlider = beamSliderUI.GetComponent<Slider>();
        beamSlider.value = 60f;
        timerSlider.maxValue = timerMaxDuration;
        timerSlider.value = timerMaxDuration;
    }


    // Update is called once per frame
    void Update()
    {
        Challenge1();
        Challenge2();
        Challenge3();
        FinalChallenge();
        shakeBeamUI();
    }

    private void Challenge1()
    {
        if (startChallenge)
        {
            setTimerPosition(0, -197);
            //timer
            if (timerSlider.value > 0)
            {
                sceneTimer.SetActive(true);
                spellImage.sprite = spellCast.spellIcons[0];
                setSpellIconAlpha(1);
                timerSlider.value -= firstChallengeTimeRate * Time.deltaTime;
            }
            else if (timerSlider.value <= 0)
            {
                startChallenge = false;
                timerSlider.value = timerMaxDuration;
                sceneTimer.SetActive(false);
                challengeDone = true;
            }

            if (spellCast.availableSpellID == 30 && startChallenge)
            {
                startChallenge = false;
                if (!iceShard)
                {
                    playerAnim.SetTrigger("IceSpell");
                    iceShard = true;
                }
                timerSlider.value = timerMaxDuration;
                sceneTimer.SetActive(false);
                challengeDone = true;
            }//failed
            else if (timerSlider.value <= 0)
            {
                startChallenge = false;
                SceneManager.LoadScene(4);
            }

            if (challengeDone)
            {
                startChallenge = false;
                sceneTimer.SetActive(false);
            }
        }
    }

    void Challenge2()
    {
        if (startChallenge2)
        {
            setSpellIconAlpha(0);
            keyboardUI.SetActive(true);
            setTimerPosition(40, -291);
            playerMovement.enabled = true;
            playerMovement.stopMoving = true;

            //timer
            if (timerSlider.value > 0)
            {
                sceneTimer.SetActive(true);
                timerSlider.value -= secondChallengeTimeRate * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                setSpellIconAlpha(1);
                keyboardUI.SetActive(false);
                timerSlider.value = timerMaxDuration;
                sceneTimer.SetActive(false);
                startChallenge2 = false;
                Time.timeScale = 1f;
                challenge2Done = true;
            }
            else if (timerSlider.value <= 0)//challenge 2 failed
            {
                setSpellIconAlpha(1);
                keyboardUI.SetActive(false);
                startChallenge2 = false;
                timerSlider.value = timerMaxDuration;
                sceneTimer.SetActive(false);
                challenge2Done = true;
                SceneManager.LoadScene(4);
            }


        }
    }

    void Challenge3()
    {
        if (startChallenge3)
        {
            setTimerPosition(37, -355);
            pc.castingSpell = true;
            playerMovement.enabled = false;
            if (timerSlider.value > 0)
            {
                timerSlider.value -= spellsChallengeTimerRate * Time.deltaTime;
                sceneTimer.SetActive(true);
            }
            else if (timerSlider.value <= 0)
            {
                challenge3Failed = true;
                //--reset scene
                sceneTimer.SetActive(false);
                challenge3Done = true;
            }
            if (!challenge3Done)
            {
                if (!spellIsChosen)
                {
                    do
                    {
                        randomSpell = Random.Range(0, spellNumbers.Length);
                    } while (previousSpell == randomSpell);
                    spellChosen = spellNumbers[randomSpell];
                    previousSpell = randomSpell;
                    spellImage.sprite = spellCast.spellIcons[randomSpell];
                    spellIsChosen = true;
                    spellCast.availableSpellID = 0;
                }

                if (spellChosen == spellCast.availableSpellID)
                {
                    if (Input.GetKey(KeyCode.R))
                    {
                        spellIsChosen = false;
                        currentSpellsDone++;
                    }
                }
            }

            if (currentSpellsDone >= numberOfSpellsToComplete)
            {
                challenge3Done = true;

            }

            if (challenge3Done)
            {
                sceneTimer.SetActive(false);
            }
        }
    }

    void FinalChallenge()
    {
        if (startFinalChallenge)
        {
            beamSliderUI.SetActive(true);
            beamSlider.value += bossPower;


            beamBlocker.position -= beamBlocker.transform.forward * bossBeamPowerRate;
            beamBlocker.position = new Vector3(
            beamBlocker.position.x,
            beamBlocker.position.y,
            Mathf.Clamp(beamBlocker.position.z, -52f, -43f)
            );

            if (Input.GetKeyDown(beamChallengeBtn))
            {
                shakeTheUI = true;
                beamSlider.value -= playerPower;
                beamBlocker.position += beamBlocker.transform.forward * playerBeamPowerRate;
                beamBlocker.position = new Vector3(
                beamBlocker.position.x,
                beamBlocker.position.y,
                Mathf.Clamp(beamBlocker.position.z, -52f, -43f)
                );
            }
        }
        //win situation
        if (beamSlider.value <= 25)
        {
            finalChallengeDone = true;
            startFinalChallenge = false;
            beamSliderUI.SetActive(false);
        }//Lose situation
        else if (beamSlider.value <= 77.7)
        {

        }
    }

    void setSpellIconAlpha(int alpha)
    {
        spellImage.color = new Color(spellImage.color.r, spellImage.color.g, spellImage.color.b, alpha);
    }

    void setTimerPosition(float x, float y)
    {
        sceneTimerPos.localPosition = new Vector3(x, y, 0);
    }

    public void DisableBeamBox()
    {
        beamBlocker.gameObject.SetActive(false);
    }

    void shakeBeamUI()
    {
        if (shakeTheUI)
        {
            if (currentTimeShake < 0.1)
            {
                beamUIAnim.SetBool("Shake", true);
                currentTimeShake += Time.deltaTime;
            }
            else
            {
                currentTimeShake = 0;
                shakeTheUI = false;
            }
        }
        else
        {
            beamUIAnim.SetBool("Shake", false);
        }
    }

}

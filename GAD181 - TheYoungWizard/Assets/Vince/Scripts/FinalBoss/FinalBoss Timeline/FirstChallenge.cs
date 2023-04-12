using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

    [Header("Character Components")]
    public CastModeManager spellCast;
    [SerializeField] BossScript bossScript;
    [SerializeField] Player_Movement playerMovement;
    [SerializeField] Animator playerAnim;
    [SerializeField] playerCombat pc;

    private void Start()
    {
        beamSlider = beamSliderUI.GetComponent<Slider>();
        beamSlider.value = 50;
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

    }

    private void Challenge1()
    {
        if (startChallenge)
        {
            //timer
            if (timerSlider.value > 0)
            {
                sceneTimer.SetActive(true);
                spellImage.sprite = spellCast.spellIcons[0];
                spellImage.color = new Color(spellImage.color.r, spellImage.color.g, spellImage.color.b, 1f);
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
                timerSlider.value = timerMaxDuration;
                sceneTimer.SetActive(false);
                startChallenge2 = false;
                Time.timeScale = 1f;
                challenge2Done = true;
            }
            else if (timerSlider.value <= 0)//challenge 2 failed
            {
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
            beamSlider.value += bossPower * Time.deltaTime;


            beamBlocker.position += -beamBlocker.transform.forward * bossBeamPowerRate;
            beamBlocker.position = new Vector3(
            beamBlocker.position.x,
            beamBlocker.position.y,
            Mathf.Clamp(beamBlocker.position.z, -52f, -43f)
            );

            if (Input.GetKeyDown(beamChallengeBtn))
            {
                beamSlider.value -= playerPower * Time.deltaTime;
                beamBlocker.position += beamBlocker.transform.forward * playerBeamPowerRate;
                beamBlocker.position = new Vector3(
                beamBlocker.position.x,
                beamBlocker.position.y,
                Mathf.Clamp(beamBlocker.position.z, -52f, -43f)
                );
            }
        }
        //win situation
        if (beamSlider.value <= 0)
        {
            finalChallengeDone = true;
            startFinalChallenge = false;
            beamSliderUI.SetActive(false);
        }//Lose situation
        else
        {

        }
    }

    public void DisableBeamBox()
    {
        beamBlocker.gameObject.SetActive(false);
    }

}

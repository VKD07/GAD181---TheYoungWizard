using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

public class FirstChallenge : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] float pounceSceneTimerRate;
    [SerializeField] float spellsChallengeTimerRate;
    [SerializeField] GameObject sceneTimer;
    [SerializeField] Slider timerSlider;
    [SerializeField] float timerMaxDuration = 10f;

    [Header("Challenges")]
    [Header("Challenge 1")]
    public bool startChallenge;
    public bool challengeDone;
    [Header("Challenge 2")]
    public bool startChallenge2;
    public bool challenge2Done;
    [Header("Challenge 3")]
    public bool startChallenge3;
    public bool challenge3Done;
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
    int bossBeamPos;
    int playerBeamPos;
    Slider beamSlider;

    [Header("Character Components")]
    public CastModeManager spellCast;
    [SerializeField] BossScript bossScript;
    [SerializeField] Player_Movement playerMovement;
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
                timerSlider.value -= pounceSceneTimerRate * Time.deltaTime;
            }
            else if (timerSlider.value <= 0)
            {
                startChallenge = false;
                timerSlider.value = timerMaxDuration;
                sceneTimer.SetActive(false);
                challengeDone = true;
            }

            if (spellCast.availableSpellID == 30 && Input.GetKeyDown(KeyCode.E))
            {
                timerSlider.value = timerMaxDuration;
                sceneTimer.SetActive(false);
                challengeDone = true;
            }

            if (challengeDone)
            {
                sceneTimer.SetActive(false);
            }
        }
    }

    void Challenge2()
    {
        if (startChallenge2)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                challenge2Done = true;
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

}

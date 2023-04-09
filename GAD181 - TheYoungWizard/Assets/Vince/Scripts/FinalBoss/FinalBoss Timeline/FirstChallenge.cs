using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

public class FirstChallenge : MonoBehaviour
{
    //challenge 1
    public bool startChallenge;
    public bool challengeDone;
    //challenge2
    public bool startChallenge2;
    public bool challenge2Done;
    public CastModeManager spellCast;
    [SerializeField] GameObject sceneTimer;
    [SerializeField] BossScript bossScript;

    // Update is called once per frame
    void Update()
    {
        Challenge1();
        Challenge2();
    }

    private void Challenge1()
    {
        if (startChallenge)
        {

            //timer
            sceneTimer.GetComponent<Image>().fillAmount -= Time.deltaTime;
            if (sceneTimer.GetComponent<Image>().fillAmount <= 0)
            {
                sceneTimer.SetActive(false);
                challengeDone = true;
            }
            else if (spellCast.availableSpellID == 30 && Input.GetKeyDown(KeyCode.E))
            {
                sceneTimer.SetActive(false);
                challengeDone = true;
            }
            if (!challengeDone)
            {
                sceneTimer.SetActive(true);
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
}

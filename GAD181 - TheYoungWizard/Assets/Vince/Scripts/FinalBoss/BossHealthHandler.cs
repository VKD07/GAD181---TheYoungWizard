using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthHandler : MonoBehaviour
{
    [SerializeField] GameObject[] rockHealth;
    [SerializeField] GameObject[] shatteredRock;
    [SerializeField] ParticleSystem powerBeam;
    [SerializeField] GameObject bossPowerBeam;
    [SerializeField] bool finalCutScene;
    [SerializeField] AudioClip shatteredRockSfx;
    AudioSource audioSource;
    BossScript bossScript;
    public bool firstRockShattered;

    void Start()
    {
        bossScript = GetComponent<BossScript>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!finalCutScene)
        {
            if (bossScript.GetBossHealth() <= 800 && rockHealth[0] != null)
            {
                firstRockShattered = true;
                ShatteredRock(0);
            }
            else if (bossScript.GetBossHealth() <= 600 && rockHealth[1] != null)
            {
                ShatteredRock(1);
            }
            else if (bossScript.GetBossHealth() <= 400 && rockHealth[2] != null)
            {
                ShatteredRock(2);
            }
            else if (bossScript.GetBossHealth() <= 200 && rockHealth[3] != null)
            {
                ShatteredRock(3);
            }
            else if (bossScript.GetBossHealth() <= 50 && rockHealth[4] != null)
            {
                SceneManager.LoadScene("FinalBoss 1");//final cutscene
            }
        }
    }

    public void ShatteredRock(int rockNum)
    {
        if (rockHealth[rockNum] == null)
        {
            return;
        }

        GameObject shatteredObj = Instantiate(shatteredRock[rockNum], rockHealth[rockNum].transform.position, rockHealth[rockNum].transform.rotation);
        audioSource.PlayOneShot(shatteredRockSfx, 0.5f);
        //spawn a repica everytime a rock is shattered
        //GameObject cloneObj = Instantiate(bossScript.bossClone, bossScript.multipleFireBallSpawners[0].position, Quaternion.identity);
        //Destroy(cloneObj, bossScript.cloneDuration);
        Destroy(rockHealth[rockNum]);
    }

    public void ReleaseCharge()
    {
        powerBeam.Play();
    }

    public void StopCharge()
    {
        if (bossPowerBeam != null)
        {
            bossPowerBeam.SetActive(false);
        }
    }
}

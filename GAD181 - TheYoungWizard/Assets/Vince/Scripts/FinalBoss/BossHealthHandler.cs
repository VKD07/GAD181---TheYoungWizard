using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthHandler : MonoBehaviour
{
    public static BossHealthHandler instance { get; private set; }
    [SerializeField] GameObject[] rockHealth;
    [SerializeField] GameObject[] shatteredRock;
    [SerializeField] ParticleSystem powerBeam;
    [SerializeField] GameObject bossPowerBeam;
    [SerializeField] public bool finalCutScene;
    [SerializeField] AudioClip shatteredRockSfx;
    AudioSource audioSource;
    BossScript bossScript;
    public bool firstRockShattered;

    void Start()
    {
        instance = this;
        bossScript = GetComponent<BossScript>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ShatteredRocks();
        //ResetRocks();
    }

    private void ShatteredRocks()
    {
        if (!finalCutScene)
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
              //  SceneManager.LoadScene(7);
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
        //rockHealth[rockNum].SetActive(false);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthHandler : MonoBehaviour
{
    [SerializeField] GameObject[] rockHealth;
    [SerializeField] GameObject[] shatteredRock;
    [SerializeField] ParticleSystem powerBeam;
    BossScript bossScript;

    void Start()
    {
        bossScript = GetComponent<BossScript>();
    }

    void Update()
    {
        if (bossScript.GetBossHealth() <=  800 && rockHealth[0] != null)
        {
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
        else if (bossScript.GetBossHealth() <= 0 && rockHealth[4] != null)
        {
            ShatteredRock(4);
        }

    }

    void ShatteredRock(int rockNum)
    {
        if (rockHealth[rockNum] == null)
        {
            return;
        }

        GameObject shatteredObj = Instantiate(shatteredRock[rockNum], rockHealth[rockNum].transform.position, rockHealth[rockNum].transform.rotation);
        //spawn a repica everytime a rock is shattered
        //GameObject cloneObj = Instantiate(bossScript.bossClone, bossScript.multipleFireBallSpawners[0].position, Quaternion.identity);
        //Destroy(cloneObj, bossScript.cloneDuration);
        Destroy(rockHealth[rockNum]);
    }

    public void ReleaseCharge()
    {
        powerBeam.Play();
    }
}

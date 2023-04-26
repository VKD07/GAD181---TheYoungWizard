using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpellUnlockHandler : MonoBehaviour
{
    public bool unlockWindGust;
    public bool unlockIceWall;
    public bool unlockLuminous;

    public CastModeManager castModeManager;
    public BossScript bossScript;
    private void Update()
    {
        CheckBossHealth();
        UnlockSpells();
    }

    private void CheckBossHealth()
    {
        bossScript = FindObjectOfType<BossScript>();

        if (bossScript != null)
        {
            if (bossScript.GetBossHealth() <= 0)
            {
                SceneManager.LoadScene("FinalCutScene");  
            }
        }
    }

    private void UnlockSpells()
    {
        castModeManager = FindObjectOfType<CastModeManager>();

        if (castModeManager != null)
        {
            if (unlockWindGust)
            {
                castModeManager.windGustLocked = false;
            }
            else if (unlockIceWall)
            {
                castModeManager.IceSpellLocked = false;
            }
            if (unlockLuminous)
            {
                castModeManager.luminousLocked = false;
            }
        }
    }
}

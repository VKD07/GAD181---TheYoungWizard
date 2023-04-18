using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUnlockHandler : MonoBehaviour
{
    public bool unlockWindGust;
    public bool unlockIceWall;
    public bool unlockLuminous;

    CastModeManager castModeManager;
    private void Update()
    {
        UnlockSpells();
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
            else if (unlockLuminous)
            {
                castModeManager.luminousLocked = false;
            }
        }
    }
}

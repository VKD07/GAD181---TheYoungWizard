using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDeathBG : MonoBehaviour
{
    [SerializeField] GameObject deathBg;
    private void Awake()
    {
        deathBg = GameObject.Find("DeathBG");

        if(deathBg != null && deathBg.activeSelf)
        {
            deathBg.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

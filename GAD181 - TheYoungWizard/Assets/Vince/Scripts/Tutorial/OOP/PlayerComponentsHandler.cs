using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentsHandler : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Player_Movement pm;
    [SerializeField] playerCombat pCombat;
    [SerializeField] GameObject thirdPersonCam;
    [SerializeField] GameObject castMode;
    [SerializeField] GameObject guideUI;
    [SerializeField] GameObject playerAttrib;
    void Start()
    {
        thirdPersonCam.SetActive(false);
        pm.enabled = false;
        pCombat.enabled = false;
        castMode.SetActive(false);
        playerAttrib.SetActive(false);
         
    }
    
    public void EnablePlayerMovement(bool value)
    {
        pm.enabled = value;   
        thirdPersonCam.SetActive(value);
    }

    public void EnablePlayerCombat()
    {
        pCombat.enabled = true;
    }
}

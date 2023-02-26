using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;


public class MapScript : MonoBehaviour
{
    [Header("enabling and disabling components")]
    [SerializeField] GameObject mapImage;
    [SerializeField] RoomScene_Player playerMovement;
    [SerializeField] GameObject thirdPersonCamera;

    [Header("Portal Components & Settings")]
    [SerializeField] float emissionValue = 5f;
    [SerializeField] Material portalMaterial;
    [SerializeField] Material trailMaterial;
    [SerializeField] Light portalLight;


    private void Update()
    {
        closeUI();
    }

    private void closeUI()
    {
        //closes UI when you press escape button
        if(mapImage.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            mapImage.SetActive(false);
            playerMovement.enabled = true;
            thirdPersonCamera.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    //Close the button 
    public void exitUI()
    {
        mapImage.SetActive(false);
        playerMovement.enabled = true;
        thirdPersonCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SnowyCavePortal()
    {
        //chaning the base material color and emission color
        Color blueColor = new Color(0.2726619f, 1.162414f, 2.603922f);
        portalMaterial.SetColor("_EmissionColor", blueColor);
        trailMaterial.SetColor("_EmissionColor", blueColor * emissionValue);
        portalLight.color = blueColor;
    }

    public void MagicalForest()
    {
        Color purpleColor = new Color(1f, 0.2879581f, 1f);
        portalMaterial.SetColor("_EmissionColor", purpleColor);
        trailMaterial.SetColor("_EmissionColor", purpleColor * emissionValue);
        portalLight.color = purpleColor;
    }

    public void NormalForest()
    {
        Color greenColor = new Color(0.3443396f, 1f, 0.3612598f);
        portalMaterial.SetColor("_EmissionColor", greenColor);
        trailMaterial.SetColor("_EmissionColor", greenColor * emissionValue);
        portalLight.color = greenColor;
    }
}

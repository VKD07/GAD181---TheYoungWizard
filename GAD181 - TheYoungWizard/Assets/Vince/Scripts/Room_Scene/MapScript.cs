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
    [SerializeField] Material portalParticleMaterial;
    [SerializeField] Light portalLight;
    [SerializeField] bool[] portalActive;
    [SerializeField] GameObject portalVfx;
    public int activePortal;

    private void Start()
    {
        portalVfx.SetActive(false);
    }

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
        portalParticleMaterial.SetColor("_EmissionColor", blueColor * emissionValue);
        portalLight.color = blueColor;
        activePortal = 1;
        portalVfx.SetActive(true);
    }

    public void MagicalForest()
    {
        Color purpleColor = new Color(1f, 0.2879581f, 1f);
        portalMaterial.SetColor("_EmissionColor", purpleColor);
        trailMaterial.SetColor("_EmissionColor", purpleColor * emissionValue);
        portalParticleMaterial.SetColor("_EmissionColor", purpleColor * emissionValue);
        portalLight.color = purpleColor;
        activePortal = 2;
        portalVfx.SetActive(true);
    }

    public void NormalForest()
    {
        Color greenColor = new Color(0.3443396f, 1f, 0.3612598f);
        portalMaterial.SetColor("_EmissionColor", greenColor);
        trailMaterial.SetColor("_EmissionColor", greenColor * emissionValue);
        portalParticleMaterial.SetColor("_EmissionColor", greenColor * emissionValue);
        portalLight.color = greenColor;
        portalVfx.SetActive(true);
    }

    private void PortalToActivate(int portal)
    {
        //enablingPortal
        for (int i = 0; i < portalActive.Length; i++)
        {
            if (i == portal)
            {
                portalActive[i] = true;
            }
            else
            {
                portalActive[i] = false;
            }
        }
    }
}

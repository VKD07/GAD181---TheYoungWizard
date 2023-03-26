using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForceField : MonoBehaviour
{

    [Header("Material Properties")]
    [SerializeField] GameObject forceField;
    [SerializeField] Color shieldColor;
    [SerializeField] float emissionIntensity = 5f;
    [SerializeField] Renderer render;
    Material renderMaterial;

    [Header("Animation")]
    float maximumScale = 3.066821f;
    float currentScale;
    public bool shieldIsActive;
    Animator anim;

    [Header("Settings")]
    [SerializeField] KeyCode activatePortalKey = KeyCode.Space;
    [SerializeField] float shieldDuration = 1f;
    public float currentShieldTime;
    playerCombat pc;
    Player_Movement pm;

    void Start()
    {
        pc = FindObjectOfType<playerCombat>();
        pm = FindObjectOfType<Player_Movement>();
        anim = GetComponent<Animator>();
        renderMaterial = render.material;
        renderMaterial.SetColor("_Emission", shieldColor);
        renderMaterial.SetFloat("_Intensity", emissionIntensity);
        forceField.SetActive(false);
    }
    private void Update()
    {
        KeyBoardSwitch();
        ActivateForceField();
        DeactivateForceField();
    }

    private void KeyBoardSwitch()
    {
        if (Input.GetKeyDown(activatePortalKey) && shieldIsActive == false &&  pm.rolling == false)
        {
            pm.stopMoving = true;
            anim.SetTrigger("Block");
            shieldIsActive = true;
        }
    }

    private void ActivateForceField()
    {
        if (shieldIsActive && currentScale < maximumScale)
        {
            pc.disableSenses();
            forceField.SetActive (true);
            currentScale += Time.deltaTime * 30f;
            forceField.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
    }

    void DeactivateForceField()
    {
        if(shieldIsActive && currentScale >= maximumScale && currentShieldTime < shieldDuration)
        {
            currentShieldTime += Time.deltaTime;
        }
        else if(currentShieldTime >= shieldDuration)
        {
            pm.stopMoving = false;
            forceField.transform.localScale = Vector3.zero;
            shieldIsActive = false;
            currentScale = 0;
            currentShieldTime = 0;
            forceField.SetActive(false);
        }
    }

}

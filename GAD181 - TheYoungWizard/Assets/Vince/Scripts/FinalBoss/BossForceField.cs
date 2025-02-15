using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForceField : MonoBehaviour
{

    [Header("Material Properties")]
    [SerializeField] Color[] colors;
    [SerializeField] float emissionIntensity = 5f;
    [SerializeField] public BossScript bossScript;

    Renderer render;
    Material renderMaterial;
    MeshRenderer meshRenderer;

    [Header("Animation")]
    float maximumScale = 2;
    float currentScale;
    [SerializeField] ParticleSystem explodeVfx;
    [SerializeField] Material explodeVfxMat;

    [Header("Choosing an Element")]
    public int randomElement;
    bool elementChosen;
    public bool activateShield;

    [Header("Number of Broken Shields")]
    public int numberOfBrokenShields;
    public bool isDummy;

    [Header("VFX")]
    [SerializeField] GameObject healingVFX;

    [Header("SFX")]
    [SerializeField] BossSFXHandler sfx;
    bool shieldActiveSfx;
    bool shieldDeactiveSfx;

    void Awake()
    {
        render = GetComponent<Renderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        renderMaterial = render.material;
        renderMaterial.SetFloat("_Intensity", emissionIntensity);
        activateShield = false;
        transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        ActivateForceField();
        DeactivateForceField();
        ChooseAnElement();
    }

    private void ChooseAnElement()
    {
        if (activateShield && elementChosen == false)
        {
            elementChosen = true;
            randomElement = UnityEngine.Random.Range(0, colors.Length);
            renderMaterial.SetColor("_Emission", colors[randomElement]);
        }
        else if (!activateShield && elementChosen == true)
        {
            elementChosen = false;
            randomElement = 0;
        }
    }

    private void ActivateForceField()
    {
        if (currentScale < maximumScale && activateShield)
        {
            meshRenderer.enabled = true;
            currentScale += Time.deltaTime * 15f;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            if (!shieldActiveSfx)
            {
                shieldDeactiveSfx = false;
                shieldActiveSfx = true;
                sfx.ActivateShieldSfx();
            }
        }
    }

    void DeactivateForceField()
    {
        if (currentScale > 0 && !activateShield)
        {
            
            currentScale -= Time.deltaTime * 20f;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            meshRenderer.enabled = false;

            if (!shieldDeactiveSfx)
            {
                sfx.ExplodingShieldSound();
                shieldDeactiveSfx = true;
                shieldActiveSfx = false;
            }

            if (healingVFX != null)
            {
                healingVFX.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (randomElement == 0 && other.tag == "Fireball" && activateShield == true)
        {
            explodeVfxMat.SetColor("_EmissionColor", colors[0] * 2.4f);
            explodeVfx.Play();
            activateShield = false;
            InterruptBoss();

            if (isDummy)
            {
                numberOfBrokenShields += 1;
            }

        }
        else if (randomElement == 1 && other.tag == "frostWall" && activateShield == true)
        {
            explodeVfxMat.SetColor("_EmissionColor", colors[1] * 2.4f);
            explodeVfx.Play();
            activateShield = false;
            InterruptBoss();

            if (isDummy)
            {
                numberOfBrokenShields += 1;
            }
        }
        else if (randomElement == 2 && other.tag == "windGust" && activateShield == true)
        {
            Player_SpellCast spellCast = FindObjectOfType<Player_SpellCast>();
            if (spellCast.releaseWind == true)
            {
                explodeVfxMat.SetColor("_EmissionColor", colors[2] * 2.4f);
                explodeVfx.Play();
                activateShield = false;
                InterruptBoss();

                if (isDummy)
                {
                    numberOfBrokenShields += 1;
                }
            }
        }
    }

    public void InterruptBoss()
    {
        activateShield = false;
        if (bossScript != null)
        {
            bossScript.damageBoss = true;
            bossScript.playStunVfx();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyForceField : MonoBehaviour
{
    [Header("Material Properties")]
    [SerializeField] Color[] colors;
    [SerializeField] float emissionIntensity = 5f;

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
        }
    }

    void DeactivateForceField()
    {
        if (currentScale > 0 && !activateShield)
        {
            currentScale -= Time.deltaTime * 20f;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            meshRenderer.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (randomElement == 0 && other.tag == "Fireball" && activateShield == true)
        {
            explodeVfxMat.SetColor("_EmissionColor", colors[0] * 2.4f);
            explodeVfx.Play();
            activateShield = false;

        }
        else if (randomElement == 1 && other.tag == "frostWall" && activateShield == true)
        {
            explodeVfxMat.SetColor("_EmissionColor", colors[1] * 2.4f);
            explodeVfx.Play();
            activateShield = false;
        }
        else if (randomElement == 2 && other.tag == "windGust" && activateShield == true)
        {
            Player_SpellCast spellCast = FindObjectOfType<Player_SpellCast>();
            if (spellCast.releaseWind == true)
            {
                explodeVfxMat.SetColor("_EmissionColor", colors[2] * 2.4f);
                explodeVfx.Play();
                activateShield = false;
            }
        }
    }

 
}

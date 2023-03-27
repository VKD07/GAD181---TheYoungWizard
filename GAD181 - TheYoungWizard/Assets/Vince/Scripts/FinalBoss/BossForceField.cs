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

    [Header("Animation")]
    float maximumScale = 2;
    float currentScale;

    [Header("Choosing an Element")]
    public int randomElement;
    bool elementChosen;
    public bool activateShield;

    void Awake()
    {
        render = GetComponent<Renderer>();
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
            currentScale += Time.deltaTime * 15f;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
    }

    void DeactivateForceField()
    {
        if (currentScale != 0 && !activateShield)
        {
            currentScale -= Time.deltaTime * 20f;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (randomElement == 0 && other.tag == "Fireball" && activateShield == true)
        {
            activateShield = false;
            InterruptBoss();

        }
        else if (randomElement == 1 && other.tag == "frostWall" && activateShield == true)
        {
            activateShield = false;
            InterruptBoss();
        }
        else if (randomElement == 2 && other.tag == "windGust" && activateShield == true)
        {
            Player_SpellCast spellCast = FindObjectOfType<Player_SpellCast>();
            if (spellCast.releaseWind == true)
            {
                activateShield = false;
                InterruptBoss();
            }
        }
    }

    public void InterruptBoss()
    {
        activateShield = false;
        bossScript.damageBoss = true;
        bossScript.playStunVfx();
    }
}

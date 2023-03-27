using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldScript : MonoBehaviour
{
    [Header("Material Properties")]
    [SerializeField] Color[] colors;
    [SerializeField] float emissionIntensity = 5f;
    Renderer render;
    Material renderMaterial;

    [Header("Animation")]
    float maximumScale = 4.372983f;
    float currentScale;

    [Header("Choosing an Element")]
    public int randomElement;
    bool elementChosen;
    public bool activateShield;
    public int randomStartingTime;
    public float currentTime;
    [SerializeField]int minimumStartingTime = 3;
    [SerializeField]int maximumStartingTime = 7;
   
    void Awake()
    {
        render = GetComponent<Renderer>();
        renderMaterial = render.material;
        renderMaterial.SetFloat("_Intensity", emissionIntensity);
        activateShield = false;
        transform.localScale = Vector3.zero;
        RandomizeStartingTime();
    }
    private void Update()
    {
        ActivateForceField();
        DeactivateForceField();
        ChooseAnElement();
    }

    private void ChooseAnElement()
    {
        if (!activateShield)
        {
            if (currentTime < randomStartingTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                activateShield = true;
                RandomizeStartingTime();
            }
        }

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
        if (currentScale > 0 && !activateShield)
        {
            currentScale -= Time.deltaTime * 20f;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (randomElement == 0 && other.tag == "Fireball")
        {
            activateShield = false;
        }
        else if(randomElement == 1 && other.tag == "frostWall")
        {
            activateShield = false;
        }else if(randomElement == 2 && other.tag == "windGust")
        {
            Player_SpellCast spellCast = FindObjectOfType<Player_SpellCast>();
            if(spellCast.releaseWind == true) 
            { 
                activateShield = false;
            }
        }
    }

    void RandomizeStartingTime()
    {
        randomStartingTime = UnityEngine.Random.Range(minimumStartingTime,maximumStartingTime);
    }
}

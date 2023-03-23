using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideUiScript : MonoBehaviour
{
    [Header("Guide UIs")]
    [SerializeField] GameObject combineUI;
    [SerializeField] Image SpellCastModeGuide;
    [SerializeField] Image useSpellGuide;
    [Header("References")]
    [SerializeField] playerCombat pc;
    void Start()
    {
        combineUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpellCastingMode()
    {
        combineUI.SetActive(true);
        SpellCastModeGuide.enabled = false;
        useSpellGuide.enabled = false;
    }

    public void DisableUISpellCastingMode()
    {
        combineUI.SetActive(false);
        SpellCastModeGuide.enabled = true;
        useSpellGuide.enabled = true;
    }
}

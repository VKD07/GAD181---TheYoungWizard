using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastUIAnim : MonoBehaviour
{
    [SerializeField] GuideUiScript guideUiScript;

    public void activateCombineGuideUI()
    {
        guideUiScript.SpellCastingMode();
    }

    public void deactivateCombineGuideUI()
    {
        guideUiScript.DisableUISpellCastingMode();
    }
}

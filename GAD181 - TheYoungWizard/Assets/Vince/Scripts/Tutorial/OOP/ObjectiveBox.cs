using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class ObjectiveBox : MonoBehaviour
{
    [Header("Objective Box")]
    [SerializeField] Animator anim;
    [SerializeField] bool[] objectiveSequence;
    [SerializeField] string[] objectives;
    [SerializeField] TextMeshProUGUI objectiveDesc;
    [SerializeField] GameObject objectiveBox;

    [Header("SFX")]
    [SerializeField] SFXHandler sfx;

    int objNum;
    public void EnableObjectiveBox(bool value)
    {
        objectiveBox.SetActive(value);
    }

    public void SetObjectiveTextNum(int index, string additionalLine)
    {
        objectiveDesc.SetText(objectives[index] + " " + additionalLine);
    }

    public void ObjectiveCompleted(bool value)
    {
        anim.SetBool("Completed",value);

        if(value)
        {
            sfx.PlaySuccessSfx();
        }
    }
}

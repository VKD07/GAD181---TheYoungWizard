using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ObjectiveBox : MonoBehaviour
{
    [Header("Objective Box")]
    [SerializeField] bool[] objectiveSequence;
    [SerializeField] string[] objectives;
    [SerializeField] TextMeshProUGUI objectiveDesc;
    [SerializeField] GameObject objectiveBox;
    int objNum;
    public void EnableObjectiveBox(bool value)
    {
        objectiveBox.SetActive(value);
    }

    public void nextLine()
    {
        if (objNum < objectives.Length - 1)
        {
            objNum++;
            objectiveDesc.SetText(string.Empty);
            SetDialogTextNum(objNum);
        }
    }

    public void SetDialogTextNum(int index)
    {
        objectiveDesc.SetText(objectives[index]);
    }
}

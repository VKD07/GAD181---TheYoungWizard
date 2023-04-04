using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [Header("Dialog Box")]
    [SerializeField] string[] dialogs;
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] float textInterval = 1f;
    public bool isTyping;
    public int dialogNum;

    [Header("Sound FX")]
    [SerializeField] AudioSource dialogAudioSource;
    [SerializeField] AudioClip typingSound;
    bool typed;
    private void Start()
    {
        dialogText.SetText(string.Empty);
    }
    public void EnableDialogBox(bool value)
    {
        dialogBox.SetActive(value);
    }

    public void SetDialogTextNum(int index)
    {
        dialogNum = index;
        StartCoroutine(TypeLine());
    }

    public void nextLine()
    {
        if (dialogNum < dialogs.Length - 1)
        {
            dialogNum++;
            dialogText.SetText(string.Empty);
            StartCoroutine(TypeLine());
        }
        else
        {
            EnableDialogBox(false);
        }
    }

    IEnumerator TypeLine()
    {

        foreach (char c in dialogs[dialogNum].ToCharArray())
        {
            dialogText.text += c;
            if (!typed)
            {
                typed = true;
                dialogAudioSource.PlayOneShot(typingSound, 0.1f);
            }
            yield return new WaitForSeconds(textInterval);
            typed = false;
            isTyping = true;
        }
        isTyping = false;
    }
}

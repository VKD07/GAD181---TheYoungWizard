using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Check Movement")]
    [SerializeField] bool[] keyboardMovements;
    public int keyPressed;
    bool keyboardMovementDone;

    [Header("Script References")]
    DialogBox dialogBox;
    private void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
    }
    void Update()
    {
        MovementTutorial();
    }

    private void MovementTutorial()
    {
        
    }
    //checks if all keys requred have been pressed
    public void CheckIfPlayerMoves()
    {
        if (Input.GetKeyDown(KeyCode.W) && !keyboardMovements[0])
        {
            dialogBox.EnableDialogBox(false);
            keyboardMovements[0] = true;
            keyPressed++;
        }

        if (Input.GetKeyDown(KeyCode.A) && !keyboardMovements[1])
        {
            dialogBox.EnableDialogBox(false);
            keyboardMovements[1] = true;
            keyPressed++;
        }

        if (Input.GetKeyDown(KeyCode.S) && !keyboardMovements[2])
        {
            dialogBox.EnableDialogBox(false);
            keyboardMovements[2] = true;
            keyPressed++;
        }

        if (Input.GetKeyDown(KeyCode.D) && !keyboardMovements[3])
        {
            dialogBox.EnableDialogBox(false);
            keyboardMovements[3] = true;
            keyPressed++;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !keyboardMovements[4])
        {
            dialogBox.EnableDialogBox(false);
            keyboardMovements[4] = true;
            keyPressed++;
        }

        if (keyPressed == 5)
        {
            keyboardMovementDone = true;
        }

    }

    public bool KeyboardMovementDone()
    {
        return keyboardMovementDone;
    }
    //Starts the Basic attack practice tutorial
}

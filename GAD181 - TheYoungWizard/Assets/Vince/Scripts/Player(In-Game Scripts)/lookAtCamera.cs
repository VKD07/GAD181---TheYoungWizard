using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lookAtCamera : MonoBehaviour
{
    [SerializeField] bool lookatCamera2;
    //floating
    [SerializeField] bool activateFloat;
    [SerializeField] float floatMoveSpeed = 1f;
    [SerializeField] float distance = 0.2f;
    Vector3 position;
    float angle;
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);

        if (lookatCamera2)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }

        FloatingMovement();
    }

    private void FloatingMovement()
    {
        if (activateFloat)
        {
            angle += floatMoveSpeed * Time.deltaTime;
            position.y = Mathf.Sin(angle) * distance * Time.deltaTime;
            transform.position += position;
        }
    }
}

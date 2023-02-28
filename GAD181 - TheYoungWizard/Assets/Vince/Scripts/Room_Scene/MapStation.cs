using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStation : MonoBehaviour
{
    [SerializeField] public GameObject floatingText;
    [SerializeField] GameObject thirdPersonCamera;

    private void Start()
    {
       
    }
    private void Update()
    {
        //keep disabling the UI
        floatingText.SetActive(false);
        //make the floating text keep looking at the camera
        floatingText.transform.LookAt(thirdPersonCamera.transform.position);
    }
}

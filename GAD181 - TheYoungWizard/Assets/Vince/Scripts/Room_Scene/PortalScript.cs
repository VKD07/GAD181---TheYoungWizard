using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    MapScript mapScript;
    void Start()
    {
        mapScript = FindObjectOfType<MapScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(mapScript.activePortal);
        }
    }
}

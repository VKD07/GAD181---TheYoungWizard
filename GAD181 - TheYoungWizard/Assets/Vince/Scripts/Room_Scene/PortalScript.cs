using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    MapScript mapScript;
    public int portalNumber;
    void Start()
    {
        mapScript = FindObjectOfType<MapScript>();
    }

    private void Update()
    {
        portalNumber = mapScript.activePortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Player");
            if(mapScript.activePortal > 0)
            {
                SceneManager.LoadScene(mapScript.activePortal);
            }
        }
    }
}

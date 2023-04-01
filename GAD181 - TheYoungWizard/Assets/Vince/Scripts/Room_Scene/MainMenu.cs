using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] bool mainMenuActive;
    [SerializeField] GameObject mainMenuCamera;
    [SerializeField] GameObject mainMenuUi;
    [SerializeField] KeyCode mainMenuKeyCode = KeyCode.Return;
    [SerializeField] GameObject player;
    [SerializeField] CanvasGroup alpha;
    // Update is called once per frame

    private void Start()
    {
        player.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(mainMenuKeyCode) && mainMenuActive == true)
        {
            mainMenuActive = false;
            mainMenuCamera.SetActive(false);
            player.SetActive(true);
        }

        if(mainMenuActive == false)
        {
            alpha.alpha -= Time.deltaTime;
        }
        
    }
}

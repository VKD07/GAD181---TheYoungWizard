using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPointHandler : MonoBehaviour
{
    [SerializeField] public Vector3 storedRespawnPoint;
    [SerializeField] GameObject respawnBtn;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform playerInitialCheckPoint;
    [SerializeField] public bool respawnToCheckPoint;
    [SerializeField] ParticleSystem playerLvlUpFx;
    public bool initialSpawned;

    //playerComponents;
    [SerializeField] playerCombat pc;
    [SerializeField] PlayerForceField pf;
    [SerializeField] GuideUiScript guideUiScript;
    [SerializeField] GameObject deathBG;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform.position = storedRespawnPoint;
    }
    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        pc = FindObjectOfType<playerCombat>();
        pf = FindObjectOfType<PlayerForceField>();
        guideUiScript = FindObjectOfType<GuideUiScript>();
        playerLvlUpFx = GameObject.FindGameObjectWithTag("LevelUpFx")?.GetComponent<ParticleSystem>();

        CheckPlayerHealth();
        if (playerLvlUpFx == null)
        {
            return;
        }
    }

    private void CheckPlayerHealth()
    {
        if (pc != null && pc.GetPlayerHealth() <= 0)
        {
            ShowCursor(true);
            deathBG.SetActive(true);
            EnablePlayerComponents(false);
        }

        if(pc == null) 
        {
            deathBG.SetActive(false);
        }
    }

    public void RespawnPlayerToLastCheckPoint()
    {
        playerTransform.position = storedRespawnPoint + Vector3.one;

        //respawnToCheckPoint = true;
    }

    void ShowCursor(bool enable)
    {
        Cursor.visible = enable;
        if (enable)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SetRespawnPoint(Vector3 pos)
    {
        storedRespawnPoint = pos;
    }

    private void EnablePlayerComponents(bool value)
    {
        pc.enabled = value;
        pf.enabled = value;
        guideUiScript.enabled = value;
    }

    public void RespawnPlayer()
    {
        //check if Final Boss
        if (SceneManager.GetActiveScene().name == "FinalBoss")
        {
            deathBG.SetActive(false);
            ShowCursor(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            ShowCursor(false);
            deathBG.SetActive(false);
            EnablePlayerComponents(true);
            pc.ResetPlayer();
            //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            playerLvlUpFx.Play();
            pc.sfx.PlayhealSfx();
            playerTransform.position = storedRespawnPoint;
            // respawnToCheckPoint = true;
            Time.timeScale = 1f;
            pc.SetPlayerHealth(100);
            ShowCursor(false);
        }
    }

    public void GoBack()
    {
        //SceneManager.LoadScene("RoomScene 1");
        LoadAsync.instance.LoadScene("RoomScene 1");
        deathBG.SetActive(false);
        Time.timeScale = 1f;
        ShowCursor(false);
    }
}


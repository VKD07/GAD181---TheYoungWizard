using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPointHandler : MonoBehaviour
{
    [Header("Spawning Components")]
    [SerializeField] public Vector3 storedRespawnPoint;
    [SerializeField] GameObject respawnBtn;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform playerInitialCheckPoint;
    [SerializeField] Animator playerAnim;
    [SerializeField] public bool respawnToCheckPoint;
    [SerializeField] ParticleSystem playerLvlUpFx;
    [SerializeField] Collider bossTriggerCollider;
    public bool initialSpawned;
    float currentTime;
    bool teleportPlayer;
    public bool respawnToBoss;

    //playerComponents;
    [SerializeField] playerCombat pc;
    [SerializeField] PlayerForceField pf;
    [SerializeField] GuideUiScript guideUiScript;
    [SerializeField] GameObject deathBG;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        playerAnim = GameObject.Find("KaelModel")?.GetComponent<Animator>();
        if (playerTransform != null)
        {
            playerTransform.position = storedRespawnPoint;
        }
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
        TeleportPlayer();
        CheckIfInBossLevel();
    }

    private void CheckPlayerHealth()
    {
        if (pc != null && pc.GetPlayerHealth() <= 0)
        {
            ShowCursor(true);
            deathBG.SetActive(true);
            EnablePlayerComponents(false);
        }

        if (pc == null)
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
      
        //check if player has entered furballs lair so that it will respawn there after death
        if (SceneManager.GetActiveScene().name == "FinalBoss" && respawnToBoss)
        {
            LoadAsync.instance.LoadScene("ResetBoss");
            deathBG.SetActive(false);
            ShowCursor(false);
        }
        //check if Final Boss
        else if (SceneManager.GetActiveScene().name == "FinalBoss")
        {
            deathBG.SetActive(false);
            ShowCursor(false);
            LoadAsync.instance.LoadScene("FinalBoss");
        }else if (SceneManager.GetActiveScene().name == "ResetBoss")
        {
            deathBG.SetActive(false);
            ShowCursor(false);
            LoadAsync.instance.LoadScene("ResetBoss");
        }
        else
        {
            if(playerAnim != null)
            {
                playerAnim.updateMode = AnimatorUpdateMode.Normal;
            }
            teleportPlayer = true;
            ShowCursor(false);
            deathBG.SetActive(false);
            EnablePlayerComponents(true);
            pc.ResetPlayer();
            //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            playerLvlUpFx.Play();
            pc.sfx.PlayhealSfx();
            // respawnToCheckPoint = true;
            Time.timeScale = 1f;
            pc.SetPlayerHealth(100);
            ShowCursor(false);
        }
    }
    private void CheckIfInBossLevel()
    {
        if(SceneManager.GetActiveScene().name == "FinalBoss")
        {
            bossTriggerCollider = GameObject.Find("FinalStageTrigger")?.GetComponent<Collider>();

            if(bossTriggerCollider != null)
            {
                if (bossTriggerCollider.bounds.Intersects(playerTransform.GetComponent<Collider>().bounds))
                {
                    respawnToBoss = true;
                }
            }
        }
    }
    private void TeleportPlayer()
    {
        if(teleportPlayer == true && currentTime < 0.1f)
        {
            currentTime += Time.deltaTime;
            playerTransform.position = storedRespawnPoint;
        }
        else
        {
            teleportPlayer = false;
            currentTime = 0f;
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


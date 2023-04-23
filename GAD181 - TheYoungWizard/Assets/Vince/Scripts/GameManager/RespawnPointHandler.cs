using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPointHandler : MonoBehaviour
{
    [SerializeField] playerCombat pc;
    [SerializeField] public Vector3 storedRespawnPoint;
    [SerializeField] GameObject respawnBtn;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform playerInitialCheckPoint;
    [SerializeField] public bool respawnToCheckPoint;
    [SerializeField] ParticleSystem playerLvlUpFx;
    public bool initialSpawned;
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
        playerLvlUpFx = GameObject.FindGameObjectWithTag("LevelUpFx")?.GetComponent<ParticleSystem>();
       
        if (pc != null &&  pc.GetPlayerHealth() <= 0)
        {
            // player choose to respawn
            if (Input.GetKeyDown(KeyCode.K) && playerLvlUpFx != null)
            {
                //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                playerLvlUpFx.Play();
                playerTransform.position = storedRespawnPoint;
                // respawnToCheckPoint = true;
                Time.timeScale = 1f;
                pc.SetPlayerHealth(100);
                ShowCursor(false);
            }
        }
        if (playerLvlUpFx == null)
        {
            return;
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
}

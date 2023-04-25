using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] RespawnPointHandler respawnPointHandler;
    [SerializeField] Transform playerTransform;

    void Awake()
    {
        respawnPointHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnPointHandler>();
        if(respawnPointHandler != null)
        {
            respawnPointHandler.SetRespawnPoint(transform.position);
        }
        //Destroy(gameObject);
    }

    private void Update()
    {
        PlayerInitPos();
    }

    private void PlayerInitPos()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform != null)
        {
            playerTransform.position = transform.position;
        }
        Destroy(gameObject, 0.5f);
    }
}

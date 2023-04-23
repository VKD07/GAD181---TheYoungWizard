using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRespawnTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] BossScript bossScript;
    [SerializeField] BossHealthHandler healthHandler;
    BoxCollider boxCollider;
    playerCombat pc;
    bool playerFirstDeath;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    private void Update()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCombat>();
        if (pc != null && pc.GetPlayerHealth() <= 0)
        {
            boxCollider.isTrigger = true;
            StartCoroutine(DisableBoss());

            if (!playerFirstDeath)
            {
                playerFirstDeath = true;
                boxCollider.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            boss.SetActive(true);
            StartCoroutine(EnableBlock());
        }
    }

    IEnumerator EnableBlock()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider.isTrigger = false;
        boss.SetActive(true);
    }
    IEnumerator DisableBoss()
    {
        yield return new WaitForSeconds(1f);
        boss.SetActive(false);
        healthHandler.resetRocks = true;
    }
}

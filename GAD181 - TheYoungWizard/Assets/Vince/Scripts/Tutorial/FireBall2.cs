using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall2 : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float speed = 10f;
    GameObject player;
    bool playerShielded;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            player.GetComponent<playerCombat>().enableSenses();

            if (distanceToPlayer <= 3f && !playerShielded && !FindAnyObjectByType<PlayerForceField>().shieldIsActive)
            {
                Time.timeScale = 0f;
            }
      
            if (distanceToPlayer <= 3f && Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<ShieldTutorial>().slowDownTime = false;
                playerShielded = true;
                Time.timeScale = 1f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerForceField")
        {
            GameObject explosionObj = Instantiate(explosion, transform.position, Quaternion.identity);
            player.GetComponent<playerCombat>().damagePlayer2(25);
            Destroy(explosionObj, 1f);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePenguins : MonoBehaviour
{
    GameObject player;
    float damage = 30f;
    void Start()
    {
        player = GameObject.Find("Player(In-Game)");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<playerCombat>().damagePlayer(damage);
            GetComponent<BoxCollider>().enabled = false;
        }

    }
    void Update()
    {
        if(gameObject.tag == "PenguinRight")
        {
            transform.position += transform.forward * 10 * Time.deltaTime;
            Destroy(gameObject, 1.5f);
        }
        if (gameObject.tag == "PenguinLeft")
        {
            transform.position -= transform.forward * 10 * Time.deltaTime;
            Destroy(gameObject, 1.5f);
        }

    }
}

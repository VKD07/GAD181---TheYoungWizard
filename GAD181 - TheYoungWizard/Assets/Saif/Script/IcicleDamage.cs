using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleDamage : MonoBehaviour
{

    GameObject player;
    float damage = 10f;
    void Start()
    {
        player = GameObject.Find("Player(In-Game)");
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.GetComponent<playerCombat>().damagePlayer(damage);
            Destroy(gameObject);
        }
        
    }

    void Update()
    {
        transform.position -= transform.up * 30 * Time.deltaTime;
        Destroy(gameObject, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleDamage : MonoBehaviour
{

    GameObject player;
    float damage = 10f;
    RaycastHit hit;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject indicator;
    bool spawn;
    void Start()
    {
        player = GameObject.Find("Player(In-Game)");
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.GetComponent<playerCombat>().damagePlayer(damage, false);
            Destroy(gameObject);
        }
        
    }

    void Raycast()
    {
        if(Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, layerMask))
        {
            if(spawn == false) 
            { 
            spawn = true;
            GameObject indicatorObject = Instantiate(indicator, hit.point, Quaternion.Euler(-90f,0f,0f));
            Destroy(indicatorObject, 1);
            }
        }
        Debug.DrawLine(transform.position, -transform.up * Mathf.Infinity, Color.red);

    }


    void Update()
    {
        transform.position -= transform.up * 20 * Time.deltaTime;
        Destroy(gameObject, 1);
        Raycast();
    }
}

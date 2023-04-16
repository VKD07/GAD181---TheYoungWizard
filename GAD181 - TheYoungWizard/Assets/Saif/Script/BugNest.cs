using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugNest : MonoBehaviour
{
    [SerializeField] GameObject theBugs;


    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpanwer", 0.5f, 4f);
           // GetComponent<BoxCollider>().enabled = false;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CancelInvoke();
            GetComponent<BoxCollider>().enabled = false;;
        }
    }

    void EnemySpanwer()
    {
        Instantiate(theBugs, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {

    }
   
}

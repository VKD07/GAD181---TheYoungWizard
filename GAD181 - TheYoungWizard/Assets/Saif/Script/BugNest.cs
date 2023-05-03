using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugNest : MonoBehaviour
{
    [SerializeField] GameObject theBugs;
    [SerializeField] float numberOfBugsToSpawn = 3f;
    [SerializeField] float timeInterval = 0.5f;
    [SerializeField] int numberOfBugsSpawned = 0;


    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //InvokeRepeating("EnemySpanwer", 0.5f, 3f);
            //// GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(spawnBugs());
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        CancelInvoke();
    //       // GetComponent<BoxCollider>().enabled = false;;
    //    }
    //}

    void EnemySpanwer()
    {
        Instantiate(theBugs, transform.position, Quaternion.identity);
    }

    IEnumerator spawnBugs()
    {
        while(numberOfBugsSpawned <= numberOfBugsToSpawn)
        {
            EnemySpanwer();
            numberOfBugsSpawned++;

            yield return new WaitForSeconds(timeInterval);
        }
    }
}

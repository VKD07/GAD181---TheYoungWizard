using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceSpell : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] float freezeDuration = 2.5f;
    public float currentTime;

    void Update()
    {
        transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.position = new Vector3(transform.position.x, -5.7f, transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Boss")
        {
            GameObject boss = other.gameObject;

            if (currentTime < freezeDuration)
            {
                currentTime += Time.deltaTime;
                boss.GetComponent<Animator>().enabled = false;
                boss.GetComponent<NavMeshAgent>().enabled = false;
                boss.GetComponent<BossScript>().enabled = false;
            }
            else
            {
                boss.GetComponent<Animator>().enabled = true;
                boss.GetComponent<NavMeshAgent>().enabled = true;
                boss.GetComponent<BossScript>().enabled = true;
            }
        }
    }

    
}

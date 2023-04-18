using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceSpell : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] float freezeDuration = 2.5f;
    [SerializeField]List <GameObject> frozenObjects = new List <GameObject>();
    public float currentTime;
    GameObject [] boss;
    bool bossDetected;

    void Update()
    {
       // icePosition();
       // FreezeBoss();
    }

    private void FreezeBoss()
    {
        boss = GameObject.FindGameObjectsWithTag("Boss");

        if(bossDetected == true)
        {
            if (currentTime < freezeDuration)
            {
                currentTime += Time.deltaTime;
                for (int i = 0; i < boss.Length; i++)
                {
                    if(boss[i].GetComponent<BossScript>().jumpedToPlayer == false)
                    {
                        boss[i].GetComponent<Animator>().enabled = false;
                        boss[i].GetComponent<NavMeshAgent>().enabled = false;
                        boss[i].GetComponent<BossScript>().enabled = false;
                    }
               
                }
        
            }
            else
            {
                for (int i = 0; i < boss.Length; i++)
                {
                    boss[i].GetComponent<Animator>().enabled = true;
                    boss[i].GetComponent<NavMeshAgent>().enabled = true;
                    boss[i].GetComponent<BossScript>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Boss")
        {
            bossDetected = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.SendMessage("Freeze");
            if (!frozenObjects.Contains(other.gameObject))
            {
                frozenObjects.Add(other.gameObject);
            }
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("BossBullet"))
        {
            Destroy(other.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("BossBullet"))
        {
            Destroy(collision.gameObject);
        }
    }


    private void OnDestroy()
    {
        foreach (GameObject obj in frozenObjects)
        {
            obj.SendMessage("UnFreeze");
        }
    }

    //private void icePosition()
    //{
    //    transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    //    // transform.position = new Vector3(transform.position.x, -5.7f, transform.position.z);

    //    if (groundHit == true)
    //    {
    //        transform.position += Vector3.up * Time.deltaTime;
    //    }
    //}




}

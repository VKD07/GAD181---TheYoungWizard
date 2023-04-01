using System.Collections;
using System.Collections.Generic;
using UnityEditor. VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class spiderScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject raycastOrigin;
    [SerializeField] LayerMask layerMask;
    GameObject player;
    [SerializeField] Player playerScript;
    NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        attack();

        if (playerScript == null)
        {
            chase();
        }
    }
    void attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, out hit, 3f, layerMask))
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Run", false);

            playerScript = hit.rigidbody.GetComponent<Player>(); 


            Debug.DrawLine(transform.position, transform.forward * 1f, Color.red);
        }
        else
        {
            playerScript = null;
        }
        Debug.DrawLine(transform.position, transform.forward * 1f, Color.red);





    }
    void chase()
    {
        animator.SetBool("Run", true);
        player = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(player.transform.position);
    }
}
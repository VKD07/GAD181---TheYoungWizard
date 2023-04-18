using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject raycastOrigin;
    [SerializeField] LayerMask layerMask;
    GameObject player;
    [SerializeField]Player playerScript;
    NavMeshAgent agent;
    [SerializeField] AudioScript audioScript;
    bool musicIsPlayed;
   [SerializeField] int skeletonHp;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        skeletonHp = 50;


    }


    private void OnTriggerEnter(Collider obj)
    {

        // animator.SetTrigger("Run00");

    }
    // Update is called once per frame
    void Update()
    {
      
            attack();

        if (playerScript == null)
        {
            chase();
        }
        if (skeletonHp < 0)
        {
            animator.SetTrigger("Dead");
        }
        
    }
    void attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, out hit,3f, layerMask))
        {
           animator.SetTrigger("attack");
           animator.SetBool("walk", false);

            playerScript = hit.rigidbody.GetComponent<Player>();

            if (musicIsPlayed == false)
            {
                musicIsPlayed = true;
                audioScript.PlayMiniBossMusic();

            }
           

        }
        else
        {
            playerScript = null;
        }

        Debug.DrawLine(transform.position, transform.forward * 1f, Color.red);
    }

    void DamagePlayer()
    {
        if (playerScript!=null)
        {
        playerScript.health -= 1;

        }
    }
    void chase()
    {
        animator.SetBool("walk", true);
        player = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(player.transform.position);
    }
}

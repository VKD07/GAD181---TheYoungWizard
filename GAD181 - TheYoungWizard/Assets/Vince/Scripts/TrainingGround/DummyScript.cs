using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "playerBullet" || collision.gameObject.tag == "Fireball")
        {
            animator.SetTrigger("dummyHit");
        }
    }
}

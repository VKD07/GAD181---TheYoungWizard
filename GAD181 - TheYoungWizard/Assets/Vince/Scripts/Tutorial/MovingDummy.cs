using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDummy : MonoBehaviour
{
    [SerializeField] float switchDirectionDuration = 5f;
    [SerializeField] float dummyMoveSpeed = 10f;
    [SerializeField] float distance = 2f;
    float currentTimeToMove;
    public bool isDead;
    public bool startMoving;
    public bool startAttack;
    Vector3 position;
    float angle;
    Animator anim;
    public BasicAttackTutorial basicAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        basicAttack = FindObjectOfType<BasicAttackTutorial>();
        StartMoving();
        StartAttacking();
    }

    private void StartMoving()
    {
        if (startMoving)
        {
            angle += dummyMoveSpeed * Time.deltaTime;
            position.z = Mathf.Cos(angle) * distance;
            transform.position += position;
        }
    }

    void StartAttacking()
    {
        if (startAttack)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void ResetDummy()
    {
        isDead = false;
        anim.SetBool("Reset", true);
    }

    public void KillDummy()
    {
        isDead=true;
        anim.SetBool("Reset", false);
        anim.SetTrigger("Dead");
    }
}

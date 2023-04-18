using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class LichMainScript : MonoBehaviour
{
    protected GameObject player;
    protected NavMeshAgent ai;
    protected Animator anim;

    public abstract void Execute();
}

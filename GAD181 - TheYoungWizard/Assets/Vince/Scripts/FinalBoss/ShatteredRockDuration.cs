using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredRockDuration : MonoBehaviour
{
    [SerializeField] float shatteredRockDuration = 7f;
    void Start()
    {
        Destroy(gameObject, shatteredRockDuration);
    }

}

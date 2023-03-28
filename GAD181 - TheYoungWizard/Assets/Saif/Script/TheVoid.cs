using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheVoid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
    }

}

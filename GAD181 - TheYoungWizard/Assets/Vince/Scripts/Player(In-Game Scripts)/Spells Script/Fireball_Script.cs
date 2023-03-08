using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Script : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dummy")
        {
            Destroy(gameObject);
        }
    }
}

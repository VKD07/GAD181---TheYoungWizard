using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dummy")
        {
            print("Dummy hit");
            Destroy(gameObject);
        }
    }
}

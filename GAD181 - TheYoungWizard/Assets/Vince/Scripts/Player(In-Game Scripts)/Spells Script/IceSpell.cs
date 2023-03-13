using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
       
       transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        

        transform.position = new Vector3(transform.position.x, -5.7f, transform.position.z);

            
    }
}

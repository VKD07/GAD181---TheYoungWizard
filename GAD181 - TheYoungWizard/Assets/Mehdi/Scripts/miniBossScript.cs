using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniBossScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        void OntriggerEnter (Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                gameObject.active = true;
            }
        }
    }
}

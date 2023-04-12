using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] GameObject thePenguins;
    
    void Start()
    {
        InvokeRepeating("SummonPenguins", 0.5f, 3f);
    }
    void SummonPenguins()
    {
        Instantiate(thePenguins, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

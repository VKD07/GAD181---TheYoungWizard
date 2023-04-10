using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleDrop : MonoBehaviour
{
    [SerializeField] GameObject theIcicle;
    void Start()
    {
        
        InvokeRepeating("EnemySpanwer", 0.5f, 5f);
       
    }

    void EnemySpanwer()
    {
        Instantiate(theIcicle, transform.position, Quaternion.identity);
    }
    void Update()
    {
        
    }
}

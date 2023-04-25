using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IcicleDrop : MonoBehaviour
{
    [SerializeField] GameObject theIcicle;
    int randomTime;
    float angle;
    float cloudSpeed = 1f;
    float distance = 5f;
    Vector3 position;
    void Start()
    {

        Invoke("IcicleSpanwer", 0.5f);
    }

    void MovingAround()
    {
        angle += cloudSpeed * Time.deltaTime;
        position.z = Mathf.Cos(angle) * distance * Time.deltaTime;
        transform.position += position;
    }

    void IcicleSpanwer()
    {
        Instantiate(theIcicle, transform.position, Quaternion.identity);
        randomTime = Random.Range(7, 10);
        Invoke("IcicleSpanwer", randomTime);
    }
    void Update()
    {
        MovingAround();
    }
}

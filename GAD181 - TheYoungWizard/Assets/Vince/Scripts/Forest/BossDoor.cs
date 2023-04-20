using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] GameObject shatteredGate;
    [SerializeField] GameObject mainParent;
    bool shattered;
    public bool destroyGate;
    private void Update()
    {
        if(!shattered && destroyGate)
        {
            shattered = true;
            GameObject gateObj = Instantiate(shatteredGate, transform.position, Quaternion.Euler(0f, 180, 0f));
            Destroy(gateObj, 5f);
            Destroy(mainParent, 5f);
            Destroy(gameObject);
        }
    }
}

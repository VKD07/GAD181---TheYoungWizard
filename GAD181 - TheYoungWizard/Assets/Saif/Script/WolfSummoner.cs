using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSummoner : MonoBehaviour
{
    [SerializeField] GameObject theWolf;
    [SerializeField] Transform [] theWolfPlace;
    public void CallWolf()
    {
        Instantiate(theWolf, theWolfPlace[0].transform.position, theWolfPlace[0].transform.rotation);
        Instantiate(theWolf, theWolfPlace[1].transform.position, theWolfPlace[1].transform.rotation);
    }
}

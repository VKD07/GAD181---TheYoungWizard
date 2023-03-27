using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    public Camera cam;
    public void HealthBarTrack(float maxHP, float currentHP)
    {
        healthBar.fillAmount = currentHP / maxHP;
    }

    void Start()
    {
       cam = Camera.main;   
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] private EnemyHealthBar hpBar;
    [SerializeField] float speed;
    float currentHealth;
    public Camera cam;

    void Start()
    {
        currentHealth = maxHealth;
        hpBar.HealthBarTrack(maxHealth, currentHealth);
        cam = Camera.main;
    }


    void Update()
    {

    }

}

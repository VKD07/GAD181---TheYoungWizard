using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MainEnemyScript : MonoBehaviour
{
    protected float enemyHealth;
    protected float enemyDamage;
    protected float enemySpeed;

    public abstract void Chase();
    public abstract void Attack();
    public abstract void Death();
    public abstract void DamageEnemy(float playerDamage);
}

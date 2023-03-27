using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float explosionDamage = 100f;
    [SerializeField] List<Collider> listOfObjects;
    [SerializeField] Collider[] colliders;
    [SerializeField] LayerMask layerMask;
    bool exploded;

    // Update is called once per frame
    void FixedUpdate()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach (Collider collider in colliders)
        {
            if (!exploded)
            {
                exploded = true;
                collider.SendMessage("DamageEnemy", explosionDamage);
            }
        }

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

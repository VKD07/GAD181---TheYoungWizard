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
    public bool exploded;
    Transform playerTransform;
    float distanceToPlayer;

    // Update is called once per frame
    void Update()
    {
        if (!exploded)
        {
            colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            foreach (Collider collider in colliders)
            {

                exploded = true;
                collider.SendMessage("DamageEnemy", explosionDamage);

            }
        }

        TriggerCameraShake();
    }

    private void TriggerCameraShake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < 10)
        {
            CameraShake.instance.ShakeCamera(0.2f, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

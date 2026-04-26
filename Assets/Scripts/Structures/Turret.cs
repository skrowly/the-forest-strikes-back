using UnityEngine;

public class Turret : BuildableStructure
{
    [Header("Turret Settings")]
    public float range = 15f;
    public float fireRate = 0.5f;
    public float damage = 10f;

    public Transform head; // rotates toward target
    public Transform firePoint;

    private float nextFireTime = 0f;

    void Update()
    {
        GameObject target = FindTarget();
        if (target == null) return;

        RotateToward(target);

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot(target);
        }
    }

    GameObject FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);

        float closestDist = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestEnemy = hit.gameObject;
                }
            }
        }

        return closestEnemy;
    }

    void RotateToward(GameObject target)
    {
        Vector3 dir = target.transform.position - head.position;
        dir.y = 0f;

        Quaternion lookRot = Quaternion.LookRotation(dir);
        head.rotation = Quaternion.Lerp(head.rotation, lookRot, Time.deltaTime * 10f);
    }

    void Shoot(GameObject target)
    {
        if (target.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}

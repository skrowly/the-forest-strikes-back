using UnityEngine;
using System.Collections;

public class MeleeWeapon : WeaponBase
{
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    [Header("Swing Animation")]
    public float swingAngle = 90f;
    public float swingSpeed = 10f;
    private bool isSwinging = false;

    public override void Fire()
    {
        if (!CanFire() || isSwinging) return;
        nextFireTime = Time.time + stats.fireRate;
        StartCoroutine(SwingWeapon());
    }

    IEnumerator SwingWeapon()
    {
        isSwinging = true;

        // swing forward
        float elapsed = 0f;
        Quaternion startRot = transform.localRotation;
        Quaternion endRot = startRot * Quaternion.Euler(-swingAngle, 0, 0);

        while (elapsed < 1f / swingSpeed)
        {
            elapsed += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(
                startRot,
                endRot,
                elapsed * swingSpeed
            );
            yield return null;
        }

        // deal damage at peak of swing
        DealDamage();

        // swing back
        elapsed = 0f;
        while (elapsed < 1f / swingSpeed)
        {
            elapsed += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(
                endRot,
                startRot,
                elapsed * swingSpeed
            );
            yield return null;
        }

        transform.localRotation = startRot;
        isSwinging = false;
    }

    void DealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(stats.damage);
            }
        }
    }

    public override void Reload()
    {
        // melee doesn't reload
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
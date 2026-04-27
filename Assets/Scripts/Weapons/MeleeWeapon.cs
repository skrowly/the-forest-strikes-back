using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    public override void Fire()
    {
        if (!CanFire()) return;
        nextFireTime = Time.time + stats.fireRate;

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
        // melee doesn't need reload
    }
}
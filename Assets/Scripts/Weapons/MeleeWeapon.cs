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
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, -swingAngle);

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
        // use camera forward direction for attack center
        Camera cam = Camera.main;
        Vector3 attackCenter = cam != null ?
            cam.transform.position + cam.transform.forward * attackRange :
            transform.position;

        Collider[] hits = Physics.OverlapSphere(
            attackCenter,
            attackRange,
            enemyLayer
        );

        foreach (Collider hit in hits)
        {
            // make sure we're not hitting ourselves
            if (hit.transform.root == transform.root) continue;

            if (hit.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(stats.damage);
                Debug.Log("Melee hit: " + hit.gameObject.name);
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
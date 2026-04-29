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

    [Header("Hit Effect")]
    public Color hitFlashColor = Color.red;
    public float flashDuration = 0.1f;

    public override void Fire()
    {
        if (!CanFire() || isSwinging) return;
        nextFireTime = Time.time + stats.fireRate;
        StartCoroutine(SwingWeapon());
    }

    IEnumerator SwingWeapon()
    {
        isSwinging = true;

        float elapsed = 0f;
        Quaternion startRot = transform.localRotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, -swingAngle);

        // swing forward
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

        // deal damage and show hit effect at peak
        bool hitSomething = DealDamage();
        if (hitSomething)
            StartCoroutine(HitFlash());

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

    bool DealDamage()
    {
        Camera cam = Camera.main;
        Vector3 attackCenter = cam != null ?
            cam.transform.position + cam.transform.forward * attackRange :
            transform.position;

        Collider[] hits = Physics.OverlapSphere(
            attackCenter,
            attackRange,
            enemyLayer
        );

        bool hitEnemy = false;

        foreach (Collider hit in hits)
        {
            if (hit.transform.root == transform.root) continue;

            if (hit.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(stats.damage);
                hitEnemy = true;
                Debug.Log("Melee hit: " + hit.gameObject.name);

                // spawn small flash at hit position
                StartCoroutine(SpawnHitMarker(hit.transform.position));
            }
        }

        return hitEnemy;
    }

    IEnumerator HitFlash()
    {
        // flash the weapon itself red on hit
        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            Color original = rend.material.color;
            rend.material.color = hitFlashColor;
            yield return new WaitForSeconds(flashDuration);
            rend.material.color = original;
        }
    }

    IEnumerator SpawnHitMarker(Vector3 position)
    {
        // small sphere flash at enemy hit point
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.transform.position = position;
        marker.transform.localScale = Vector3.one * 0.2f;
        Destroy(marker.GetComponent<Collider>());

        Renderer rend = marker.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Sprites/Default"));
        rend.material.color = hitFlashColor;

        yield return new WaitForSeconds(0.08f);
        Destroy(marker);
    }

    public override void Reload()
    {
        //melee doesn't need to reload
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Camera cam = Camera.main;
        if (cam != null)
            Gizmos.DrawWireSphere(
                cam.transform.position + cam.transform.forward * attackRange,
                attackRange
            );
    }
}
using UnityEngine;

public class Gun : WeaponBase
{
    public Camera cam;

    public override void Fire()
    {
        if (!CanFire()) return;

        nextFireTime = Time.time + stats.fireRate;
        ConsumeAmmo();

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, stats.range))
        {
            //if (hit.collider.TryGetComponent(out EnemyHealth enemy))
            //{
               // enemy.TakeDamage(stats.damage);
            //}
        }
    }

    public override void Reload()
    {
        int needed = stats.magSize - stats.currentAmmo;
        int toReload = Mathf.Min(needed, stats.reserveAmmo);

        stats.currentAmmo += toReload;
        stats.reserveAmmo -= toReload;
    }
}

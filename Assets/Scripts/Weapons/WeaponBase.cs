using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponStats stats;

    protected float nextFireTime = 0f;

    public abstract void Fire();
    public abstract void Reload();

    public bool CanFire()
    {
        return Time.time >= nextFireTime && stats.currentAmmo > 0;
    }

    protected void ConsumeAmmo()
    {
        stats.currentAmmo--;
    }
}

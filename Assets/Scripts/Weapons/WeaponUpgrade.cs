using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Weapon Upgrade")]
public class WeaponUpgrade : ScriptableObject
{
    public float damageMultiplier = 1f;
    public float fireRateMultiplier = 1f;
    public float ammoBonus = 0f;

    public void Apply(WeaponStats stats)
    {
        stats.damage *= damageMultiplier;
        stats.fireRate *= fireRateMultiplier;
        stats.magSize += Mathf.RoundToInt(ammoBonus);
        stats.currentAmmo = stats.magSize;
    }
}

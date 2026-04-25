using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    public string weaponName;

    public float damage = 20f;
    public float fireRate = 0.2f;
    public float range = 100f;

    public int magSize = 30;
    public int currentAmmo = 30;
    public int reserveAmmo = 90;
}

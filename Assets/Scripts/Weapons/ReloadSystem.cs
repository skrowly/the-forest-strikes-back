using UnityEngine;

public class ReloadSystem : MonoBehaviour
{
    public WeaponBase weapon;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            StartCoroutine(ReloadRoutine());
    }

    private System.Collections.IEnumerator ReloadRoutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        weapon.Reload();
        isReloading = false;
    }
}

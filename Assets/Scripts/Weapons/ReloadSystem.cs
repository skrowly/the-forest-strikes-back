using UnityEngine;
using UnityEngine.InputSystem;

//no longer in use - decided reloading was too complex for game and game player infinite bullets
public class ReloadSystem : MonoBehaviour
{
    public WeaponBase weapon;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame && !isReloading)
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
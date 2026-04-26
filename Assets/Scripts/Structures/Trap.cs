using UnityEngine;

public class Trap : BuildableStructure
{
    [Header("Trap Settings")]
    public float damage = 20f;
    public float cooldown = 2f;

    private bool canTrigger = true;

    void OnTriggerEnter(Collider other)
    {
        if (!canTrigger) return;

        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
                StartCoroutine(ResetTrap());
            }
        }
    }

    System.Collections.IEnumerator ResetTrap()
    {
        canTrigger = false;
        yield return new WaitForSeconds(cooldown);
        canTrigger = true;
    }
}

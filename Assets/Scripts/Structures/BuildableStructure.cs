using UnityEngine;

public class BuildableStructure : MonoBehaviour
{
    [Header("Structure Stats")]
    public int cost = 10;
    public float maxHealth = 100f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
            DestroyStructure();
    }

    public virtual void DestroyStructure()
    {
        Destroy(gameObject);
    }
}

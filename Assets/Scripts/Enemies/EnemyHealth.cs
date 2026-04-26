using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static float globalHealthMultiplier = 1f;
    public float baseHealth = 100f;
    private float currentHealth;

    public delegate void EnemyDeath();
    public event EnemyDeath OnEnemyDeath;

    void Start()
    {
        currentHealth = baseHealth * globalHealthMultiplier;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
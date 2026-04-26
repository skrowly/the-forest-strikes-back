using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void TakeDamage(float amount)
    {
        GetComponent<EnemyHealth>()?.TakeDamage(amount);
    }
}
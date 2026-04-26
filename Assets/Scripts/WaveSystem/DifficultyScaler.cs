using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    public float healthMultiplier = 1.15f;
    public float damageMultiplier = 1.1f;
    public float speedMultiplier = 1.05f;

    void OnEnable()
    {
        WaveManager.OnWaveStarted += ScaleDifficulty;
    }

    void OnDisable()
    {
        WaveManager.OnWaveStarted -= ScaleDifficulty;
    }

    void ScaleDifficulty(int wave)
    {
        EnemyHealth.globalHealthMultiplier *= healthMultiplier;
        EnemyAI.globalDamageMultiplier *= damageMultiplier;
        EnemyAI.globalSpeedMultiplier *= speedMultiplier;
    }
}

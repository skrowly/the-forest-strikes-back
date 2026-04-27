using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    public float healthMultiplier = 1.05f;   // was 1.15
    public float damageMultiplier = 1.02f;   // was 1.1
    public float speedMultiplier = 1.01f;    // was 1.05

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

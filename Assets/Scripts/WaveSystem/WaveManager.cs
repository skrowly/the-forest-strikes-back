using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;

    [Header("Wave Settings")]
    public float timeBetweenWaves = 10f;
    public int startingEnemies = 5;
    public float waveMultiplier = 1.3f;

    private int currentWave = 0;
    private bool waveActive = false;

    public delegate void WaveEvent(int wave);
    public static event WaveEvent OnWaveStarted;
    public static event WaveEvent OnWaveEnded;

    void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
            waveActive = true;
            OnWaveStarted?.Invoke(currentWave);

            int enemyCount = Mathf.RoundToInt(startingEnemies * Mathf.Pow(waveMultiplier, currentWave - 1));
            //spawner.SpawnWave(enemyCount); // Commenting out to avoid errors since SpawnWave is not defined

            // Wait until all enemies are dead
            while (spawner.ActiveEnemyCount > 0)
                yield return null;

            waveActive = false;
            OnWaveEnded?.Invoke(currentWave);
        }
    }
}


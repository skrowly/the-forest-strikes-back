using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;

    [Header("Wave Settings")]
    public float timeBetweenWaves = 10f;

    private int currentWave = 0;

    public delegate void WaveEvent(int wave);
    public static event WaveEvent OnWaveStarted;
    public static event WaveEvent OnWaveEnded;

    void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        while (currentWave < 3)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;

            OnWaveStarted?.Invoke(currentWave);
            spawner.SpawnLevel(currentWave);

            // wait until all enemies are dead
            while (spawner.ActiveEnemyCount > 0)
                yield return null;

            OnWaveEnded?.Invoke(currentWave);

            // after wave 3 player wins
            if (currentWave >= 3)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }
}
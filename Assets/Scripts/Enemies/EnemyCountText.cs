using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    public EnemySpawner spawner;

    //to show on canvas UI on text the number of enemies remaining
    void Update()
    {
        if (enemyCountText != null && spawner != null)
        {
            enemyCountText.text = "Enemies Remaining: "
                + spawner.ActiveEnemyCount;
        }
    }
}
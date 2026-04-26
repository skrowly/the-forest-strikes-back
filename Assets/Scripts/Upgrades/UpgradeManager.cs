using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public List<Upgrade> allUpgrades;
    public int upgradesPerChoice = 3;

    public delegate void UpgradeChoiceEvent(List<Upgrade> choices);
    public static event UpgradeChoiceEvent OnUpgradeChoicesReady;

    void OnEnable()
    {
        WaveManager.OnWaveEnded += GiveUpgradeChoices;
    }

    void OnDisable()
    {
        WaveManager.OnWaveEnded -= GiveUpgradeChoices;
    }

    void GiveUpgradeChoices(int wave)
    {
        List<Upgrade> choices = new List<Upgrade>();

        for (int i = 0; i < upgradesPerChoice; i++)
        {
            Upgrade random = allUpgrades[Random.Range(0, allUpgrades.Count)];
            choices.Add(random);
        }

        OnUpgradeChoicesReady?.Invoke(choices);
    }

    public void ApplyUpgrade(Upgrade upgrade, GameObject player)
    {
        upgrade.ApplyUpgrade(player);
    }
}

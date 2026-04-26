using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Player Upgrade")]
public class PlayerUpgrade : Upgrade
{
    public float healthBonus;
    public float speedMultiplier = 1f;
    public float damageReduction = 0f;

    public override void ApplyUpgrade(GameObject player)
    {
        PlayerHealth hp = player.GetComponent<PlayerHealth>();
        PlayerMovement move = player.GetComponent<PlayerMovement>();

        if (hp != null)
            hp.maxHealth += healthBonus;

        if (move != null)
            move.walkSpeed *= speedMultiplier;
    }
}

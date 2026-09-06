using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthBuff")]
public class HealthBuffController : PowerupEffectController
{
    public float healthBuff;

    public override void ApplyEffect(PlayerController player)
    {
        player.health += healthBuff;
        if (player.health > 100)
        {
            player.health = 100;
        }

        player.healthImage.fillAmount = player.health / 100;
        base.ApplyEffect(player);
    }
}

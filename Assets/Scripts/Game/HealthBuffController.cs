using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthBuff")]
public class HealthBuffController : PowerupEffectController
{
    public float healthBuff;

    public override void ApplyEffect()
    {
        Player.Instance.health += healthBuff;
        if (Player.Instance.health > 100)
        {
            Player.Instance.health = 100;
        }

        Player.Instance.healthImage.fillAmount = Player.Instance.health / 100;
        base.ApplyEffect();
    }
}

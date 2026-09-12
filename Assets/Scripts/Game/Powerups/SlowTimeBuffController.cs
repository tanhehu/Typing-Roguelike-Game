using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Powerups/SlowTimeBuff")]
public class SlowTimeBuffController : PowerupEffectController
{
    public float SlowTimeFactor;
    public override void ApplyEffect(PlayerController player)
    {
        Time.timeScale = SlowTimeFactor;
        base.ApplyEffect(player);
    }

    public override void RemoveEffect(PlayerController player)
    {
        Time.timeScale = 1f;
        base.RemoveEffect(player);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Trim Letter")]
public class TrimFirstTwoBuff : PowerupEffectBase
{
    protected TrimFirstTwoBuff(Buff type) : base(type) { }

    public override void ApplyEffect(PlayerController player)
    {
        WordList.Instance.buff = Buff.TrimFirstTwo;
        base.ApplyEffect(player);
    }

    public override void RemoveEffect(PlayerController player)
    {
        WordList.Instance.buff = Buff.None;
        base.RemoveEffect(player);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Trim Letter Last Two")]
public class TrimLastTwoBuff : PowerupEffectBase
{
    protected TrimLastTwoBuff(Buff type) : base(type) { }

    public override void ApplyEffect(PlayerController player)
    {
        WordList.Instance.buff = Buff.TrimLastTwo;
        base.ApplyEffect(player);
    }

    public override void RemoveEffect(PlayerController player)
    {
        WordList.Instance.buff = Buff.None;
        base.RemoveEffect(player);
    }
}

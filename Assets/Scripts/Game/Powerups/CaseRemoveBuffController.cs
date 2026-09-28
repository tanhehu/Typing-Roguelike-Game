using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/CaseRemoveBuff")]
public class CaseRemoveBuffController : PowerupEffectController
{
    public override void ApplyEffect(PlayerController player)
    {
        WordList.Instance.buff = Buff.RemoveCase;
        base.ApplyEffect(player);
    }

    public override void RemoveEffect(PlayerController player)
    {
        WordList.Instance.buff = Buff.None;
        base.RemoveEffect(player);
    }
}

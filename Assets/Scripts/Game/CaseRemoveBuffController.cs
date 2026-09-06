using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/CaseRemoveBuff")]
public class CaseRemoveBuffController : PowerupEffectController
{
    public float time;

    public override void ApplyEffect(PlayerController player)
    {
        WordList.Instance.BuffApplyCountDown(ref WordList.Instance.caseSensitiveBuff, time);
        base.ApplyEffect(player);
    }
}

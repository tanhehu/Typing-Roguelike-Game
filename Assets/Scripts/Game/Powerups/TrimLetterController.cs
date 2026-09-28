using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Trim Letter")]
public class TrimLetterController : PowerupEffectController
{
    public TrimType trimPos;

    public enum TrimType
    {
        first,
        last
    }

    public override void ApplyEffect(PlayerController player)
    {
        if(trimPos == TrimType.first)
        {
            WordList.Instance.buff = Buff.TrimLetterFirst;
        }
        else if(trimPos == TrimType.last)
        {
            WordList.Instance.buff = Buff.TrimLetterLast;
        }
        base.ApplyEffect(player);
    }

    public override void RemoveEffect(PlayerController player)
    {
        WordList.Instance.buff = Buff.None;
        base.RemoveEffect(player);
    }
}

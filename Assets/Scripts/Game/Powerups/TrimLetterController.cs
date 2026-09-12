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
            WordList.Instance.trimLetterBuff = 1;
        }
        else if(trimPos == TrimType.last)
        {
            WordList.Instance.trimLetterBuff = -1;
        }
        base.ApplyEffect(player);
    }

    public override void RemoveEffect(PlayerController player)
    {
        WordList.Instance.trimLetterBuff = 0;
        base.RemoveEffect(player);
    }
}

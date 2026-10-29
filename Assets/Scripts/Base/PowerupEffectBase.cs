using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupEffectBase : ScriptableObject
{
    public float duration;
    public virtual void ApplyEffect(PlayerController player)
    {

    }

    public virtual void RemoveEffect(PlayerController player)
    {

    }
}

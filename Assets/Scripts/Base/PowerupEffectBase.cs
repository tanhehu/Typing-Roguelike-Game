using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupEffectBase : ScriptableObject
{
    public Buff type;
    public float duration;

    protected PowerupEffectBase(Buff type) { this.type = type; }

    public virtual void ApplyEffect(PlayerController player)
    {

    }

    public virtual void RemoveEffect(PlayerController player)
    {

    }
}

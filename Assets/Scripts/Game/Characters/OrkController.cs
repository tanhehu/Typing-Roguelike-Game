using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrkController : EnemyBase
{
    public override void Start()
    {
        base.Start();
        drop = UnityEngine.Random.Range(1, 100) <= (int)DropChance.Ork;
    }

    public override void OnDeath()
    {
        animator.Play("OrkDeath");
        base.OnDeath();
    }
}

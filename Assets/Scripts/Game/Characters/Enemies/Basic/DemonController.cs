using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonController : EnemyBase
{
    public override void Start()
    {
        base.Start();
        drop = UnityEngine.Random.Range(1, 100) <= (int)DropChance.Demon;
    }

    public override void OnDeath()
    {
        animator.Play("DemonDeath");
        base.OnDeath();
    }
}

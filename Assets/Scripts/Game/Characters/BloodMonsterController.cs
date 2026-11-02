using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BloodMonsterController : EnemyBase
{
    public override void Start()
    {
        base.Start();
        drop = UnityEngine.Random.Range(1, 100) <= (int)DropChance.BloodMonster;
    }

    public override void OnDeath()
    {
        animator.Play("BloodMonsterDeath");
        base.OnDeath();
    }
}

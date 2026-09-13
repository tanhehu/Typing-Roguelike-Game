using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMonsterController : EnemyController
{
    public override void OnDeath()
    {
        animator.Play("BloodMonsterDeath");
        base.OnDeath();
    }
}

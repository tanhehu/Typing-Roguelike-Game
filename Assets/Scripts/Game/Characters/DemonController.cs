using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : EnemyController
{
    public override void OnDeath()
    {
        animator.Play("DemonDeath");
        base.OnDeath();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkController : EnemyController
{
    public override void OnDeath()
    {
        animator.Play("OrkDeath");
        base.OnDeath();
    }
}

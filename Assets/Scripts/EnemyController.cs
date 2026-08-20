using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AllCharacterController
{
    [SerializeField] private float range;

    private Vector3 playerPos => Player.Instance.transform.position;
    private Vector3 distance => playerPos - transform.position;

    public override void Update()
    {
        Direction = distance;
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Flip()
    {
        if((distance.x > 0 && !isFacingRight) || (distance.x <= 0 && isFacingRight))
        {
            base.Flip();
        }
    }
}

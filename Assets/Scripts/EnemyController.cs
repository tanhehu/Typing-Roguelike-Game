using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MoveController
{
    [SerializeField] private float range;
    private bool 
    private Vector3 playerPos => Player.Instance.transform.position;

    private void Update()
    {
        base.Move(playerPos - transform.position);
    }

    public void Attack()
    {

    }
}

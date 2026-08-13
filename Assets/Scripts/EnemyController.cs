using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MoveController
{
    [SerializeField] private float range;

    [Header("Reference")]
    [SerializeField] private Animator animator;
    private Vector3 playerPos => Player.Instance.transform.position;
    private Vector3 distance => playerPos - transform.position;

    public void Update()
    {
        base.Move(distance);
    }

    public void Attack()
    {
        
    }
}

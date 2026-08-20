using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllCharacterController : MoveController
{
    public bool isWalking = false;
    public bool isFacingRight = true;
    public bool isAttacking = false;

    [Header("References")]
    [SerializeField] protected Animator animator;

    public virtual void Update()
    {
        Move();
        Flip();
        Animation();
    }

    public virtual void Move()
    {
        Direction.Normalize();
        base.Move(Direction);
    }

    public virtual void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    public virtual void Attack()
    {

    }

    public virtual void Animation()
    {
        //animator.SetBool("Attack", isAttacking);
    }
}

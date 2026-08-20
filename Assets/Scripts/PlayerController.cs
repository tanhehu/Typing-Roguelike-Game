using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AllCharacterController
{
    private float inputX;
    private float inputY;

    private bool isIdle = true;

    public override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        Direction = new Vector3(inputX, inputY, 0);
        base.Move(Direction);
    }

    public override void Flip()
    {
        if((isFacingRight && inputX < 0) || (!isFacingRight && inputX > 0))
        {
            base.Flip();
        }
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Animation()
    {
        isWalking = inputX * inputX + inputY * inputY >= 0.25f;
        isIdle = !isWalking;
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Walk", isWalking);
        base.Animation();
    }
}

public class Player : SingletonMonobehaviour<PlayerController>
{

}

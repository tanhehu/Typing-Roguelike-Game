using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MoveController
{
    private float inputX;
    private float inputY;

    private bool isIdle = true;
    private bool isWalking = false;
    private bool isFacingRight = true;

    [Header("References")]
    [SerializeField] private Animator animator;

    private void Update()
    {
        Movement();
        Flip();
        Animation();
    }

    private void Movement()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        Direction = new Vector3(inputX, inputY, 0);
        Direction.Normalize();
        base.Move(Direction);
    }

    private void Flip()
    {
        if((isFacingRight && inputX < 0) || (!isFacingRight && inputX > 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = !isFacingRight;
        }
    }

    private void Animation()
    {
        isWalking = inputX * inputX + inputY * inputY >= 0.25f;
        isIdle = !isWalking;
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Walk", isWalking);
    }
}

public class Player : SingletonMonobehaviour<PlayerController>
{

}

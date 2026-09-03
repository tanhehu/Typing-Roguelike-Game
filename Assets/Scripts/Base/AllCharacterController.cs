using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllCharacterController : MoveController
{
    public bool isIdle = true;
    public bool isWalking = false;
    public bool isFacingRight = true;
    public bool isAttacking = false;
    public bool isDying = false;

    [Header("References")]
    [SerializeField] protected Animator animator;
    [SerializeField] private WordController wordPrefab;
    protected WordController word;
    public Vector3 wordOffset;

<<<<<<< HEAD:Assets/Scripts/AllCharacterController.cs

    public delegate void Death();
    public Death deathDelegate;
=======
    public delegate void Death();                                                           
    public Death deathDelegate;                                                 // Death event trigger
>>>>>>> ef556ff625e9715aef00d8cd83c4a9e484483c47:Assets/Scripts/Base/AllCharacterController.cs

    public virtual void Start()
    {
        SpawnWord();
    }

    public virtual void Update()
    {
        Move();
        Flip();
        Attack();
        Animation();
    }

    #region Base
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
        //isAttacking = true;
    }

    public virtual void Animation()
    {
        isIdle = !isWalking && !isAttacking;
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Walk", isWalking);
        animator.SetBool("Attack", isAttacking);
        //animator.SetBool("Death", isDying);
    }

    #endregion

    public virtual void SpawnWord()
    {
<<<<<<< HEAD:Assets/Scripts/AllCharacterController.cs
        word = CreateController.Instance.Create<WordController>(wordPrefab);
        word.transform.SetParent(WordList.Instance.wordCanvas.transform, false);
=======
        word = CreateController.Instance.Create<WordController>(wordPrefab);        
        word.transform.SetParent(WordList.Instance.wordCanvas.transform, false);        // Set word to a separate canvas (word canvas) so the word can appear on game screen
>>>>>>> ef556ff625e9715aef00d8cd83c4a9e484483c47:Assets/Scripts/Base/AllCharacterController.cs
    }
}

public enum LayerMask
{
    Player = 6,
    Enemy = 7,
}

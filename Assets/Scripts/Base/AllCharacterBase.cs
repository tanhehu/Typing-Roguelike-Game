using System;
using UnityEngine;

public abstract class AllCharacterBase : MoveController, IDeath, IWordHolder
{
    private bool isIdle = true;
    public bool isWalking = false;
    public bool isFacingRight = true;
    public bool isAttacking = false;

    [Header("References")]
    [SerializeField] protected Animator animator;

    public WordController word { get; set; }
    public Vector3 wordOffset { get; set; } = new Vector3(0, 1, 0);
    public Action destroy { get; set; }

    private void OnEnable()
    {
        destroy += OnDeath;
    }

    private void OnDisable()
    {
        destroy -= OnDeath;
    }

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

    public virtual void Attack() { }

    public virtual void Animation()
    {
        isIdle = !isWalking && !isAttacking;
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Walk", isWalking);
        animator.SetBool("Attack", isAttacking);
    }

    #endregion

    #region Interfaces

    public GameObject GameObject() { return this.gameObject; }

    public virtual void OnHit(float damage = 0) { }

    public virtual void OnDeath() { }
    #endregion

    public virtual void SpawnWord()
    {
        word = CreateController.Instance.Create<WordController>(WordList.Instance.wordPrb);        
        word.transform.SetParent(WordList.Instance.wordCanvas.transform, false);        // Set word to a separate canvas (word canvas) so the word can appear on game screen
        word.holder = this;
    }
}

public enum LayerMask
{
    Player = 6,
    Enemy = 7,
}

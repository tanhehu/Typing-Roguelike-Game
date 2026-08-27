using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyController : AllCharacterController
{
    [SerializeField] private float range;
    public float damage;

    private Vector3 playerPos => Player.Instance.transform.position;
    private Vector3 distance => playerPos - transform.position;

    private void OnEnable()
    {
        deathDelegate += OnDeath;
    }

    private void OnDisable()
    {
        deathDelegate -= OnDeath;
    }

    public override void Start()
    {
        base.Start();
        word.character = this;
    }

    public override void Update()
    {
        base.Update();
    }

    #region Base
    public override void Move()
    {
        Direction = distance;
        base.Move();
    }
    public override void Attack()
    {
        if(distance.magnitude <= range)
        {
            isAttacking = true;
            base.Attack();
        }
        else
        {
            isAttacking = false;
        }
    }

    public override void Flip()
    {
        if((distance.x > 0 && !isFacingRight) || (distance.x <= 0 && isFacingRight))
        {
            base.Flip();
        }
    }

    public override void Animation()
    {
        isWalking = distance.magnitude > range && !isAttacking;
        base.Animation();
    }

    public void OnDeath()
    {
        isDying = true;
        animator.Play("OrkDeath");
        StartCoroutine(OnDeathCoroutine());
    }

    #endregion

    private IEnumerator OnDeathCoroutine()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

    public override void SpawnWord()
    {
        base.SpawnWord();
        int num = WordList.Instance.ChooseWord(this);
        word.text.text = WordList.Instance.wordList[num];
    }

    public void DrawRange()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OnDrawGizmos()
    {
        DrawRange();
    }
}

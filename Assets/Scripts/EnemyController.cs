using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : AllCharacterController
{
    [SerializeField] private float range;
    public float damage;

    private Vector3 playerPos => Player.Instance.transform.position;
    private Vector3 distance => playerPos - transform.position;

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

    #endregion

    public override void SpawnWord()
    {
        base.SpawnWord();
        foreach (var word in WordList.Instance.wordDictionary)
        {
            if(word.Value)
            {
                this.word.text.text = word.Key;
                WordList.Instance.wordDictionary[word.Key] = false;
                break;
            }
        }
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

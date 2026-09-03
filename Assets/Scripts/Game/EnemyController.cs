using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyController : AllCharacterController
{
    [SerializeField] private float range;
    public float damage;
    public float spawnPowerupChance;

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
        spawnPowerupChance = UnityEngine.Random.Range(1, 100);
        word.character = this;
        this.speed = UnityEngine.Random.Range(0.1f, 0.4f);
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

<<<<<<< HEAD:Assets/Scripts/Game/EnemyController.cs
    public virtual void OnDeath()
=======
<<<<<<< HEAD
    public override void Death()
    {
        base.Death();
=======
    public void OnDeath()
>>>>>>> c6a0af5bb65983fa746adc657aa24f7ccdbd5395:Assets/Scripts/EnemyController.cs
    {
        isDying = true;
        StartCoroutine(OnDeathCoroutine());
>>>>>>> fefa3974b6001809cf68855b6b0fa0f2d4037efe
    }

    #endregion

    private IEnumerator OnDeathCoroutine()
    {
        yield return new WaitForSeconds(1);
        PowerupController powerup;
        if (spawnPowerupChance <= 30)
        {
            powerup = PowerupListController.Instance.SpawnPowerup();
            powerup.transform.position = this.transform.position;
        }
        this.gameObject.SetActive(false);
    }

    public override void SpawnWord()
    {
        base.SpawnWord();
        int num = WordList.Instance.RandomizeWord(this);
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

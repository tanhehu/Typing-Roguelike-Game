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
        deathDelegate += OnDeath;                                                   // Add enemy death logic
    }

    private void OnDisable()
    {
        deathDelegate -= OnDeath;                                                   // Remove enemy death logic
    }

    public override void Start()
    {
        base.Start();
        spawnPowerupChance = UnityEngine.Random.Range(1, 100);                      // Randomize powerup spawn chance
        word.character = this;
        this.speed = UnityEngine.Random.Range(0.1f, 0.4f);                          // Vary enemies speed
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
        if((distance.x > 0 && !isFacingRight) || (distance.x <= 0 && isFacingRight))            // Direction changes
        {
            base.Flip();
        }
    }

    public override void Animation()
    {
        isWalking = distance.magnitude > range && !isAttacking;
        base.Animation();
    }

    public virtual void OnDeath()
    {
        isDying = true;
        StartCoroutine(OnDeathCoroutine());
    }

    #endregion

    private IEnumerator OnDeathCoroutine()
    {
        yield return new WaitForSeconds(1);
        PowerupController powerup;
        if (spawnPowerupChance <= 30)
        {
            powerup = PowerupListController.Instance.SpawnPowerup();                            // Spawn powerup
            powerup.transform.position = this.transform.position;                               // Set powerup position to enemy death position
        }
        this.gameObject.SetActive(false);
    }

    public override void SpawnWord()
    {
        base.SpawnWord();
        int num = WordList.Instance.RandomizeWord(this);                                        // Choose a word from the word pool using its index
        word.text.text = WordList.Instance.wordList[num];                                       
    }

    public void DrawRange()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);                                       // Draw enemy range on Scene tab
    }

    public void OnDrawGizmos()
    {
        DrawRange();
    }
}

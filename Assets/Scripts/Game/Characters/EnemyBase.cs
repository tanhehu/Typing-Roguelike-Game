using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class EnemyBase : AllCharacterBase, IDropPowerup
{
    [Header("Combat")]
    [SerializeField] private float range;
    public float damage;

    public int dropChance { get; set; }
    public bool drop { get; set; }

    private Vector3 playerPos => Player.Instance.transform.position;
    private Vector3 distance => playerPos - transform.position;

    [SerializeField] Transform target;

    NavMeshAgent agent;

    public override void Start()
    {
        base.Start();
        this.speed = UnityEngine.Random.Range(0.1f, 0.4f);                          // Vary enemies speed
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;

        target = Player.Instance.transform;
        Debug.Log(transform);
    }

    public override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        if (agent.enabled)
        {
            agent.SetDestination(target.position);
        }
    }

    #region Base
    public override void Move()
    {
        Direction = distance;
        base.Move();
    }
    public override void Attack()
    {
        isAttacking = distance.magnitude <= range ? true : false;
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
        isWalking = distance.magnitude > range && !isAttacking && speed != 0;
        base.Animation();
    }

    #endregion

    #region Interfaces

    public override void OnHit(float damage = 0) { destroy?.Invoke(); }

    public override void OnDeath()
    {
        TutorialController tutorial = TutorialController.Instance;
        if (tutorial != null)
        {
            tutorial.killCount++;
        }
        StartCoroutine(OnDeathCoroutine());
    }

    #endregion

    private IEnumerator OnDeathCoroutine()
    {
        yield return new WaitForSeconds(1);
        PowerupController powerup;
        if (drop)
        {
            powerup = PowerupListController.Instance.SpawnPowerup();                            // Spawn powerup
            powerup.transform.position = this.transform.position;                               // Set powerup position to enemy death position
        }
        this.gameObject.SetActive(false);
    }

    public override void SpawnWord()
    {
        base.SpawnWord();                                      
        word.text.text = WordList.Instance.RandomizeWord(this);                                 // Choose a word from the word pool using its index                  
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

    public void SetSpeed(EnemyBase enemy, float speed)
    {
        enemy.speed = speed;
    }

    public void SetPowerupChance(EnemyBase enemy, bool drop)
    {
        enemy.drop = drop;
    }
}

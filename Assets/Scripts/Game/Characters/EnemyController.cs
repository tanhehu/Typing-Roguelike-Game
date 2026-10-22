using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class EnemyController : AllCharacterController
{
    [SerializeField] private float range;
    public float damage;
    public float spawnPowerupChance;
    private float spawnPowerupNumber;

    private Vector3 playerPos => Player.Instance.transform.position;
    private Vector3 distance => playerPos - transform.position;

    [SerializeField] Transform target;

    NavMeshAgent agent;
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
        spawnPowerupNumber = UnityEngine.Random.Range(1, 100);                      // Randomize powerup spawn chance
        word.character = this;


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

    public override void OnDeath()
    {
        TutorialController tutorial = TutorialController.Instance;
        if(tutorial != null)
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
        if (spawnPowerupNumber <= spawnPowerupChance)
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

    public void SetSpeed(EnemyController enemy, float speed)
    {
        enemy.speed = speed;
    }

    public void SetPowerupChance(EnemyController enemy, float chance)
    {
        enemy.spawnPowerupChance = chance;
    }

    


   

    private void FixedUpdate() { agent.SetDestination(target.position); }
   
    

}

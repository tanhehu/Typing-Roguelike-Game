using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemBoss : SingletonMonobehaviour<GolemBossController> { }

public class GolemBossController : EnemyBase, ISpawnEnemy
{
    public GolemBossState currentState;
    public int waveCount { get; set; } = 100000;
    public List<EnemyBase> enemyPrefabList { get; set; } = new List<EnemyBase>();
    public List<GameObject> spawners { get; set; }
    [SerializeField] private BulletController bulletPrb;
    [SerializeField] private EnemyBase ork;
    [SerializeField] private EnemyBase demon;
    [SerializeField] private EnemyBase bloodMonster;
    public bool hasSpawn = false;
    public int health = 3;
    public List<GameObject> spawnerList;

    public override void Awake() { }
    public override void Start()
    {
        base.Start();
        word.DisableWord();
        ChangeState(new GolemBossSpawnState(this));
        wordOffset = new Vector3(0, 2, 0);

        enemyPrefabList.Add(ork);
        enemyPrefabList.Add(demon);
        enemyPrefabList.Add(bloodMonster);
        spawnerList = WordList.Instance.bossSpawnEnemy;
    }

    public override void Update()
    {
        Move();
        Flip();

        currentState?.Update();
    }

    public override void Move()
    {
        Direction = Vector3.zero;
    }

    public void ChangeState(GolemBossState next)
    {
        currentState?.Exit();
        currentState = next;
        currentState?.Enter();
    }

    public void CheckAnimEnd()
    {
        ChangeState(new GolemBossIdleWalkState(this));
    }

    public override void OnHit(float damage = 0) 
    {
        if (health > 1) ChangeState(new GolemBossIdleWalkState(this));
        else { ChangeState(new GolemBossDeathState(this)); destroy?.Invoke(); }
    }

    public void Recover()
    {
        SpawnWord();
        word.DisableWord();
    }

    public void SpawnEnemy(int amount)
    {
        float timer = 0;
        waveCount = amount;
        for (int i = 0; i < waveCount; i++)
        {
            int random = UnityEngine.Random.Range(0, spawnerList.Capacity - 1);
            StartCoroutine(SpawnEnemyDelay(spawnerList[random].transform.position, timer));
            timer += UnityEngine.Random.Range(1f, 2f);
        }
    }

    public override void OnDeath()
    {
        StartCoroutine(OnWinCoroutine());
    }

    private IEnumerator OnWinCoroutine()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        MenuController.Instance.OnWin();
    }

    private IEnumerator SpawnEnemyDelay(Vector3 pos, float time)
    {
        yield return new WaitForSeconds(time);

        int random = UnityEngine.Random.Range(0, enemyPrefabList.Capacity - 1);
        EnemyBase enemy = CreateController.Instance.Create<EnemyBase>(enemyPrefabList[random]);
        var agent = enemy.GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.Warp(pos);              // places the agent on the NavMesh at pos
        else
            enemy.transform.position = pos;
    }

    public void Shoot()
    {
        float shootTimeDelay = 0;
        for(int i = 0; i < 4; i++)
        {
            StartCoroutine(BulletDelay(Player.Instance.transform.position, shootTimeDelay));
            shootTimeDelay += UnityEngine.Random.Range(0.5f, 1);
        }
        ChangeState(new GolemBossIdleWalkState(this));
    }

    private IEnumerator BulletDelay(Vector3 pos, float time)
    {
        yield return new WaitForSeconds(time);
        MoveController bullet = CreateController.Instance.Create<BulletController>(bulletPrb);
        bullet.transform.position = pos + new Vector3(-10, UnityEngine.Random.Range(-8, 8), 0);
        bullet.Direction = Vector3.right;
    }
}

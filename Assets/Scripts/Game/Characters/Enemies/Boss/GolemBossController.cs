using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void Start()
    {
        base.Start();
        ChangeState(new GolemBossSpawnState(this));
        wordOffset = new Vector3(0, 2, 0);

        enemyPrefabList.Add(ork);
        enemyPrefabList.Add(demon);
        enemyPrefabList.Add(bloodMonster);
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
        if (health > 1)
        {
            health--;
            word.Destroy();
            ChangeState(new GolemBossIdleWalkState(this));
        }
        else destroy?.Invoke();
    }

    public void SpawnEnemy(Vector3 pos, int amount)
    {
        float timer = 0;
        waveCount = amount;
        for (int i = 0; i < waveCount; i++)
        {
            StartCoroutine(SpawnEnemyDelay(pos, timer));
            timer += UnityEngine.Random.Range(1f, 2f);
        }
    }

    public override void OnDeath()
    {
        StartCoroutine(OnWinCoroutine());
    }

    private IEnumerator OnWinCoroutine()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        MenuController.Instance.OnWin();
    }

    public IEnumerator Recover()
    {
        yield return new WaitForSeconds(1);
        SpawnWord();
    }

    private IEnumerator SpawnEnemyDelay(Vector3 pos, float time)
    {
        yield return new WaitForSeconds(time);

        int distance = UnityEngine.Random.Range(-15, 15);
        if (distance < 8 && distance >= 0) distance = 8;
        else if (distance > -8 && distance < 0) distance = -8;

        int random = UnityEngine.Random.Range(0, enemyPrefabList.Capacity - 1);
        EnemyBase enemy = CreateController.Instance.Create<EnemyBase>(enemyPrefabList[random]);
        enemy.transform.position = pos + new Vector3(distance,distance, 0);
    }

    public void Shoot()
    {
        float shootTimeDelay = 0;
        for(int i = 0; i < 4; i++)
        {
            StartCoroutine(BulletDelay(shootTimeDelay));
            shootTimeDelay += UnityEngine.Random.Range(0.5f, 1);
        }
        ChangeState(new GolemBossIdleWalkState(this));
    }

    private IEnumerator BulletDelay(float time)
    {
        yield return new WaitForSeconds(time);
        MoveController bullet = CreateController.Instance.Create<BulletController>(bulletPrb);
        bullet.transform.position += new Vector3(-10, UnityEngine.Random.Range(-8, 8), 0);
        bullet.Direction = Vector3.right;
    }
}

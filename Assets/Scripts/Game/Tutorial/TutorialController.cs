using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : SingletonMonobehaviour<TutorialController>
{
    public EnemyBase enemyPrb;

    [Header("Spawn Positions")]
    public Transform spawnPos1;
    public Transform spawnPos2;

    [Header("Trigger Zones")]
    public Transform trigger1;
    public Transform trigger2;

    public int killCount = 0;
    private bool goal1 = false;
    private bool goal2 = false;
    private bool goal3 = false;

    private float spawnRate = 10;
    private float count = 0;

    private void Start()
    {
        StartCoroutine(SetEnemySpeed(SpawnEnemy(spawnPos1)));
        StartCoroutine(SetEnemySpeed(SpawnEnemy(spawnPos2)));
    }

    void Update()
    {
        if(killCount >= 2 && !goal1)
        {
            AdjustPos();
            StartCoroutine(SpawnEnemyOnTrigger(trigger1));
            goal1 = true;
        }

        if(killCount >= 4 && !goal2)
        {
            AdjustPos();
            StartCoroutine(SpawnEnemyOnTrigger(trigger2));
            goal2 = true;
        }

        if(killCount >= 6 && !goal3)
        {
            AdjustPos();
            goal3 = true;
        }

        if(goal3)
        {
            count += Time.deltaTime;
            if(count >= spawnRate)
            {
                EnemyBase e = SpawnEnemy(spawnPos1);
                SpawnEnemy(spawnPos2);
                e.SetPowerupChance(e, true);
                count = 0;
            }
        }
    }

    public void AdjustPos()
    {
        spawnPos1.transform.position += new Vector3(10, 0, 0);
        spawnPos2.transform.position += new Vector3(10, 0, 0);
    }

    public EnemyBase SpawnEnemy(Transform pos)
    {
        EnemyBase enemy = CreateController.Instance.Create<EnemyBase>(enemyPrb);
        enemy.SetPowerupChance(enemy, false);
        enemy.transform.position = pos.transform.position;
        return enemy;
    }

    private IEnumerator SetEnemySpeed(EnemyBase enemy)
    {
        yield return new WaitForEndOfFrame();
        enemy.SetSpeed(enemy, 0);
    }

    private IEnumerator SpawnEnemyOnTrigger(Transform trigger)
    {
        while (Vector3.Distance(Player.Instance.transform.position, trigger.position) > 1)
        {
            yield return null;
        }
        EnemyBase e1 = SpawnEnemy(spawnPos1);
        SpawnEnemy(spawnPos2);
        if(trigger == trigger2)
        {
            e1.SetPowerupChance(e1, true);
        }
    }
}

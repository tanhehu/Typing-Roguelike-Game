using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public int waveCount { get; set; }
    public List<EnemyBase> enemyPrefabList { get; set; }
    public List<GameObject> spawnerList { get; set; }
    private Vector3 playerPos => Player.Instance.transform.position;
    public List<EnemyBase> enemyPrefabs;
    public List<GameObject> spawners;
    public int waveNum;
    private bool hasSpawn = false;

    private void Awake()
    {
        enemyPrefabList = enemyPrefabs;
        spawnerList = spawners;
        waveCount = waveNum;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)LayerMask.Player)
        {
            if (hasSpawn) return;

            SpawnEnemy(waveCount);
            hasSpawn = true;
        }
    }

    private IEnumerator SpawnEnemyCoroutine(Vector3 pos, float timer)
    {
        yield return new WaitForSeconds(timer);
        int random = UnityEngine.Random.Range(0, enemyPrefabList.Capacity);
        EnemyBase enemy = CreateController.Instance.Create<EnemyBase>(enemyPrefabList[random]);
        enemy.transform.position = pos;
    }

    public void SpawnEnemy(int amount)
    {
        float timer = 0;
        for (int i = 0; i < amount; i++)
        {
            int random = UnityEngine.Random.Range(0, spawnerList.Capacity);
            StartCoroutine(SpawnEnemyCoroutine(spawnerList[random].transform.position, timer));
            timer++;
        }
    }
}

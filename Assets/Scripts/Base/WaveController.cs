using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public int waveCount;
    private bool triggeredOnce = false;
    public List<EnemyBase> enemyPrefabList;
    public List<GameObject> spawners;
    private Vector3 playerPos => Player.Instance.transform.position;
    private bool spawnReady = true;
    private bool spawnFlag = false;
    private int spawnCount = 0;

    private void Update()
    {
        if (triggeredOnce) return;

        if(spawnFlag)
        {
            if(spawnReady)
            {
                int random = UnityEngine.Random.Range(0, spawners.Capacity);
                StartCoroutine(SpawnEnemy(spawners[random].transform.position));
                spawnCount++;
                spawnReady = false;
            }

            if (spawnCount == waveCount)
            {
                spawnCount = 0;
                spawnFlag = false;
                triggeredOnce = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Player.Instance.gameObject.layer)
        {
            spawnFlag = true;
        }
    }

    private IEnumerator SpawnEnemy(Vector3 pos)
    {
        int random = UnityEngine.Random.Range(0, enemyPrefabList.Capacity);
        yield return new WaitForSeconds(0.5f);
        EnemyBase enemy = CreateController.Instance.Create<EnemyBase>(enemyPrefabList[random], pos);
        enemy.transform.position = pos;
        spawnReady = true;
    }
}

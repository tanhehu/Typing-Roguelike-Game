using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaveController : MonoBehaviour, ISpawnEnemy
{
    public int waveCount { get; set; }
    public List<EnemyBase> enemyPrefabList { get; set; }
    public List<GameObject> spawners { get; set; }
    private Vector3 playerPos => Player.Instance.transform.position;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)LayerMask.Player)
        {
            for(int i = 0; i < waveCount; i++)
            {
                int random = UnityEngine.Random.Range(0, spawners.Capacity);
                StartCoroutine(SpawnEnemyCoroutine(spawners[random].transform.position));
            }
        }
    }

    private IEnumerator SpawnEnemyCoroutine(Vector3 pos)
    {
        yield return new WaitForSeconds(1f);
        SpawnEnemy(pos, 1);
    }

    public void SpawnEnemy(Vector3 pos, int amount)
    {
        int random = UnityEngine.Random.Range(0, enemyPrefabList.Capacity);
        EnemyBase enemy = CreateController.Instance.Create<EnemyBase>(enemyPrefabList[random]);
        enemy.transform.position = pos;
    }
}

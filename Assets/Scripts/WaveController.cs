using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private float spawnTimer = 0;
    public float spawnRate = 4;

    public EnemyController enemyPrefab;
    private Vector3 playerPos => Player.Instance.transform.position;

    void Start()
    {
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
    {
       EnemyController enemy = CreateController.Instance.Create<EnemyController>(enemyPrefab);
       enemy.transform.position = new Vector3(Random.Range(playerPos.x - 10, playerPos.x + 10), Random.Range(playerPos.y - 10, playerPos.y + 10), 0);
    }
}

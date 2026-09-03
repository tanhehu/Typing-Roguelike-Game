using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private float spawnTimer = 0;
    public float spawnRate = 4;
    public float spawnLessZone;

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
        spawnTimer += Time.deltaTime;                       // Time-based enemy spawning
        if(spawnTimer >= spawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
    {
        EnemyController enemy = CreateController.Instance.Create<EnemyController>(enemyPrefab);
        enemy.transform.position = new Vector3(UnityEngine.Random.Range(playerPos.x - 10, playerPos.x + 10), UnityEngine.Random.Range(playerPos.y - 10, playerPos.y + 10), 0);           // Set enemy pos to a random pos around player

        if(Mathf.Abs(Vector3.Magnitude(enemy.transform.position - playerPos)) <= spawnLessZone)             // if the enemy is too close to the player, push it away
        {
            enemy.transform.position += new Vector3(spawnLessZone, spawnLessZone, 0);
        }

    }
}

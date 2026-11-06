using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnEnemy
{
    int waveCount { get; set; }
    List<EnemyBase> enemyPrefabList { get; set; }
    List<GameObject> spawners { get; set; }

    void SpawnEnemy(int amount);
}

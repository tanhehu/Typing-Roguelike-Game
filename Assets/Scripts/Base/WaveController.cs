using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public int waveCount;
    public List<EnemyBase> enemyPrefabList;
    public List<GameObject> spawners;
    private Vector3 playerPos => Player.Instance.transform.position;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Player.Instance.gameObject.layer)
        {
            for(int i = 0; i < waveCount; i++)
            {
                int random = UnityEngine.Random.Range(0, spawners.Capacity);
                StartCoroutine(SpawnEnemy(spawners[random].transform.position));
            }
        }
    }

    private IEnumerator SpawnEnemy(Vector3 pos)
    {
        int random = UnityEngine.Random.Range(0, enemyPrefabList.Capacity);
        yield return new WaitForSeconds(0.5f);
        EnemyBase enemy = CreateController.Instance.Create<EnemyBase>(enemyPrefabList[random]);
        enemy.transform.position = pos;
    }
}

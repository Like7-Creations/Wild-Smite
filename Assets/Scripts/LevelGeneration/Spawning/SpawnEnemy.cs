using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    public float EnemyCount;
    public float radius = 1;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        

        for (int i = 0; i < EnemyCount; i++)
        {
            Vector3 randPos = transform.position + Random.insideUnitSphere * radius;

            int j = Random.Range(0, enemyPrefab.Length);

            Instantiate(enemyPrefab[j], randPos, Quaternion.identity);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}

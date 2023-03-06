using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Ultimate.AI;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float radius;

    public EnemyInfo[] possibleEnemies;

    public int spawnCount;

    float timer;

    bool test;

    void Start()
    {
        test = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 4 & test)
        {
            SpawnEnemies();
            timer = 0;
        }
        
    }
    void SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            float angleIteration = 360 / spawnCount;

            float currentRotation = angleIteration * i;

            EnemyInfo enemy = possibleEnemies[Random.Range(0, possibleEnemies.Length)];
            GameObject enemyObject = Instantiate(enemy.enemyPrefab, transform.position, transform.rotation);

            enemyObject.transform.Rotate(new Vector3(0, currentRotation, 0));
            enemyObject.transform.Translate(new Vector3(radius, 5, 0));

            enemyObject.GetComponent<EnemyStats>().ESR = enemy.stateRange;
            enemyObject.GetComponent<EnemyStats>().GenerateStatValues();
        }
        test = false;
    }
}

[System.Serializable]
public class EnemyInfo
{
    public enum Type
    {
        Melee,
        Ranged,
        Tank
    }

    public Type type;
    public GameObject enemyPrefab;
    public EnemyStatRange stateRange;
}

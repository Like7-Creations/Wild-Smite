using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Ultimate.AI;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public float radius;
    public Vector2Int SpawnCountRange;

    public EnemyInfo[] possibleEnemies;

    public LevelSettings levelSettings;
    LevelData currentLevel;
    LevelSettings.Difficulty currentDifficulty;

    float timer;
    bool test;

    void Awake()
    {
        //test = true;
        currentLevel = levelSettings.GetSelectedLevel();
        currentDifficulty = levelSettings.GetDifficulty();

        for (int i = 0; i < possibleEnemies.Length; i++)
        {
            for (int j = 0; j < currentLevel.enemyData.Length; j++)
            {
                if (possibleEnemies[i].type == currentLevel.enemyData[j].Type)
                    possibleEnemies[i].statRange = currentLevel.enemyData[j].StatRange;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (timer > .5f && !test)
        {            
            SpawnPositions[] rooms = FindObjectsOfType<SpawnPositions>();
            for (int i = 0; i < rooms.Length; i++)
            {
                //SpawnPositions room = child.GetComponent<SpawnPositions>();
                spawnPoints.AddRange(rooms[i].points);
            }
            timer = 0;
            test = true;

            SpawnEnemies();
        }
        else
            timer += Time.deltaTime;
    }

    void SpawnEnemies()
    {
        //Debug.Log ("Spawning Enemies at " + spawnPoints.Count + "Points");
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int amount = Random.Range(SpawnCountRange.x, SpawnCountRange.y);
            //Debug.Log("Spawning " + amount + "enemies");

            for (int j = 0; j < amount; j++)
            {
                float angleIteration = 360 / amount;

                float currentRotation = angleIteration * i;

                EnemyInfo enemy = possibleEnemies[Random.Range(0, possibleEnemies.Length)];
                GameObject enemyObject = Instantiate(enemy.enemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation);

                enemyObject.transform.Rotate(new Vector3(0, currentRotation, 0));
                enemyObject.transform.Translate(new Vector3(radius, 5, 0));

                enemyObject.GetComponent<EnemyStats>().ESR = enemy.statRange;
                enemyObject.GetComponent<EnemyStats>().GenerateStatValues(currentDifficulty);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < spawnPoints.Count; i++)
            Gizmos.DrawWireSphere(spawnPoints[i].transform.position, radius);
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
    public EnemyStatRange statRange;
}

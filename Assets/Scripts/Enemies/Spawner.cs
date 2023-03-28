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

    SpawnPositions[] roomSpawns;

    List<SpawnPositions> enemySpawns = new List<SpawnPositions>();

    List<SpawnPositions> ItemSpawns = new List<SpawnPositions>();


    [Range(0, 100)]
    public float enemyPercentage;
    [Range(0, 100)]
    public float ItemPercentage;

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
            roomSpawns = FindObjectsOfType<SpawnPositions>();

            for (int i = 0; i < roomSpawns.Length; i++)
            {
                switch (roomSpawns[i].spawnType)
                {
                    case SpawnPositions.Type.Enemy:

                        enemySpawns.Add(roomSpawns[i]);

                        break;
                    
                    case SpawnPositions.Type.Item:
                       
                        ItemSpawns.Add(roomSpawns[i]);

                        break;                 
                }
            }

            timer = 0;
            test = true;

            SpawnEnemies();
            SpawnItems();
        }
        else
            timer += Time.deltaTime;
    }

    void SpawnEnemies()
    {
        //Debug.Log ("Spawning Enemies at " + spawnPoints.Count + "Points");

        int roomsAmount = Mathf.RoundToInt(enemySpawns.Count * (enemyPercentage / 100));
        Debug.Log($"attempting to spawn in {roomsAmount} rooms");
        int roomGap = enemySpawns.Count / roomsAmount;

        for (int i = 0; i < enemySpawns.Count; i+= roomGap) 
        {
            enemySpawns[i].SpawnEnemies(SpawnCountRange, possibleEnemies[Random.Range(0, possibleEnemies.Length)], currentDifficulty);
            /*int amount = Random.Range(SpawnCountRange.x, SpawnCountRange.y);
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
            }*/
        }
    }

    void SpawnItems()
    {
        int roomsAmount = Mathf.RoundToInt(ItemSpawns.Count * (ItemPercentage / 100));
        Debug.Log($"attempting to spawn Items in {roomsAmount} rooms");
        int roomGap = ItemSpawns.Count / roomsAmount;

        for (int i = 0; i < ItemSpawns.Count; i++)
        {
            ItemSpawns[i].spawnItem();
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

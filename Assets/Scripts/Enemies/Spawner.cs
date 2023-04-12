using System.Collections;
using System.Collections.Generic;
using Ultimate.AI;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public float radius;
    public Vector2Int SpawnCountRange;

    public EnemyInfo[] possibleEnemies;

    public ItemInfo[] possibleItems;

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
        int currentEnemy = 0;

        int roomsAmount = Mathf.RoundToInt(enemySpawns.Count * (enemyPercentage / 100));
        Debug.Log($"attempting to spawn in {roomsAmount} rooms");
        int roomGap = enemySpawns.Count / roomsAmount;

        for (int i = 0; i < enemySpawns.Count; i+= roomGap) 
        {
            for (int e = 0; e < enemySpawns[i].points.Count; e++)
            {
                int enemyAmount = Random.Range(SpawnCountRange.x, SpawnCountRange.y);

                for (int k = 0; k < enemyAmount; k++)
                {
                    EnemyInfo enemy = possibleEnemies[currentEnemy];
                    if (currentEnemy + 1 >= possibleEnemies.Length) currentEnemy = 0;
                    else currentEnemy++;
                    GameObject enemyObject = Instantiate(enemy.enemyPrefab, enemySpawns[i].points[e].position, enemySpawns[i].points[e].rotation);

                    float angleIteration = 360 / enemyAmount;

                    float currentRotation = angleIteration * i;

                    enemyObject.transform.Rotate(new Vector3(0, currentRotation, 0));
                    enemyObject.transform.Translate(new Vector3(radius, 5, 0));

                    enemyObject.GetComponent<EnemyStats>().ESR = enemy.statRange;
                    enemyObject.GetComponent<EnemyStats>().GenerateStatValues(currentDifficulty);
                }
            }
            enemySpawns[i].SpawnEnemies(SpawnCountRange, possibleEnemies[Random.Range(0, possibleEnemies.Length)], currentDifficulty);
        }
    }

    void SpawnItems()
    {
        int currentItem = 0;

        int roomsAmount = Mathf.RoundToInt(ItemSpawns.Count * (ItemPercentage / 100));
        Debug.Log($"attempting to spawn Items in {roomsAmount} rooms");
        int roomGap = ItemSpawns.Count / roomsAmount;

        for (int i = 0; i < ItemSpawns.Count; i++)
        {
            for (int k = 0; k < ItemSpawns[i].points.Count; k++)
            {
                ItemInfo item = possibleItems[currentItem];
                if (currentItem + 1 >= possibleItems.Length) currentItem = 0;
                else currentItem++;
                Instantiate(item.itemPrefab[Random.Range(0, item.itemPrefab.Length)], ItemSpawns[i].points[k].position, Quaternion.identity);
            }
           // ItemSpawns[i].spawnItem();
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

[System.Serializable]
public class ItemInfo 
{
    public enum Type 
    { 
        Health,
        Buff
    }

    public Type type;
    public GameObject[] itemPrefab;
}

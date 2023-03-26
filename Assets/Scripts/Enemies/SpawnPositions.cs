using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.UI.Image;

public class SpawnPositions : MonoBehaviour
{

    public enum Type
    {
        Enemy,
        Item
    }

    public Type spawnType;

    public List<Transform> points;

    [SerializeField] Item[] items;

    public float radius;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            points.Add(transform.GetChild(i).GetComponent<Transform>());
    }

    public void SpawnEnemies(Vector2 amountRange, EnemyInfo enemy, LevelSettings.Difficulty currentDifficulty)
    {
        for (int i = 0; i < points.Count; i++)
        {
            int enemyAmount = (int)Random.Range(amountRange.x, amountRange.y);
            for (int e = 0; e < enemyAmount; e++)
            {
                GameObject enemyObject = Instantiate(enemy.enemyPrefab, points[i].position, points[i].rotation);

                float angleIteration = 360 / enemyAmount;

                float currentRotation = angleIteration * i;

                enemyObject.transform.Rotate(new Vector3(0, currentRotation, 0));
                enemyObject.transform.Translate(new Vector3(radius, 5, 0));

                enemyObject.GetComponent<EnemyStats>().ESR = enemy.statRange;
                enemyObject.GetComponent<EnemyStats>().GenerateStatValues(currentDifficulty);
            }
        }
    }

    public void spawnItem()
    {
        Instantiate(items[Random.Range(0, items.Length)], points[Random.Range(0, points.Count)].position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStatRange : ScriptableObject
{
    [Header("Base General Stats")]
    public Vector2 Health;
    public Vector2 SPD;

    public Vector2 MATK;
    public Vector2 MDEF;
    public Vector2 MCDN;

    public Vector2 RATK;
    public Vector2 RDEF;
    public Vector2 RCDN;

    [Header("Allocation Settings")]
    public int numOfDice;
    public int diceMaxValue;

    public EnemyInfo.Type enemyType;
    //[Header("Difficulty")]
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public LevelSettings.Difficulty difficulty;

    [Range(2,10)]
    public float lowProbPower;

    [Range(0.001f,0.999f)]
    public float highProbPower;


    public float GeneratePosInRange()
    {
        int generationValue = 0;

        for (int i = 0; i < numOfDice; i++)
        {
            generationValue += Random.Range(1, diceMaxValue);
        }

        float hello = diceMaxValue * numOfDice;
        float percentage = generationValue / hello;


        return percentage;
    }

    public float AllocateStats(Vector2 stat, LevelSettings.Difficulty diff)
    {
        float percentage = 0;
        switch (diff)
        {
            case LevelSettings.Difficulty.Easy:
                percentage = GenerateStats(stat, lowProbPower);
                break;

            case LevelSettings.Difficulty.Medium:
                percentage = (Random.Range(1, 51) + Random.Range(1,51));
                break;

            case LevelSettings.Difficulty.Hard:
                percentage = GenerateStats(stat, highProbPower);
                break; 
        }
        float result;
        result = stat.x + (stat.y - stat.x) * (percentage / 100);
        result = Mathf.RoundToInt(result);

        return result;
       // return 0;
    }

    public float GenerateStats(Vector2 stat, float diffPower)
    {
        float result = Mathf.Floor(stat.x + (stat.y - stat.x) * Mathf.Pow(Random.value, diffPower));
        return result;
    }
}

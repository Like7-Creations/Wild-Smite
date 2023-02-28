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

    //[Header("Difficulty")]
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    [Range(2,10)]
    float lowProbPower;

    [Range(0.001f,0.999f)]
    float highProbPower;


    public float GeneratePosInRange()
    {
        int generationValue = 0;

        for (int i = 0; i < numOfDice; i++)
        {
            generationValue += Random.Range(1, diceMaxValue);
        }

        float hello = diceMaxValue * numOfDice;
        float percentage = generationValue / hello;

        //Debug.Log(percentage);

        return percentage;
    }

    public float AllocateStats(Difficulty dif, Vector2 stat)
    {
        float percentage = 0;
        switch (dif)
        {
            case Difficulty.Easy:
                percentage = GenerateStats(stat, lowProbPower);
                break;

            case Difficulty.Medium:
                percentage = (Random.Range(1, 51) + Random.Range(1,51)) / 100;
                break;

            case Difficulty.Hard:
                percentage = GenerateStats(stat, highProbPower);
                break; 
        }
        float result;
        result = stat.x + (stat.y - stat.x) * percentage;
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

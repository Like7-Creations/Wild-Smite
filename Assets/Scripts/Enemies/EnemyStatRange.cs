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

    public float GeneratePosInRange()
    {
        int generationValue = 0;

        for (int i = 0; i < numOfDice; i++)
        {
            generationValue += Random.Range(1, diceMaxValue);
        }

        float hello = diceMaxValue * numOfDice;
        float percentage = generationValue / hello;

        Debug.Log(percentage);

        return percentage;
    }

    public float AllocateStats(Vector2 Stat)
    {
        float percentage = GeneratePosInRange();
        float result;
        result = Stat.x + (Stat.y - Stat.x) * percentage;
        result = Mathf.RoundToInt(result);

        return result;
    }

    public void GenerateStats(Vector2 stat, float result)
    {
        result = AllocateStats(stat);
    }

    public void GenerateStats(float HP, float speed, float Matk, float Mdef, float Mcdn, float Ratk, float Rdef, float Rcdn)
    {
        HP = AllocateStats(Health);
        speed = AllocateStats(SPD);
        Matk = AllocateStats(MATK);
        Mdef = AllocateStats(MDEF);
        Mcdn = AllocateStats(MCDN);
        Ratk = AllocateStats(RATK);
        Rdef = AllocateStats(RDEF);
        Rcdn = AllocateStats(RCDN);
    }
}

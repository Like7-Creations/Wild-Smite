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


    public float[] GenerateStats()
    {
        float health = GeneratePosInRange();
        float speed =
        float dmg =
    }
}

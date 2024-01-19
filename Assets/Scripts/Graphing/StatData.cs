using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class StatData : ScriptableObject
{
    public List<Stat> stats;
}

[System.Serializable]
public class Stat
{
    public int NumOfDice;
    public int DiceSides;
    public int Rolls;

    public float mean;
    //public float mode;
    public float median;

    public int[] values;

    public Stat(int[] stats , int diceNum, int sides, int rolls)
    {
        NumOfDice = diceNum;
        DiceSides = sides;
        Rolls = rolls;

        values = stats;

        float meanSum = 0;
        for (int i = 0; i < values.Length; i++)
            meanSum += values[i];
        
        mean = meanSum / values.Length;
        median = GetMedian(values);
        //mode = GetMode(values);
    }

    float GetMedian(int[] values)
    {
        if (values == null || values.Length == 0)
            Debug.Log("List is empty");

        int[] sorted = values;
        Array.Sort(sorted);

        int size = sorted.Length;
        int mid = size / 2;

        if (size % 2 != 0)
            return sorted[mid];

        float value1 = sorted[mid];
        float value2 = sorted[mid - 1];
        return (value1 + value2) / 2;
    }

    IEnumerable<float> GetMode(float[] values)
    {
        if (values == null || values.Length == 0)
            Debug.Log("List is empty");

        var dictSource = values.ToLookup(x => x);

        var numOfModes = dictSource.Max(x => x.Count());

        var modes = dictSource.Where(x => x.Count() == numOfModes).Select(x => x.Key);

        return modes;
    }
}

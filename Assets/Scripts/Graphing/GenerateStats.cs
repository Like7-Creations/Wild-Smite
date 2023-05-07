using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GenerateStats : MonoBehaviour
{

    public Vector2 statRange;
    public float probPower;
    public enum Distrubution
    {
        Standard,
        Skewed
    }

    public Distrubution useType;

    public int NumOfDice;
    public int DiceSides;
    public int Rolls;

    public List<int> values;
    public WindowGraph WindowGraph;
    public StatData data;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Rolls; i++)
        {
            int value = 0;
            for (int j = 0; j < NumOfDice; j++)
            {
                value += Random.Range(1, DiceSides + 1);
            }

            float percent = (float)value / (DiceSides * NumOfDice);
            Debug.Log("Percent Value is: " + percent);

            float val = 0;
            switch (useType)
            {
                case Distrubution.Standard:
                    val = percent;
                    val *= 100;
                    break;
                case Distrubution.Skewed:
                    
                    val = Mathf.Floor(statRange.x + (statRange.y - statRange.x) * Mathf.Pow(Random.value, probPower));
                    break;
            }            

            values.Add((int)val);
        }

        values.Sort();
        WindowGraph.ShowGraph(values);
        Stat s = new Stat(values.ToArray(), NumOfDice, DiceSides, Rolls);
        if (data != null)
            data.stats.Add(s);
    }
}

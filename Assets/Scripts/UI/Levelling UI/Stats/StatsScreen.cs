using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsScreen : MonoBehaviour
{

    public StatBar[] bars;

    // Start is called before the first frame update
    public void ShowStats(PlayerStat_Data data)
    {
        for (int i = 0; i < bars.Length; i++)
            bars[i].SetBars(data);
    }

    public void ResetBars()
    {
        for (int i = 0; i < bars.Length; i++)
            bars[i].ResetBar();
    }
}

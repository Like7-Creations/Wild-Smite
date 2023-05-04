using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsScreen : MonoBehaviour
{
    public TMP_Text levelText;
    public bool showLevel;
    public StatBar[] bars;

    // Start is called before the first frame update
    public void ShowStats(PlayerStat_Data data)
    {
        if (levelText != null)
        {

            if (showLevel)
            {
                levelText.color = data.config.PlayerMat.color;
                levelText.text = $"P{data.playerIndex}   //Level {data.lvl}";
            }
            else
                levelText.gameObject.SetActive(false);
        }

        for (int i = 0; i < bars.Length; i++)
            bars[i].SetBars(data);
    }

    public void ResetBars()
    {
        for (int i = 0; i < bars.Length; i++)
            bars[i].ResetBar();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase_Stats : MonoBehaviour
{
    public GameObject statsPrefab;

    public GameObject statsPanel;

    int playerCount;

    // Start is called before the first frame update
    void Awake()
    {
        playerCount = PlayerConfigManager.Instance.GetPlayerConfigs().Count;

        for (int i = 0; i < playerCount; i++)
        {
            GameObject statsObj = Instantiate(statsPrefab, statsPanel.transform);
            statsObj.GetComponent<StatsScreen>().ShowStats(PlayerConfigManager.Instance.GetPlayerConfigs()[i].playerStats);
        }

        statsPanel.SetActive(false);
    }

    public void ToggleStatsPanel(bool toggled)
    {
        statsPanel.SetActive(toggled);
    }
}

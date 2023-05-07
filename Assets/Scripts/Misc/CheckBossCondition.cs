using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossCondition : MonoBehaviour
{

    public int requiredLevel;
    public GameObject Button;

    // Start is called before the first frame update
    void Awake()
    {
        bool readyForBoss = false;

        List<PlayerConfig> configs = new List<PlayerConfig>();
        configs = PlayerConfigManager.Instance.GetPlayerConfigs();
        for (int i = 0; i < configs.Count; i++)
        {
            if (configs[i].playerStats.lvl >= requiredLevel)
            {
                readyForBoss = true;
                break;
            }
        }

        if (readyForBoss)
        {
            Button.SetActive(true);
        }
        else
            Button.SetActive(false);

    }
}

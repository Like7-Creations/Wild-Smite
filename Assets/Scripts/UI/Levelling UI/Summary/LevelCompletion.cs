using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelCompletion : MonoBehaviour
{
    public string HomebaseScene;
    public ExperienceData xpData;
    PlayerStats[] players;

    public int mXP, rXP, tXP;

    [Header("Solo Summary Panel")]
    //Summary Text Items
    public GameObject solo_SummaryPanel;
    public TMP_Text levelText;

    public TMP_Text gainedXPText;
    public TMP_Text currentXPText;
    public Slider xpSlider;

    public Transform battleLogRoot;
    public GameObject logItemPrefab;

    public TMP_Text totalXPText;

    [Header("Coop Summary Panel")]
    public GameObject coop_SummaryPanel;
    public TMP_Text levelText_1;
    public TMP_Text levelText_2;

    public TMP_Text gainedXPText_1;
    public TMP_Text gainedXPText_2;
    public TMP_Text currentXPText_1;
    public TMP_Text currentXPText_2;
    public Slider xpSlider_1;
    public Slider xpSlider_2;

    public Transform battleLogRoot_1;
    public Transform battleLogRoot_2;
    public GameObject logItemPrefab_coop;

    public TMP_Text totalXPText_1;
    public TMP_Text totalXPText_2;

    [Header("LevelUp")]
    public GameObject levelUpPanel;
    public GameObject allocationPanel;
    public GameObject statsPanel;
    bool levelUp;
    Queue<PlayerStats> playersToLevel;

    private void Awake()
    {
        playersToLevel = new Queue<PlayerStats>();

        EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();
        foreach (EnemyStats enemy in enemies)
            enemy.gameObject.SetActive(false);
    }

    public void ShowSummary()
    {
        players = FindObjectsOfType<PlayerStats>();

        if (battleLogRoot.childCount != 0)
            for (int i = 0; i < battleLogRoot.childCount; i++)
                Destroy(battleLogRoot.GetChild(i).gameObject);

        if (players.Length == 1)
        {
            solo_SummaryPanel.SetActive(true);

            PlayerStats p = players[0];

            int xp = 0;
            for (int i = 0; i < p.defeatedEnemies.Count; i++)
            {
                int sumXP = 0;

                if (p.defeatedEnemies[i].enemyType == EnemyInfo.Type.Melee)
                    sumXP = p.defeatedEnemies[i].count * mXP;
                else if (p.defeatedEnemies[i].enemyType == EnemyInfo.Type.Ranged)
                    sumXP = p.defeatedEnemies[i].count * rXP;
                else if (p.defeatedEnemies[i].enemyType == EnemyInfo.Type.Tank)
                    sumXP = p.defeatedEnemies[i].count * tXP;

                xp += sumXP;

                GameObject item = Instantiate(logItemPrefab, battleLogRoot);
                item.GetComponent<LogItem>().SetItem(("" + p.defeatedEnemies[i].enemyType.ToString() + " x" + p.defeatedEnemies[i].count), ("+ " + sumXP.ToString("000") + " XP"));
            }

            //Apply XP to player
            int lvl = p.playerData.lvl;
            levelText.text = "Rank " + lvl;

            int currentMax = xpData.milestones[lvl];
            int currentXP = p.playerData.current_XP + xp;
            xpSlider.minValue = 0;
            xpSlider.maxValue = currentMax;
            xpSlider.value = currentXP;

            gainedXPText.text = "+" + xp.ToString("000");
            currentXPText.text = currentXP.ToString("000") + "/" + currentMax.ToString("000");
            totalXPText.text = xp.ToString("000") + " XP";

            if (currentXP > currentMax)
                playersToLevel.Enqueue(p);

            p.playerData.XPGained(xp);
        }
        else if (players.Length == 2)
        {
            coop_SummaryPanel.SetActive(true);

            PlayerStats p1 = players[0];
            PlayerStats p2 = players[1];

            int xp_1 = 0;
            for (int i = 0; i < p1.defeatedEnemies.Count; i++)
            {
                int sumXP = 0;

                if (p1.defeatedEnemies[i].enemyType == EnemyInfo.Type.Melee)
                    sumXP = p1.defeatedEnemies[i].count * mXP;
                else if (p1.defeatedEnemies[i].enemyType == EnemyInfo.Type.Ranged)
                    sumXP = p1.defeatedEnemies[i].count * rXP;
                else if (p1.defeatedEnemies[i].enemyType == EnemyInfo.Type.Tank)
                    sumXP = p1.defeatedEnemies[i].count * tXP;

                xp_1 += sumXP;

                GameObject item = Instantiate(logItemPrefab, battleLogRoot_1);
                item.GetComponent<LogItem>().SetItem(("" + p1.defeatedEnemies[i].enemyType.ToString() + " x" + p1.defeatedEnemies[i].count), ("+ " + sumXP.ToString("000") + " XP"));
            }
            int xp_2 = 0;
            for (int i = 0; i < p2.defeatedEnemies.Count; i++)
            {
                int sumXP = 0;

                if (p2.defeatedEnemies[i].enemyType == EnemyInfo.Type.Melee)
                    sumXP = p2.defeatedEnemies[i].count * mXP;
                else if (p2.defeatedEnemies[i].enemyType == EnemyInfo.Type.Ranged)
                    sumXP = p2.defeatedEnemies[i].count * rXP;
                else if (p2.defeatedEnemies[i].enemyType == EnemyInfo.Type.Tank)
                    sumXP = p2.defeatedEnemies[i].count * tXP;

                xp_2 += sumXP;

                GameObject item = Instantiate(logItemPrefab, battleLogRoot_2);
                item.GetComponent<LogItem>().SetItem(("" + p2.defeatedEnemies[i].enemyType.ToString() + " x" + p2.defeatedEnemies[i].count), ("+ " + sumXP.ToString("000") + " XP"));
            }

            //Apply XP to players
            int lvl_1 = p1.playerData.lvl;
            levelText_1.text = "Rank " + lvl_1;
            int lvl_2 = p2.playerData.lvl;
            levelText_2.text = "Rank " + lvl_2;

            int currentMax_1 = xpData.milestones[lvl_1];
            int currentXP_1 = p1.playerData.current_XP + xp_1;
            xpSlider_1.minValue = 0;
            xpSlider_1.maxValue = currentMax_1;
            xpSlider_1.value = currentXP_1;
            int currentMax_2 = xpData.milestones[lvl_2];
            int currentXP_2 = p2.playerData.current_XP + xp_2;
            xpSlider_2.minValue = 0;
            xpSlider_2.maxValue = currentMax_2;
            xpSlider_2.value = currentXP_2;

            gainedXPText_1.text = "+" + xp_1.ToString("000");
            currentXPText_1.text = currentXP_1.ToString("000") + "/" + currentMax_1.ToString("000");
            totalXPText_1.text = xp_1.ToString("000") + " XP";
            gainedXPText_2.text = "+" + xp_2.ToString("000");
            currentXPText_2.text = currentXP_2.ToString("000") + "/" + currentMax_2.ToString("000");
            totalXPText_2.text = xp_2.ToString("000") + " XP";

            if (currentXP_1 > currentMax_1)
                playersToLevel.Enqueue(p1);
            if (currentXP_2 > currentMax_2)
                playersToLevel.Enqueue(p2);

            p1.playerData.XPGained(xp_1);
            p2.playerData.XPGained(xp_2);
        }
    }

    public void ContinueScreen()
    {
        ResetLevelPanels();
        levelUpPanel.SetActive(false);

        Debug.Log("Players that leveled up: " + playersToLevel.Count);

        if (playersToLevel.Count > 0)
        {
            levelUpPanel.SetActive(true);
            levelUpPanel.GetComponent<StatAllocations>().BeginAllocations(playersToLevel.Dequeue().playerData);
            Debug.Log("Levelling Player at top of queue, queue is now: " + playersToLevel.Count);
        }
        else
        {
            Debug.Log("No Level Up, Returning to Base");
            ReturnToBase();
        }

        if (solo_SummaryPanel.activeInHierarchy)
            solo_SummaryPanel.SetActive(false);
        if (coop_SummaryPanel.activeInHierarchy)
            coop_SummaryPanel.SetActive(false);
    }

    public void ResetLevelPanels()
    {
        statsPanel.GetComponent<StatsScreen>().ResetBars();
        statsPanel.SetActive(false);

        allocationPanel.SetActive(true);
    }

    public void ReturnToBase()
    {
        List<PlayerConfig> configs = PlayerConfigManager.Instance.GetPlayerConfigs();


        for (int i = 0; i < configs.Count; i++)
        {
            SaveLoadTest.SavePlayerData(configs, PlayerConfigManager.Instance.saveFileName);

            Debug.Log("Saving Data for " + configs[i].Name);
        }

        SceneManager.LoadScene(HomebaseScene);
    }
}

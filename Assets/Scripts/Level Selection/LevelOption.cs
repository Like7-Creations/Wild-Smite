using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelOption : MonoBehaviour
{
    public LevelSettings Settings;
    public string levelToLoad;

    public int playerLevel = 10;
    public bool OptionCreated;

    public GameObject DifBase, DifFilled;
    public Transform DifHolder;

    int levelField;
    LevelSettings.Difficulty difficultyField;

    public TMP_Text Info_UI;
    public GameObject expanded;



    private void Awake()
    {
        List< PlayerConfig> players = PlayerConfigManager.Instance.GetPlayerConfigs();
        int maxLvl = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].playerStats.lvl > maxLvl)
                maxLvl = players[i].playerStats.lvl;
        }

        if (Info_UI == null)
            Info_UI = GetComponentInChildren<TMP_Text>();

        if (!OptionCreated)
        {
            levelField = Random.Range(0, maxLvl);
            difficultyField = (LevelSettings.Difficulty)Random.Range(0, 3);

            if (Info_UI != null)
            {
                for (int i = 0; i <= (int)difficultyField; i++)
                {
                    Instantiate(DifFilled, DifHolder);
                }
                for (int i = 2; i > (int)difficultyField; i--)
                {
                    Instantiate(DifBase, DifHolder);
                }

                Info_UI.text = "REQ. LVL " + levelField;
            }

            OptionCreated = true;
        }

        expanded.SetActive(false);
    }

    public void LoadLevel()
    {
        Settings.SetSelectedLevel(levelField, difficultyField);
        SceneManager.LoadScene(levelToLoad);
    }
}

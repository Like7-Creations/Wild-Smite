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
    public List<GameObject> bars;
    public float barsInterval;

    int levelField;
    LevelSettings.Difficulty difficultyField;

    public TMP_Text Info_UI;
    public GameObject expanded;

    public Vector3 optionPosition;

    public Button CollapsedButton;
    public Jun_TweenRuntime collapsedTween;
    public Button ExpandedButton;

    [Header("BarColors")]
    public Color easyColor;
    public Color mediumColor;
    public Color hardColor;

    public void CreateOption()
    {
        List<PlayerConfig> players = new List<PlayerConfig>();
        if (PlayerConfigManager.Instance != null)
            players = PlayerConfigManager.Instance.GetPlayerConfigs();
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

            int dif = 0;

            if (Info_UI != null)
            {
                for (int i = 0; i <= (int)difficultyField; i++)
                {
                    GameObject obj = Instantiate(DifFilled, DifHolder);
                    bars.Add(obj);
                    dif++;

                    if (dif == 1)
                        obj.GetComponent<Image>().color = easyColor;
                    else if (dif == 2)
                        obj.GetComponent<Image>().color = mediumColor;
                    else if (dif == 3)
                        obj.GetComponent<Image>().color = hardColor;

                }
                for (int i = 2; i > (int)difficultyField; i--)
                {
                    GameObject obj = Instantiate(DifBase, DifHolder);
                    bars.Add(obj);

                    if (dif == 1)
                        obj.GetComponent<Image>().color = easyColor;
                    else if (dif == 2)
                        obj.GetComponent<Image>().color = mediumColor;
                    else if (dif == 3)
                        obj.GetComponent<Image>().color = hardColor;
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

    public void StartTween()
    {
        collapsedTween.Play();
    }

    public void BarsTween()
    {
        StartCoroutine(BarsSequence());
    }

    IEnumerator BarsSequence()
    {
        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].GetComponent<Jun_TweenRuntime>().Play();
            yield return new WaitForSeconds(barsInterval);
        }
    }

    public void SelectOption()
    {
        expanded.SetActive(true);
        ExpandedButton.Select();
    }
}

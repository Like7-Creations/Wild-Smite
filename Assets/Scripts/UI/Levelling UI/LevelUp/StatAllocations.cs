using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatAllocations : MonoBehaviour
{
    [Header("General")]
    public RawImage render;
    public TMP_Text StatPointCount_Text;
    public Jun_TweenRuntime flashTween;
    int StatPoints;
    int startingPoints;
    public Leveling_Data data;
    public PlayerStat_Data player;
    public GameObject AllocatePanel;
    public GameObject StatPanel;
    public GameObject statBarPrefab;

    [Header("PlayerInfo")]
    public Toggle player1_Toggle;
    public Toggle player2_Toggle;
    public TMP_Text playerCharacter;

    [Header("Health")]
    public TMP_Text CurrentHP_Text;
    public TMP_Text NewHP_Text;
    public Transform HP_Bars;

    int baseHP;
    int currentHP;
    int newHP;

    [Header("Stamina")]
    public TMP_Text CurrentSTAM_Text;
    public TMP_Text NewSTAM_Text;
    public Transform STAM_Bars;

    int baseSTAM;
    int currentSTAM;
    int newSTAM;

    [Header("M_Attack")]
    public TMP_Text CurrentMATK_Text;
    public TMP_Text NewMATK_Text;
    public Transform MATK_Bars;

    int baseMATK;
    int currentMATK;
    int newMATK;

    [Header("R_Attack")]
    public TMP_Text CurrentRATK_Text;
    public TMP_Text NewRATK_Text;
    public Transform RATK_Bars;

    int baseRATK;
    int currentRATK;
    int newRATK;

    public void GiveRenderTexture(RawImage tex)
    {
        render.texture = tex.texture;
    }

    public void BeginAllocations(PlayerStat_Data p)
    {
        ResetLevellingPanels();

        player = p;
        if (player.config.PlayerIndex == 0)
            player1_Toggle.isOn = true;
        else if (player.config.PlayerIndex == 1)
            player2_Toggle.isOn = true;
        playerCharacter.text = player.config.Name;

        StatPoints = player.Stat_Points;
        StatPointCount_Text.text = "" + StatPoints;
        startingPoints = StatPoints;

        currentHP = player.hp;
        CurrentHP_Text.text = "" + currentHP;
        baseHP = data.HP_Increment;
        newHP = data.HP_Increment;
        NewHP_Text.text = "" + newHP;

        currentSTAM = player.stamina;
        CurrentSTAM_Text.text = "" + currentSTAM;
        baseSTAM = data.STAM_Increment;
        newSTAM = data.STAM_Increment;
        NewSTAM_Text.text = "" + newSTAM;

        currentMATK = player.m_ATK;
        CurrentMATK_Text.text = "" + currentMATK;
        baseMATK = data.M_ATK_Increment;
        newMATK = data.M_ATK_Increment;
        NewMATK_Text.text = "" + newMATK;

        currentRATK = player.r_ATK;
        CurrentRATK_Text.text = "" + currentRATK;
        baseRATK = data.R_ATK_Increment;
        newRATK = data.R_ATK_Increment;
        NewRATK_Text.text = "" + newRATK;
    }

    public void AllocatePoints()
    {
        if (StatPoints == 0)
        {
            int[] stats = new int[4];
            stats[0] = newHP;
            stats[1] = newSTAM;
            stats[2] = newMATK;
            stats[3] = newRATK;

            player.AllocateStatPoints(stats);
            StatPanel.SetActive(true);
            StatPanel.GetComponent<StatsScreen>().ShowStats(player);
            AllocatePanel.SetActive(false);
        }
        else
            flashTween.Play();
    }

    public void AddBar(Transform root)
    {
        Instantiate(statBarPrefab, root);
    }
    public void RemoveBar(Transform root)
    {
        int index = root.childCount - 1;
        Destroy(root.GetChild(index).gameObject);
    }

    public void ResetLevellingPanels()
    {
        StatPanel.SetActive(true);
        StatPanel.GetComponent<StatsScreen>().ResetBars();
        StatPanel.SetActive(false);

        for (int i = HP_Bars.childCount-1; i > 0; i--)
            Destroy(HP_Bars.GetChild(i).gameObject);
        for (int i = STAM_Bars.childCount-1; i > 0; i--)
            Destroy(STAM_Bars.GetChild(i).gameObject);
        for (int i = MATK_Bars.childCount-1; i > 0; i--)
            Destroy(MATK_Bars.GetChild(i).gameObject);
        for (int i = RATK_Bars.childCount-1; i > 0; i--)
            Destroy(RATK_Bars.GetChild(i).gameObject);

    }

    public void IncreaseHP()
    {
        if (StatPoints > 0)
        {
            newHP += data.hp_Conversion;
            NewHP_Text.text = "" + newHP;

            StatPoints--;
            StatPointCount_Text.text = "" + StatPoints;
            AddBar(HP_Bars);
        }
    }
    public void DecreaseHP()
    {
        if (StatPoints < startingPoints && newHP - data.hp_Conversion >= baseHP)
        {
            newHP -= data.hp_Conversion;
            NewHP_Text.text = "" + newHP;

            StatPoints++;
            StatPointCount_Text.text = "" + StatPoints;
            RemoveBar(HP_Bars);
        }
    }

    public void IncreaseSTAM()
    {
        if (StatPoints > 0)
        {
            newSTAM += data.stamina_Conversion;
            NewSTAM_Text.text = "" + newSTAM;

            StatPoints--;
            StatPointCount_Text.text = "" + StatPoints;
            AddBar(STAM_Bars);
        }
    }
    public void DecreaseSTAM()
    {
        if (StatPoints < startingPoints && newSTAM - data.stamina_Conversion >= baseSTAM)
        {
            newSTAM -= data.stamina_Conversion;
            NewSTAM_Text.text = "" + newSTAM;

            StatPoints++;
            StatPointCount_Text.text = "" + StatPoints;
            RemoveBar(STAM_Bars);
        }
    }

    public void IncreaseMATK()
    {
        if (StatPoints > 0)
        {
            newMATK += data.mATK_Conversion;
            NewMATK_Text.text = "" + newMATK;

            StatPoints--;
            StatPointCount_Text.text = "" + StatPoints;
            AddBar(MATK_Bars);
        }
    }
    public void DecreaseMATK()
    {
        if (StatPoints < startingPoints && newMATK - data.mATK_Conversion >= baseMATK)
        {
            newMATK -= data.mATK_Conversion;
            NewMATK_Text.text = "" + newMATK;

            StatPoints++;
            StatPointCount_Text.text = "" + StatPoints;
            RemoveBar(MATK_Bars);
        }
    }

    public void IncreaseRATK()
    {
        if (StatPoints > 0)
        {
            newRATK += data.rATK_Conversion;
            NewRATK_Text.text = "" + newRATK;

            StatPoints--;
            StatPointCount_Text.text = "" + StatPoints;
            AddBar(RATK_Bars);
        }
    }
    public void DecreaseRATK()
    {
        if (StatPoints < startingPoints && newRATK - data.rATK_Conversion >= baseRATK)
        {
            newRATK -= data.rATK_Conversion;
            NewRATK_Text.text = "" + newRATK;

            StatPoints++;
            StatPointCount_Text.text = "" + StatPoints;
            RemoveBar(RATK_Bars);
        }
    }
}

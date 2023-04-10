using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serielization;
using TMPro;

public static class SaveLoadTest
{
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("saveddd");
            Save();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("loaded");
            Load();
        }
    }*/

    public static void SavePlayerData(List<PlayerConfig> data)
    {
        if (data.Count == 1)
        {
            SaveLoadSystem.BeginSave("/player.data");
            PlayerData pData = new PlayerData(data[0].playerStats);
            SaveData save = new SaveData(pData);
            SaveLoadSystem.Insert(save);
            //SaveLoadSystem.Insert(lolTwo);
            SaveLoadSystem.EndSave();
        }
        else if (data.Count == 2)
        {
            SaveLoadSystem.BeginSave("/player.data");

            PlayerData p1Data = new PlayerData(data[0].playerStats);
            PlayerData p2Data = new PlayerData(data[1].playerStats);

            SaveData save = new SaveData(p1Data, p2Data);
            SaveLoadSystem.Insert(save);
            //SaveLoadSystem.Insert(lolTwo);
            SaveLoadSystem.EndSave();
        }


    }

    public static void LoadPlayerData(List<PlayerConfig> data)
    {
        SaveLoadSystem.BeginLoad("/player.data");

        SaveData save = SaveLoadSystem.Load<SaveData>();
        
        if (save.playerCount == 1)
        {
            data[0].playerStats.LoadStats(save.player1_Data.hp, save.player1_Data.stamina, save.player1_Data.melee, save.player1_Data.range, save.player1_Data.index, save.player1_Data.currentXP, save.player1_Data.level);
        }
        else if (save.playerCount == 2)
        {
            data[0].playerStats.LoadStats(save.player1_Data.hp, save.player1_Data.stamina, save.player1_Data.melee, save.player1_Data.range, save.player1_Data.index, save.player1_Data.currentXP, save.player1_Data.level);
            data[1].playerStats.LoadStats(save.player2_Data.hp, save.player2_Data.stamina, save.player2_Data.melee, save.player2_Data.range, save.player2_Data.index, save.player2_Data.currentXP, save.player2_Data.level);
        }

        SaveLoadSystem.EndLoad();
    }
}

[System.Serializable]
public class PlayerData
{
    public int hp;
    public int stamina;
    public int melee;
    public int range;
    public int index;
    public int currentXP;
    public int level;

    public PlayerData(PlayerStat_Data data)
    {
        this.hp = data.hp;
        this.stamina = data.stamina;
        this.melee = data.m_ATK;
        this.range = data.r_ATK;
        this.index = data.playerIndex;
        this.currentXP = data.current_XP;
        this.level = data.lvl;
    }
}

[System.Serializable]
public class SaveData
{
    public PlayerData player1_Data;
    public PlayerData player2_Data;

    public int playerCount;

    public SaveData(PlayerData p1)
    {
        player1_Data = p1;

        playerCount = 1;
    }

    public SaveData(PlayerData p1, PlayerData p2)
    {
        player1_Data = p1;
        player2_Data = p2;

        playerCount = 2;
    }
}
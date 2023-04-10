using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serielization;
using TMPro;

public class SaveLoadTest : MonoBehaviour
{
    public string lol;
    public string lolTwo;
    public TextMeshProUGUI lolly;
    public TextMeshProUGUI lollyTwo;

    public PlayerStat_Data dataToSave;
    public PlayerStat_Data dataToLoad;

    void Start()
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
    }

    void Save()
    {
        SaveLoadSystem.BeginSave("/player.data");
        PlayerData playerdata = new PlayerData(dataToSave.hp, dataToSave.stamina, dataToSave.m_ATK, dataToSave.r_ATK, dataToSave.playerIndex, dataToSave.current_XP, dataToSave.lvl);
        SaveLoadSystem.Insert(playerdata);
        //SaveLoadSystem.Insert(lolTwo);
        SaveLoadSystem.EndSave();
    }

    void Load()
    {
        SaveLoadSystem.BeginLoad("/player.data");
        PlayerData playerdata = SaveLoadSystem.Load<PlayerData>();
        dataToLoad.LoadStats(playerdata.hp, playerdata.stamina, playerdata.melee, playerdata.range, playerdata.index, playerdata.currentXP, playerdata.level);
        //lollyTwo.text = SaveLoadSystem.Load<string>();
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

    public PlayerData(int hp, int stamina, int melee, int range, int index, int currentXP, int level)
    {
        this.hp = hp;
        this.stamina = stamina;
        this.melee = melee;
        this.range = range;
        this.index = index;
        this.currentXP = currentXP;
        this.level = level;
    }
}

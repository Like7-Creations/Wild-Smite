using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serielization;
using TMPro;
using UnityEngine.UI;

public class SaveLoadTest : MonoBehaviour
{
    public string slot;
    public string saveName;

    public PlayerStat_Data dataToSave;
    public PlayerStat_Data dataToLoad;

    public Button save1;
    public Button save2;
    public Button save3;

    void Start()
    {
        LoadSlots();
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("loaded");
            LoadSlots();
        }
    }

    void Save()
    {
        SaveLoadSystem.BeginSave(slot);
        SaveLoadSystem.Insert(saveName);
        PlayerData playerdata = new PlayerData(dataToSave.hp, dataToSave.stamina, dataToSave.m_ATK, dataToSave.r_ATK, dataToSave.playerIndex, dataToSave.current_XP, dataToSave.lvl);
        SaveLoadSystem.Insert(playerdata);
        SaveLoadSystem.EndSave();
    }

    void Load()
    {
        SaveLoadSystem.BeginLoad(slot);
        PlayerData playerdata = SaveLoadSystem.Load<PlayerData>();
        dataToLoad.LoadStats(playerdata.hp, playerdata.stamina, playerdata.melee, playerdata.range, playerdata.index, playerdata.currentXP, playerdata.level);
        SaveLoadSystem.EndLoad();
    }

    public void LoadSlots()
    {
        SaveLoadSystem.BeginLoad("/SlotOne.data");
        save1.GetComponentInChildren<TextMeshProUGUI>().text = SaveLoadSystem.Load<string>();
        SaveLoadSystem.EndLoad();

        SaveLoadSystem.BeginLoad("/SlotTwo.data");
        save2.GetComponentInChildren<TextMeshProUGUI>().text = SaveLoadSystem.Load<string>();
        SaveLoadSystem.EndLoad();

        SaveLoadSystem.BeginLoad("/SlotThree.data");
        save3.GetComponentInChildren<TextMeshProUGUI>().text = SaveLoadSystem.Load<string>();
        SaveLoadSystem.EndLoad();

        if(save1.GetComponentInChildren<TextMeshProUGUI>().text == null)
        {
            save1.GetComponentInChildren<TextMeshProUGUI>().text = "No Save Slot";
        }
        if (save2.GetComponentInChildren<TextMeshProUGUI>().text == null)
        {
            save2.GetComponentInChildren<TextMeshProUGUI>().text = "No Save Slot";
        }
        if (save3.GetComponentInChildren<TextMeshProUGUI>().text == null)
        {
            save3.GetComponentInChildren<TextMeshProUGUI>().text = "No Save Slot";
        }
    }

    public void SlotOne()
    {
        slot = "/SlotOne.data";
        saveName = "farhans's Save";
    }
    public void SlotTwo()
    {
        slot = "/SlotTwo.data";
        saveName = "chris's Save";
    }
    public void SlotThree()
    {
        slot = "/SlotThree.data";
        saveName = "anmar's Save";
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

[System.Serializable]
public class SaveSlot
{
    string name;
}

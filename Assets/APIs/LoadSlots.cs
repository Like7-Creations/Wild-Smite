using Serielization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LoadSlots : MonoBehaviour
{
    public string slot;
    //public string saveName;

    private string filePath;

    public Button saveButton;
    public TMP_Text saveInfo;
    public TMP_Text saveInfo_P1;
    public TMP_Text saveInfo_P2;

    public Jun_TweenRuntime tweenControl;
    public RectDimensionsControl rectControl;
    public RectDimensionsControl rectControlOutline;

    public bool DebugKeys;
    bool hasData;
    bool newGame;
    int players;

    void Awake()
    {
        saveButton = GetComponent<Button>();
        filePath = $"/slot{slot}.save";
        LoadSaveInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugKeys)
        {

            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("saved");
                Save();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("loaded");
                Load();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Debug.Log("loaded slots");
                LoadSaveInfo();
            }
        }
    }

    public void ResetTweens()
    {
        rectControl.ResetTween();
        rectControlOutline.ResetTween();
    }

    void Save()
    {
        /// SaveLoadSystem.BeginSave("/player.data");
        SaveLoadSystem.BeginSave(filePath);
        SaveData save = PlayerConfigManager.Instance.CreateSave();
        SaveLoadSystem.Insert(save);
        //SaveLoadSystem.Insert(lolTwo);
        SaveLoadSystem.EndSave();
    }
    public void SelectCurrentSlot()
    {
        PlayerConfigManager.Instance.SetJoinState(true);
        PlayerConfigManager.Instance.SetGameState(newGame);
        PlayerConfigManager.Instance.SetMaxPlayers(players);
        PlayerConfigManager.Instance.SetCurrentSlot(filePath);
    }

    public void SetPlayers(int count)
    {
        if (newGame)
            players = count;
    }

    public void SetMode(bool game)
    {
        newGame = game;
    }

    public void Load()
    {
        SaveLoadSystem.BeginLoad(filePath);
        bool SaveFileExists = SaveLoadSystem.checkLoad();

        if (SaveFileExists)
        {
            Debug.Log("loaded data");
            SaveLoadSystem.EndLoad();
        }
        else
        {
            SaveLoadSystem.EndLoad();
            Debug.LogError("Your creating a new Save File");
        }
        // load the new game scene

    }

    public void LoadSaveInfo()
    {
        SaveLoadSystem.BeginLoad(filePath);
        bool slotExist = SaveLoadSystem.checkLoad();
 
        if (slotExist)
        {
            hasData = true;
            SaveData savedata = SaveLoadSystem.Load<SaveData>();
            players = savedata.playerCount;
            if (savedata.playerCount == 1)
            {
                saveInfo.text = "Solo";
                // Set Save Info Text To Be Solo
                saveInfo_P1.text = "P1 : LVL" + savedata.player1_Data.level;
                saveInfo_P2.GetComponent<RectTransform>().sizeDelta = new Vector2(saveInfo_P2.GetComponent<RectTransform>().sizeDelta.x, 0);
                saveInfo_P2.text = "";
                // Player one Text is equal to player one level
                // Player two text i empty
                SaveLoadSystem.EndLoad();
            }
            else if (savedata.playerCount == 2)
            {
                saveInfo.text = "Duo";
                saveInfo_P1.text = "P1 : LVL" + savedata.player1_Data.level;
                saveInfo_P2.text = "P2 : LVL" + savedata.player2_Data.level;
                // Set Save Info Text To Be Duo
                // Player one Text is equal to player one level
                // Player two text is is equal to player two level
                SaveLoadSystem.EndLoad();
            }
        }
        else
        {
            saveInfo.text = "Empty Save";
            saveInfo_P1.text = "";
            saveInfo_P2.text = "";
        }

        if (!newGame && !hasData)
            DisableSlot(true);
        else
            DisableSlot(false);
    }

    public void DisableSlot(bool disable)
    {
        if (disable)
        {
            saveButton.enabled = false;
            saveInfo.color = Color.gray;
        }
        else
        {
            saveButton.enabled = true;
            saveInfo.color = Color.black;
        }
    }

    public void SlotOne()
    {
        slot = "/SlotOne.data";
    }
    public void SlotTwo()
    {
        slot = "/SlotTwo.data";
    }
    public void SlotThree()
    {
        slot = "/SlotThree.data";
    }

    public void ClickedOnASlot()
    {

    }
}

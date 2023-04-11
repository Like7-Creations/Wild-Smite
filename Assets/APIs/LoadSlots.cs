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

    void Start()
    {
        saveButton = GetComponent<Button>();
        filePath = $"/slot{slot}.save";
        //LoadS();
    }

    // Update is called once per frame
    void Update()
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
            LoadS();
        }
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

    public void Load()
    {
        SaveLoadSystem.BeginLoad(filePath);
        bool SaveFileExists = SaveLoadSystem.checkLoad();

        if (SaveFileExists)
        {
            Debug.Log("loaded data");
            SaveLoadSystem.EndLoad();
        }
        else Debug.LogError("Your creating a new Save File");
        // load the new game scene

    }


    public void LoadS()
    {
        SaveLoadSystem.BeginLoad(filePath);
        bool slotOneExist = SaveLoadSystem.checkLoad();
        if(slotOneExist) 
        {
            SaveData savedata = SaveLoadSystem.Load<SaveData>();
            if(savedata.playerCount == 1) 
            {
                saveButton.GetComponentInChildren<TextMeshProUGUI>().text = "Your Solo Save";
                // Set Save Info Text To Be Solo
                saveInfo_P1.text = "P1 : "+ savedata.player1_Data.level;
                saveInfo_P2.text = "";
                // Player one Text is equal to player one level
                // Player two text i empty
                SaveLoadSystem.EndLoad();
            }
            else if(savedata.playerCount == 2) 
            {
                saveButton.GetComponentInChildren<TextMeshProUGUI>().text = "Your Duo";
                saveInfo_P1.text = "P1 : " + savedata.player1_Data.level;
                saveInfo_P2.text = "P2 : " + savedata.player2_Data.level;
                // Set Save Info Text To Be Duo
                // Player one Text is equal to player one level
                // Player two text is is equal to player two level
                SaveLoadSystem.EndLoad();
            }
        }
        //else save1.GetComponentInChildren<TextMeshProUGUI>().text = "Create New";

        /*SaveLoadSystem.BeginLoad("/SlotTwo.data");
        bool slotTwoExist = SaveLoadSystem.checkLoad();
        if (slotTwoExist)
        {
            save2.GetComponentInChildren<TextMeshProUGUI>().text = "Your Second Save";
            SaveLoadSystem.EndLoad();
        }
        else save2.GetComponentInChildren<TextMeshProUGUI>().text = "Create New";

        SaveLoadSystem.BeginLoad("/SlotThree.data");
        bool slotThreeExist = SaveLoadSystem.checkLoad();
        if (slotThreeExist)
        {
            save3.GetComponentInChildren<TextMeshProUGUI>().text = "Your Third Save";
            SaveLoadSystem.EndLoad();
        }
        else save3.GetComponentInChildren<TextMeshProUGUI>().text = "Create New";*/
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

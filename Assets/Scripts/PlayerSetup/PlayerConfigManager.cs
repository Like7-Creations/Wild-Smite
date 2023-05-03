using Serielization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour//, IDataPersistence
{
    [Header("Save Data Stuff")]
    public string saveFileName = "";
    bool newGame;

    [Header("Player Settings")]
    public Leveling_Data levelData;
    public ExperienceData experienceData;

    List<PlayerConfig> playerConfigs;

    [SerializeField]
    int MaxPlayers = 2;
    bool canJoin;

    [Header("LoadToSceneConfig")]
    public string SceneToLoad;
    public bool loadScene;

    [Header("TestConfig")]
    bool spawnedPlayers;
    public bool joinonStart;
    public InitialiseLevel levelStart;

    [Header("CharacterDefaults")]
    public GameObject defaultCharacter;
    public Material defaultMaterial;

    bool isPaused;

    public static PlayerConfigManager Instance { get; private set; }

    private void OnEnable()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Singleton - Trying to create another instance");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfig>();
            if (joinonStart)
            {
                canJoin = true;
                GetComponent<PlayerInputManager>().EnableJoining();
            }
            else
            {
                canJoin = false;
                GetComponent<PlayerInputManager>().DisableJoining();
            }
        }
    }

    /*public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
        if(scene.buildIndex == 1)
        {
            DataPersistenceManager.instance.LoadGame();

            Debug.Log("Loading Data for Homebase");
        }
    }*/

    private void Update()
    {
        if (!loadScene && !spawnedPlayers)
        {
            if (MaxPlayers == playerConfigs.Count)
            {
                levelStart.Initialise();
                spawnedPlayers = true;
            }
        }
    }

    /*public void LoadData(GameData dataToLoad)
    {

    }

    public void SaveData(GameData data)
    {

    }*/

    public void ResetManager(GameObject SelectionPanel)
    {
        for (int i = 0; i < SelectionPanel.transform.childCount; i++)
        {
            Destroy(SelectionPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < playerConfigs.Count; i++)
        {
            Destroy(playerConfigs[i].Input.gameObject);
        }

        playerConfigs.Clear();
        GetComponent<PlayerInputManager>().DisableJoining();
    }

    public void SetMaxPlayers(int num)
    {
        MaxPlayers = num;
        //GetComponent<PlayerInputManager>().maxPlayerCount = num;
    }

    public void SetGameState(bool state)
    {
        newGame = state;
    }

    public void SetJoinState(bool state)
    {
        if (state)
        {
            canJoin = true;
            GetComponent<PlayerInputManager>().EnableJoining();
        }
        else
        {
            canJoin = false;
            GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

    public List<PlayerConfig> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void TogglePausedState(bool state)
    {
        isPaused = state;
        for (int i = 0; i < playerConfigs.Count;i++)
            playerConfigs[i].isPaused = isPaused;
    }

    public void SetPlayerColor(int index, Material color)
    {
        playerConfigs[index].PlayerMat = color;
    }

    public void SetPlayerCharacter(int index, GameObject character)
    {
        playerConfigs[index].Character = character;
    }

    public void ReadyPlayer(int index, bool ready)
    {
        playerConfigs[index].IsReady = ready;

        if (playerConfigs.Count == MaxPlayers)
        {
            bool allReady = true;
            for (int i = 0; i < playerConfigs.Count; i++)
            {
                if (!playerConfigs[i].IsReady)
                {
                    allReady = false;
                    break;
                }
            }

            if (allReady)
            {
                if (loadScene)
                {
                    Debug.Log("All Players Ready");

                    //SaveLoadSystem.BeginLoad(/*"/player.data"*/saveFileName);
                    //bool loading = SaveLoadSystem.checkLoad();
                    //SaveLoadSystem.EndLoad();*/

                    if (!newGame)
                    {
                        Debug.Log("Data Found. Loading To Player");
                        SaveLoadTest.LoadPlayerData(playerConfigs, saveFileName);
                        SaveLoadTest.SavePlayerData(playerConfigs, saveFileName);
                    }
                    else
                    {
                        SaveLoadTest.SavePlayerData(playerConfigs, saveFileName);
                    }

                    SceneManager.LoadScene(SceneToLoad);
                }
            }
        }
    }

    public void HandlePlayerJoin(PlayerInput pInput)
    {
        if (playerConfigs.Count < MaxPlayers && canJoin)
        {
            Debug.Log("Player joined" + pInput.playerIndex);
            if (!playerConfigs.Any(p => p.PlayerIndex == pInput.playerIndex))
            {
                pInput.transform.SetParent(transform);
                PlayerConfig p = new PlayerConfig(pInput, experienceData, levelData);

                /*if (!DataPersistenceManager.instance.firstInstance)
                {
                    DataPersistenceManager.instance.LoadGame();

                    List<PlayerConfig> tempData = DataPersistenceManager.instance.gameData.pConfigs;

                    for (int i = 0; i < tempData.Count; i++)
                    {
                        if (tempData[i].PlayerIndex == p.PlayerIndex)
                        {
                            p.playerStats.reInit(tempData[i].playerStats);
                        }
                    }
                }*/

                p.Character = defaultCharacter;
                p.PlayerMat = defaultMaterial;
                playerConfigs.Add(p);
            }

            if (playerConfigs.Count == MaxPlayers)
                GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

    public void SetCurrentSlot(string filename)
    {
        saveFileName = filename;
    }

    public SaveData CreateSave()
    {
        if (playerConfigs.Count == 1)
        {
            //sami is gay
            PlayerData playerone = new PlayerData(playerConfigs[0].playerStats);
            SaveData savedata = new SaveData(playerone);
            return savedata;
        }
        else if (playerConfigs.Count == 2)
        {
            //sami is gay
            PlayerData playerone = new PlayerData(playerConfigs[0].playerStats);
            PlayerData playerTwo = new PlayerData(playerConfigs[1].playerStats);
            SaveData savedata = new SaveData(playerone, playerTwo);
            return savedata;
        }
        return null;
    }

}

[System.Serializable]
public class PlayerConfig
{
    public string Name;
    public PlayerStat_Data playerStats;
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public bool isPaused { get; set; }

    public Material PlayerMat { get; set; }
    public GameObject Character { get; set; }

    public PlayerConfig(PlayerInput pInput, ExperienceData xpData, Leveling_Data lvlData)
    {
        PlayerIndex = pInput.playerIndex;
        playerStats = ScriptableObject.CreateInstance<PlayerStat_Data>();
        playerStats.init($"Player {PlayerIndex + 1}", PlayerIndex, xpData, lvlData, this);
        Name = $"Player {PlayerIndex + 1}";
        Input = pInput;
    }
}

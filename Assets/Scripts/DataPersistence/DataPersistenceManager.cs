using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor;

#region Summary & Notes
/* This class must exist as a singleton, and attached to its own GameObject in the scene. 
 * No other class related to Data Persistence should exist in the scene itself.
 *
 * Instead of using OnSceneUnloaded, reference the Save function of this instance just before calling a scene change.
 **/
#endregion
public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] bool disableDataPersistence = false;
    [SerializeField] bool initializeDataIfNull = false;     //Set to true if you wanna test data persistence for specific scenes.

    [SerializeField] bool overrideSelectedProfileID = false;
    [SerializeField] string testSelectedProfileID = "test";

    public bool firstInstance;

    [Header("File Storage Config")]
    [SerializeField] string filename;
    [SerializeField] bool useEncryption;
    public static DataPersistenceManager instance { get; private set; }

    public GameData gameData;
    List<IDataPersistence> dataPersistenceObjs;
    FileDataManager dataManager;

    string selectedProfileID = "";

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        //Application.quitting += SaveOnQuit;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        //Application.quitting -= SaveOnQuit;
    }



    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple 'Data Persistence Managers' found in scene. Destrying newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence Is Currently Disabled!");
        }

        this.dataManager = new FileDataManager(Application.persistentDataPath, filename, useEncryption);

        //this.selectedProfileID = dataManager.GetRecentlyUpdatedProfileID();

        //For debugging
        if (overrideSelectedProfileID)
        {
            selectedProfileID = testSelectedProfileID;
            Debug.LogWarning("Selected Profile ID has been overriden by the Test Profile ID: " + testSelectedProfileID);
        }
    }

    static void SaveOnQuit()
    {
        Debug.Log("Quitting PlayMode");

        instance.SaveGame();
    }

    #region NewGame() Summary:
    /* This function should be called only under a specific circumstance:
     * When players create a new game, we directly reference this class' instance, and call the function through it.
     * After calling this function, the "SceneManager.LoadSceneAsync("insert scene here")" function should be called.
     * This in turn will result in this new game being saved, since the current scene will be unloaded, resulting in OnSceneUnloaded(), a subscriber of the SceneUnload, is called. 
     * */
    #endregion
    public void NewGame()
    {
        //Create a new Game Data object when starting a new game.
        //this.gameData = new GameData();

        firstInstance = true;

        Debug.Log("New Game Data is being created");
        Debug.Log("Please load the Homebase scene to create a new save");
    }

    public void SaveGame()
    {
        #region Debugging Section
        //Return if disableDataPersistence is enabled
        if (disableDataPersistence)
        {
            return;
        }
        #endregion

        Debug.Log("Saving Game");

        //If there is no save data, send out a warnign.
        if (this.gameData == null)
        {
            Debug.LogWarning("No data found. Please start a new game, before trying to save data.");
            return;
        }

        //Pass the GameData to any scripts so that they can update it with their own information.
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjs)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        Debug.Log("Game Data Loaded In.");
        //Debug.Log($"The Saved Player Stats are - HP: {gameData.hp}, STAM: {gameData.stamina}, MELEE: {gameData.m_ATK}, RANGE: {gameData.r_ATK}");

        //Update the timestamp of the saved game.
        gameData.lastUpdatedStamp = System.DateTime.Now.ToBinary();

        //Save that data to a file using a data handler.
        dataManager.Save(gameData, selectedProfileID);
    }

    #region LoadGame() Summary:
    /* This function should only be called when continuing an existing game:
     * As such, it will first Load the next scene.
     * This results in OnSceneLoaded being called from this class, due to it being a subscriber of SceneLoaded.
     * Lastly the scene is then loaded using "SceneManager.LoadSceneAsync("insert scene here")".
     * 
     * */
    #endregion
    public void LoadGame()
    {
        #region Debugging Section
        //Return if disableDataPersistence is enabled
        if (disableDataPersistence)
        {
            return;
        }
        #endregion

        //Load any saved data from a file using a data handler.
        this.gameData = dataManager.Load(selectedProfileID);

        //Start a new game  if the data is null && we're configured to initialize data for debugging purposes.
        if (this.gameData == null && initializeDataIfNull)
        {
            //NewGame();
            Debug.Log("No data to load. Starting new game.");
        }

        //If there is no data to load, don't continue.
        if (instance.gameData == null)
        {
            Debug.Log("No Saves Located. Please start a new game.");

            //firstInstance = true;

            return;
        }

        //Push all the loaded data to all scripts that need it.
        //In our case, its just the PlayerStat_Data script.
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjs)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Game Data Loaded In.");
        Debug.Log($"The Loaded Player Stats are - HP: {gameData.playerData[0].hp}, STAM: {gameData.playerData[0].stamina}, MELEE: {gameData.playerData[0].m_ATK}, RANGE: {gameData.playerData[0].r_ATK}");
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Debug.Log("OnSceneLoaded called for Scene: " + scene.buildIndex);
            this.dataPersistenceObjs = FindAllDataPersistenceObjects();
            LoadGame();

            /*List<PlayerConfig> configs = PlayerConfigManager.Instance.GetPlayerConfigs();

            for (int i = 0; i < configs.Count; i++)
            {
                 configs[i].playerStats = 
            }*/
        }
        else if (scene.buildIndex == 0)
        {
            Debug.Log("OnSceneLoaded called for Scene: " + scene.buildIndex);
        }
    }


    List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //Default Code
        //IEnumerable<IDataPersistence> dataPersistenceObjs = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        //This code is designed to work with monobehaviours. But we use Scriptable Objects to store data. 
        //So Im using Resources.FindObjectsOfTypeAll to locate ay scriptableObj that is connected to the IDataPersistence interface.
        //Basically, what Im saying is I have no idea if this is gonna work.

        //IEnumerable<IDataPersistence> dataPersistenceObjs = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<IDataPersistence>();

        //Might need to rewrite this formula, so that it can properly track data. Might just have this link to PlayerStats instead of the scriptable object.

        IEnumerable<IDataPersistence> dataPersistenceObjs = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjs);
    }

    #region HasGameData() Summarry:
    //Can be used to disable the continue game button in the mainmenu if there is no save game
    #endregion
    public bool HasGameData()
    {
        return this.gameData != null;
    }

    /*public void ChangeSelectedProfileID(string newProfileID)
    {
        //Update the profile to use for saving and loading
        this.selectedProfileID = newProfileID;

        //Load the game saved on this profile, and use its data accordingly.
        LoadGame();
    }*/

    /*public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataManager.LoadAllProfiles();
    }*/

    private void OnApplicationQuit()
    {
        //Call Save func here??
    }
}

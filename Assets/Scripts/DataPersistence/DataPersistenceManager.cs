using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


#region Summary & Notes
//This class must exist as a singleton, and attached to its own GameObject in the scene. 
//No other class related to Data Persistence should exist in the scene itself.
#endregion
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] string filename;
    [SerializeField] bool useEncryption;
    public static DataPersistenceManager instance { get; private set; }

    GameData gameData;
    List<IDataPersistence> dataPersistenceObjs;
    FileDataManager dataManager;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple 'Data Persistence Managers' found in scene");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataManager = new FileDataManager(Application.persistentDataPath, filename, useEncryption);
        this.dataPersistenceObjs = FindAllDataPersistenceObjects();
    }

    public void NewGame()
    {
        //Create a new Game Data object when starting a new game.
        this.gameData = new GameData();
    }

    public void SaveGame()
    {
        //Pass the GameData to any scripts so that they can update it with their own information.
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjs)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        Debug.Log("Game Data Loaded In.");
        Debug.Log($"The Saved Player Stats are - HP: {gameData.hp}, STAM: {gameData.stamina}, MELEE: {gameData.m_ATK}, RANGE: {gameData.r_ATK}");

        //Save that data to a file using a data handler.
        dataManager.Save(gameData);
    }

    public void LoadGame()
    {
        //Load any saved data from a file using a data handler.
        this.gameData = dataManager.Load();

        //If there is no data to load, start a new game.
        if (instance.gameData == null)
        {
            Debug.Log("No Saves Located. Initializing New Game With Default Data.");

            NewGame();
        }

        //Push all the loaded data to all scripts that need it.
        //In our case, its just the PlayerStat_Data script.
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjs)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Game Data Loaded In.");
        Debug.Log($"The Loaded Player Stats are - HP: {gameData.hp}, STAM: {gameData.stamina}, MELEE: {gameData.m_ATK}, RANGE: {gameData.r_ATK}");
    }

    List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //Default Code
        //IEnumerable<IDataPersistence> dataPersistenceObjs = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        //This code is designed to work with monobehaviours. But we use Scriptable Objects to store data. 
        //So Im using Resources.FindObjectsOfTypeAll to locate ay scriptableObj that is connected to the IDataPersistence interface.
        //Basically, what Im saying is I have no idea if this is gonna work.
        IEnumerable<IDataPersistence> dataPersistenceObjs = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjs);
    }
}

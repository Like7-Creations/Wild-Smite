using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseLevel : MonoBehaviour
{

    [SerializeField]
    Transform[] playerSpawns;
    [SerializeField]
    GameObject playerPrefab;

    [Header("Config")]
    public bool initialiseOnStart;
    public bool spawnLevel;
    bool initialised;
    bool levelSpawned;

    [Header("Difficulty Settings")]
    public LevelSettings levelSettings;
    LevelSettings.Difficulty levelDifficulty;

    [Header("Level")]
    public GameObject levelEndObject;
    GameObject levelRoot;

    // Start is called before the first frame update
    void Start()
    {
        initialised = false;

        if (levelSettings != null)
            levelDifficulty = levelSettings.GetDifficulty();

        if (initialiseOnStart)
            Initialise();
    }

    public void Initialise()
    {
        if (!initialised)
        {
            List<PlayerConfig> playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();

            for (int i = 0; i < playerConfigs.Count; i++)
            {
                GameObject player = Instantiate(playerConfigs[i].Character, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerControl>().InitialisePlayer(playerConfigs[i]);
                player.GetComponent<PlayerStats>().SetData(playerConfigs[i].playerStats);

                if(i == 0)
                {
                    player.tag = "Player1";
                }
                else if (i == 1)
                {
                    player.tag = "Player2";
                }

            }
            initialised = true;
        }

        if (spawnLevel && !levelSpawned)
        {
            LevelData levelData = levelSettings.GetSelectedLevel();

            switch (levelDifficulty)
            {
                case LevelSettings.Difficulty.Easy:
                    levelRoot = Instantiate(levelData.EasyLevelGen, transform);
                    break;
                case LevelSettings.Difficulty.Medium:
                    levelRoot = Instantiate(levelData.MediumLevelGen, transform);
                    break;
                case LevelSettings.Difficulty.Hard:
                    levelRoot = Instantiate(levelData.HardLevelGen, transform);
                    break;
            }
            levelSpawned = true;
            StartCoroutine(createEndPoint());
        }
    }

    IEnumerator createEndPoint()
    {
        yield return new WaitForSeconds(.2f);
        LevelGenerator.Scripts.Section[] rooms = levelRoot.GetComponentsInChildren<LevelGenerator.Scripts.Section>();
        int room = Random.Range(1, rooms.Length);
        Instantiate(levelEndObject, levelRoot.transform.GetChild(room).transform.position, Quaternion.identity);
    }
}

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
    public bool levelSpawned;

    [Header("Difficulty Settings")]
    public LevelSettings levelSettings;
    LevelSettings.Difficulty levelDifficulty;

    [Header("Level")]
    public bool RunBarrierAdjustment;
    public float BarrierDistance;
    List<Barrier> testList;
    public bool spawnEndPoint;
    public GameObject levelEndObject;
    [Range(0, 10)]
    public int furthestRooms;
    public List<FurthestR> Furthest;
    GameObject levelRoot;

    public int rain;

    // Start is called before the first frame update
    void Start()
    {
        initialised = false;
        Furthest = new List<FurthestR>();

        if (levelSettings != null)
            levelDifficulty = levelSettings.GetDifficulty();

        if (initialiseOnStart)
            Initialise();

        PlayerActions[] players = GetComponentsInChildren<PlayerActions>();
        rain = Random.Range(0, 2);

        for (int i = 0; i < players.Length; i++)
        {
            if (rain == 0)
            {
                players[i].rain.Play();
            }
            else players[i].rain.Stop();
        }
    }

    public void Initialise()
    {
        if (!initialised)
        {
            PlayerConfigManager.Instance.SetJoinState(false);
            List<PlayerConfig> playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();

            for (int i = 0; i < playerConfigs.Count; i++)
            {
                GameObject player = Instantiate(playerConfigs[i].Character, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerControl>().InitialisePlayer(playerConfigs[i]);
                player.GetComponent<PlayerStats>().SetData(playerConfigs[i].playerStats);


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

            if (spawnEndPoint)
                StartCoroutine(createEndPoint());
        }
    }

    IEnumerator createEndPoint()
    {
        yield return new WaitForSeconds(.2f);

        LevelGenerator.Scripts.Section[] rooms = levelRoot.GetComponentsInChildren<LevelGenerator.Scripts.Section>();

        GameObject startingRoom = null;
        bool closeLoop = false;
        for (int i = 0; i < rooms.Length; i++)
        {
            for (int j = 0; j < rooms[i].Tags.Length; j++)
            {
                if (rooms[i].Tags[j] == "Start")
                {
                    startingRoom = rooms[i].gameObject;
                    closeLoop = true;
                    break;

                }
            }

            if (closeLoop)
                break;
        }

        int range = Mathf.RoundToInt(rooms.Length * .2f);
        List<GameObject> possibleEndRooms = new List<GameObject>();
        
        float prevFurthest = 0;
        for (int i = 0; i < furthestRooms; i++)
        {
            //if ()
            LevelGenerator.Scripts.Section currentFurthest = null;
            float furthestDist = 0;

            for (int j = 0; j < rooms.Length; j++)
            {
                float dist = Vector3.Distance(startingRoom.transform.position, rooms[j].gameObject.transform.position);

                if (i == 0)
                {
                    if (dist > furthestDist)
                    {
                        furthestDist = dist;
                        currentFurthest = rooms[j];
                    }
                }
                else
                {
                    if (dist > furthestDist && dist < prevFurthest)
                    {
                        furthestDist = dist;
                        currentFurthest = rooms[j];
                    }
                }
            }

            possibleEndRooms.Add(currentFurthest.gameObject);
            Furthest.Add(new FurthestR(furthestDist, currentFurthest.gameObject));
            prevFurthest = furthestDist;
        }

        GameObject end = Instantiate(levelEndObject, possibleEndRooms[Random.Range(0, possibleEndRooms.Count)].transform);
        //end.transform.SetParent(transform);

        yield return new WaitForSeconds(1);
        if (RunBarrierAdjustment)
        {
            testList = new List<Barrier>();
            Barrier[] barriers = FindObjectsOfType<Barrier>();
            for (int i = 0; i < barriers.Length; i++)
            {
                barriers[i].name = "Barrier_" + i;
            }

            for (int i = 0; i < barriers.Length - 1; i++)
            {
                for (int j = 1; j < barriers.Length; j++)
                {
                    if (i == j)
                    {
                        Debug.Log($"i = {i} & j = {j} Moving on with Loop");
                        continue;
                    }

                    if (Vector3.Distance(barriers[i].transform.position, barriers[j].transform.position) < BarrierDistance)
                    {
                        Debug.Log($"Position of {barriers[i].name} is: {barriers[i].transform.position} & Position of {barriers[j].name} is: {barriers[j].transform.position}");
                        Debug.Log($"Distance between {barriers[i].name} and {barriers[j].name} is {Vector3.Distance(barriers[i].transform.position, barriers[j].transform.position)}");
                        if (testList.Count > 0)
                        {
                            if (testList.Contains(barriers[i]) == false)
                            {
                                testList.Add(barriers[i]);
                                //barriers[i].transform.position += new Vector3(0, 20, 0);
                            }
                            if (testList.Contains(barriers[j]) == false)
                            {
                                testList.Add(barriers[j]);
                                //barriers[j].transform.position += new Vector3(0, 20, 0);
                            }
                        }
                        else
                        {
                            testList.Add(barriers[i]);
                            //barriers[i].transform.position += new Vector3(0, 20, 0);
                            testList.Add(barriers[j]);
                            //barriers[j].transform.position += new Vector3(0, 20, 0);
                        }
                    }
                }
            }

            for (int i = testList.Count - 1; i > 0; i--)
                Destroy(testList[i].gameObject);
            //testList.Clear();
        }
    }

    [System.Serializable]
    public class FurthestR
    {
        public float distance;
        public GameObject room;

        public FurthestR (float d, GameObject r)
        {
            distance = d;
            room = r;
        }
    }
}

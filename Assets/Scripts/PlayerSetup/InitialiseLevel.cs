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
    bool initialised;

    // Start is called before the first frame update
    void Start()
    {
        initialised = false;

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
                GameObject player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerControl>().InitialisePlayer(playerConfigs[i]);
            }
            initialised = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseLevel : MonoBehaviour
{

    [SerializeField]
    Transform[] playerSpawns;
    [SerializeField]
    GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        List<PlayerConfig> playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs();

        for (int i = 0; i < playerConfigs.Count; i++)
        {
            GameObject player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<TestMovement>().InitialisePlayer(playerConfigs[i]);
        }
    }
}

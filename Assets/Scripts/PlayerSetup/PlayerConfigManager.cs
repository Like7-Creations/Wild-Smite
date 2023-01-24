using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{

    List<PlayerConfig> playerConfigs;

    [SerializeField]
    int MaxPlayers = 2;

    public string SceneToLoad;

    public static PlayerConfigManager Instance { get; private set; }

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
        }
    }

    public List<PlayerConfig> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerColor(int index, Material color)
    {
        playerConfigs[index].PlayerMat = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;

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
                Debug.Log("All Players Ready");
                SceneManager.LoadScene(SceneToLoad);
            }
        }
    }

    public void HandlePlayerJoin(PlayerInput pInput)
    {
        Debug.Log("Player joined" + pInput.playerIndex);

        if (!playerConfigs.Any(p => p.PlayerIndex == pInput.playerIndex))
        {
            pInput.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfig(pInput));
        }
    }
}

public class PlayerConfig
{
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material PlayerMat { get; set; }

    public PlayerConfig(PlayerInput pInput)
    {
        PlayerIndex = pInput.playerIndex;
        Input = pInput;
    }
}

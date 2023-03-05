using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnSetupMenu : MonoBehaviour
{
    public string rootName;
    public GameObject setupMenuPrefab;
    public PlayerInput input;

    private void Awake()
    {
        GameObject rootMenu = GameObject.Find(rootName);

        if (rootMenu != null)
        {
            GameObject menu = Instantiate(setupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupController>().SetPlayerInfo(input.playerIndex, input.devices[0].displayName);
        }
    }
}

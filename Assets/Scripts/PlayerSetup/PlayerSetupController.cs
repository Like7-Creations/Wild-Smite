using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetupController : MonoBehaviour
{

    int PlayerIndex;

    [SerializeField]
    TMP_Text playerText;
    [SerializeField]
    TMP_Text deviceText;
    [SerializeField]
    TMP_Text characterText;

    public GameObject defaultCharacter;
    public Material defaultMaterial;

    //Old Variables
    //[SerializeField]
    //GameObject readyPanel;
    //[SerializeField]
    //GameObject menuPanel;

    float ignoreInputTime = 1.5f;
    bool inputEnabled;

    [SerializeField]
    Button readyButton;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ignoreInputTime)
            inputEnabled = true;
    }

    public void SetPlayerInfo(int pi, string device)
    {
        PlayerIndex = pi;
        playerText.text = "Player " + (pi + 1);
        deviceText.text = device;
        ignoreInputTime = Time.time + ignoreInputTime;
        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, defaultMaterial);
        PlayerConfigManager.Instance.SetPlayerCharacter(PlayerIndex, defaultCharacter);
    }

    public void SetColor(Material color)
    {
        if (!inputEnabled)
            return;

        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, color);
        //readyPanel.SetActive(true);
        //readyButton.Select();
        //menuPanel.SetActive(false);
    }

    public void SetCharacter(GameObject character)
    {
        PlayerConfigManager.Instance.SetPlayerCharacter(PlayerIndex, character);
    }

    public void SetCText(string name)
    {
        characterText.text = name;
    }

    public void ReadyPlayer(bool ready)
    {
        if (!inputEnabled)
            return;

        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex, ready);
        //readyButton.gameObject.SetActive(false);
    }
}

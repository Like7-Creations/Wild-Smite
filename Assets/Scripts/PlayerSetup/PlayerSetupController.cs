using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetupController : MonoBehaviour
{

    int PlayerIndex;

    [SerializeField]
    TMP_Text titleText;
    [SerializeField]
    GameObject readyPanel;
    [SerializeField]
    GameObject menuPanel;

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

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        titleText.text = "Player " + (pi + 1);
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public void SetColor(Material color)
    {
        if (!inputEnabled)
            return;

        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, color);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled)
            return;

        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }
}

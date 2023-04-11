using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSetupController : MonoBehaviour
{
    bool ready;

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

    float ignoreInputTime = .5f;
    bool inputEnabled;

    [SerializeField]
    Toggle readyButton;
    Toggle charAButton;
    Button backButton;

    private void Awake()
    {
        ready = false;
    }

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

    public void ReadyPlayer()
    {
        if (!inputEnabled)
            return;

        if (!ready)
        {
            ready = true;
        }
        else if (ready)
        {
            ready = false;
        }

        readyButton.isOn = ready;
        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex, ready);
        //readyButton.gameObject.SetActive(false);
    }

    //public void CancelPressed()
    //{
    //    if (playerConfig.IsReady)
    //    {
    //        ReadyPlayer(false);
    //        readyButton.onValueChanged.Invoke(true);
    //    }
    //    else
    //    {
    //        selectionControl.ReturnToMainMenu();
    //    }
    //}

    //public void OnInputAction(InputAction.CallbackContext context)
    //{
    //    if (context.action.name == controls.UI.Return.name && context.performed)
    //    {
    //        CancelPressed();
    //    }
    //}
}

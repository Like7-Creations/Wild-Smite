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

    public RawImage charSprite;
    public CharacterInfo Crocodile;
    public CharacterInfo Kangaroo;
    CharacterInfo selectedCharacter;
    [Range(0, 2)]
    int selectedColor;

    //Old Variables
    //[SerializeField]
    //GameObject readyPanel;
    //[SerializeField]
    //GameObject menuPanel;

    float ignoreInputTime = .5f;
    bool inputEnabled;

    [SerializeField]
    Toggle readyButton;
    //[SerializeField]
    //Toggle charAButton;
    //[SerializeField]
    //Toggle charBButton;
    Button backButton;

    private void Awake()
    {
        ready = false;
        //SetCharacter(0);
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
        SetCharacter(0);
        charSprite.texture = selectedCharacter.Char1_Sprite;

        //SetColor(0);
    }

    public void SetColor(int index)
    {
        if (!inputEnabled)
            return;

        switch (index)
        {
            case 0:
                PlayerConfigManager.Instance.SetPlayerCharacter(PlayerIndex, selectedCharacter.Character_1);
                charSprite.texture = selectedCharacter.Char1_Sprite;
                break;
            case 1:
                PlayerConfigManager.Instance.SetPlayerCharacter(PlayerIndex, selectedCharacter.Character_2);
                charSprite.texture = selectedCharacter.Char2_Sprite;
                break;
            case 2:
                PlayerConfigManager.Instance.SetPlayerCharacter(PlayerIndex, selectedCharacter.Character_3);
                charSprite.texture = selectedCharacter.Char3_Sprite;
                break;
        }

        selectedColor = index;

        //readyPanel.SetActive(true);
        //readyButton.Select();
        //menuPanel.SetActive(false);
    }

    public void SetCharacter(int index)
    {
        switch (index)
        {
            case 0:
                selectedCharacter = Crocodile;
                SetColor(selectedColor);
                break;
            case 1:
                selectedCharacter = Kangaroo;
                SetColor(selectedColor);
                break;
        }        
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

[System.Serializable]
public class CharacterInfo
{
    public GameObject Character_1;
    public GameObject Character_2;
    public GameObject Character_3;
    public RenderTexture Char1_Sprite;
    public RenderTexture Char2_Sprite;
    public RenderTexture Char3_Sprite;
}
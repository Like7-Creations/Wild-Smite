using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public float delayTime = 2f;

    [Header("PlayerSelection")]
    public PlayerConfigManager playerConfigManager;
    public GameObject SelectionCanvas;
    public GameObject SelectionPanel;
    public GameObject MenuPanel;

    private void Awake()
    {
        // playerConfigManager = PlayerConfigManager.Instance;
        ResetManager(SelectionPanel);
    }

    public void LoadCharacterSelection(int playerCount)
    {
        PlayerConfigManager.Instance.SetMaxPlayers(playerCount);
        SelectionCanvas.SetActive(true);
    }

    public void ResetManager(GameObject panel)
    {
        PlayerConfigManager.Instance.ResetManager(panel);
        SelectionCanvas.SetActive(false);
    }

    public void ToggleMenuState()
    {
        if (MenuPanel.active)
            MenuPanel.SetActive(false);
        else
            MenuPanel.SetActive(true);
    }

    public void SetPlayerJoins(bool state)
    {
        PlayerConfigManager.Instance.SetJoinState(state);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        //Vector3 pos = Button_QuitGame.transform.position;
        //pos.x -= 30f;
        //Button_QuitGame.transform.position = pos;

        //backsound.Play();
        Application.Quit();
        Invoke("DelayQuitGame", delayTime);
    }
}

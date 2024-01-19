using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    //public string menuScene;
    public GameObject pauseMenu;

    PlayerInput input;
    public bool paused;

    void Awake()
    {
        paused = false;
        PlayerConfigManager.Instance.TogglePausedState(paused);
    }

    public void GamePause(PlayerConfig pc)
    {
        if (!paused)
        {
            Debug.Log("Game has been paused");

            Time.timeScale = 0;
            paused = true;
            PlayerConfigManager.Instance.TogglePausedState(paused);

            pauseMenu.SetActive(true);
        }
    }

    public void GameResume()
    {
        if (paused)
        {
            Debug.Log("Game has been resumed");

            Time.timeScale = 1f;
            paused = false;
            PlayerConfigManager.Instance.TogglePausedState(paused);

            pauseMenu.SetActive(false);
        }
    }

    public void GameForfeit()
    {
        Time.timeScale = 1f;

        paused = false;
        PlayerConfigManager.Instance.TogglePausedState(paused);
        //SceneManager.LoadScene(menuScene);
    }
}

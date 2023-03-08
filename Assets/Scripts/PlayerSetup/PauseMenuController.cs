using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using TMPro;

public class PauseMenuController : MonoBehaviour
{

    public GameObject PauseMenuPrefab;
    GameObject pauseMenu;

    PlayerInput input;
    int currentPlayerIndex;

    TMP_Text playerText;



    bool IsPaused;

    private void Awake()
    {
        IsPaused = false;
        currentPlayerIndex = -1;
    }

    public void PauseGame(PlayerConfig pc)
    {
        if (currentPlayerIndex == -1)
            currentPlayerIndex = pc.PlayerIndex;

        if (pc.PlayerIndex == currentPlayerIndex)
        {
            if (!IsPaused)
            {
                Debug.Log($"Player {pc.PlayerIndex + 1} paused the game");
                pauseMenu = Instantiate(PauseMenuPrefab, transform);

                input = pc.Input;
                input.uiInputModule = pauseMenu.GetComponentInChildren<InputSystemUIInputModule>();
                GameObject textholder = GameObject.Find("PlayerContext");
                SetPlayerText(textholder.GetComponent<TMP_Text>(), pc.PlayerIndex);

                Time.timeScale = 0;
                IsPaused = true;
            }
            else
            {
                Debug.Log($"Player {pc.PlayerIndex + 1} resumed the game");
                input.uiInputModule = null;
                input = null;

                Time.timeScale = 1;
                Destroy(pauseMenu);
                IsPaused = false;
                currentPlayerIndex = -1;
            }
        }
    }

    public void ResumeGame()
    {        
        input.uiInputModule = null;
        input = null;

        Time.timeScale = 1;
        Destroy(pauseMenu);
        IsPaused = false;
        currentPlayerIndex = -1;
    }

    void SetPlayerText(TMP_Text text, int index)
    {
        text.text = "Player " + (index + 1);
    }
}

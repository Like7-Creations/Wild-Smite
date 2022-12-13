using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] PauseController[] pauseMenus;
    [SerializeField] GameObject PauseUI;
    
    public static bool IsPaused;


    void Start()
    {
        pauseMenus = new PauseController[2];

        pauseMenus = FindObjectsOfType<PauseController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Attempting To Pause");

            for (int i = 0; i < pauseMenus.Length; i++)
            {
                if (pauseMenus[i] != this)
                {
                    Debug.Log("Game is Already Paused");
                    IsPaused = false;
                    PauseUI.SetActive(false);
                }

                else if (pauseMenus[i] == this)
                {
                    if (IsPaused)
                        ResumeGame();
                    else
                        PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
        Debug.Log("Resuming Game");
    }

    public void ExitToMainMenu()
    {
        Debug.Log("Exiting To Main Menu...");
    }

    public void OptionsMenu()
    {
        Debug.Log("Loading Options Menu...");
    }

    void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
        Debug.Log("Pausing Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;

    public AudioSource pausesound;
    public AudioSource selectsound;
    public AudioSource quitsound;
    public AudioSource resumesound;


    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                Debug.Log("Unpaused");
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausesound.Play();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        resumesound.Play();
    }

    public void ToMenuButton()
    {
        {
            quitsound.Play();
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1f;
        }
    }

    /*public void ToRestartButton()
    {
        selectsound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}

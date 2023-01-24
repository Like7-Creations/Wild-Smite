using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;

    public string MenuScene;

    //public AudioSource pausesound;
    //public AudioSource selectsound;
    //public AudioSource quitsound;
    //public AudioSource resumesound;


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
        //pausesound.Play();
    }

    public void ResumeGame()
    {
        Debug.Log("Unpaused");
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //resumesound.Play();
        pauseMenu.SetActive(false);
    }

    public void ToMenuButton()
    {
        {
           // quitsound.Play();
            SceneManager.LoadScene(MenuScene);
            Time.timeScale = 1f;
        }
    }

    /*public void ToRestartButton()
    {
        selectsound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}

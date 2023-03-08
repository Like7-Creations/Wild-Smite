using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PauseMenuController PauseControl;
    private bool isPaused;

    public string MenuScene;

    //public AudioSource pausesound;
    //public AudioSource selectsound;
    //public AudioSource quitsound;
    //public AudioSource resumesound;

    private void Awake()
    {
        PauseControl = transform.parent.GetComponent<PauseMenuController>();
    }

    public void Resume()
    {
        PauseControl.ResumeGame();
    }

    public void ToMenuButton()
    {
        SceneManager.LoadScene(MenuScene);
        Time.timeScale = 1f;
    }

    /*public void ToRestartButton()
    {
        selectsound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}

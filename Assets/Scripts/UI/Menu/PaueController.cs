using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PaueController : MonoBehaviour
{
    //Public Variables
    [Header("Scene To Load")]
    public string mainMenu;

    [Header("Activation KeyBind")]
    public KeyCode pauseKey;

    //Private Variables
    bool isPaused;

    //Functions

    public void PauseGame()
    {
        //Notes:-
        /*There may be certain scripts that need to be manually disabled*/


        gameObject.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void ForfeitGame()
    {
        SceneManager.LoadScene(mainMenu);
    }

    void Update()
    {
        if(Input.GetKeyDown(pauseKey))
        {
            if(!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
}

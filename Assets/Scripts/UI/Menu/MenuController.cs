using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels to Load")]
    public string newGameLevel;
    string levelToLoad;

    [Header("UI Elements")]
    [SerializeField] GameObject noSaveGameObj = null;


    //Functions
    public void StartNewGame()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedGames"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedGames");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSaveGameObj.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("You have abandoned the animals. PETA is closing in on your position");
    }
}

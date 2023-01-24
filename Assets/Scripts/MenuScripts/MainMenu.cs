using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    //public AudioSource entersound;
    //public AudioSource backsound;
    //public GameObject Button_LoadGame = GameObject.Find("Button_LoadGame");
    //public GameObject Button_NewGame = GameObject.Find("Button_NewGame");
    //public GameObject Button_Character = GameObject.Find("Button Character");
    //public GameObject Button_Settings = GameObject.Find("Button Settings");
    //public GameObject Button_CoopGame = GameObject.Find("Button_CoopGame");
    //public GameObject Button_Credits = GameObject.Find("Button_Credits");
    //public GameObject Button_QuitGame = GameObject.Find("Button_QuitGame");

    public float delayTime = 2f;
    public string sceneToLoad;

    public void PlayLoadGame()
    {
        //Vector3 pos = Button_LoadGame.transform.position;
        //pos.x -= 30f;
        //Button_LoadGame.transform.position = pos;

        //entersound.Play();
        Invoke("DelayLoadGame", delayTime);
    }

    void DelayLoadGame()
    {
        SceneManager.LoadScene("Sami");
        Time.timeScale = 1f;
    }


    public void PlayNewGame()
    {
        //Vector3 pos = Button_NewGame.transform.position;
        //pos.x -= 30f;
        //Button_NewGame.transform.position = pos;

        //entersound.Play();
        //Invoke("DelayNewGame", delayTime);
        SceneManager.LoadScene(sceneToLoad);
    }

    void DelayNewGame()
    {
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1f;
    }

    public void PlayCharacter()
    {
        //Vector3 pos = Button_Character.transform.position;
        //pos.x -= 30f;
        //Button_Character.transform.position = pos;

        //entersound.Play();
        Invoke("DelayCharacter", delayTime);
    }

    void DelayCharacter()
    {
        SceneManager.LoadScene("CharacterSelection");
        Time.timeScale = 1f;
    }


    public void PlayCoopGame()
    {
        //Vector3 pos = Button_CoopGame.transform.position;
        //pos.x -= 30f;
        //Button_CoopGame.transform.position = pos;

        //entersound.Play();
        Invoke("DelayPlayCoopGame", delayTime);
    }

    void DelayPlayCoopGame()
    {
        SceneManager.LoadScene("CoopScene");
        Time.timeScale = 1f;
    }

    public void PlayCredits()
    {
        //Vector3 pos = Button_Credits.transform.position;
        //pos.x -= 30f;
        //Button_Credits.transform.position = pos;

        //entersound.Play();
        Invoke("DelayPlayCredits", delayTime);
    }

    void DelayPlayCredits()
    {
        SceneManager.LoadScene("Credits");
        Time.timeScale = 1f;
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

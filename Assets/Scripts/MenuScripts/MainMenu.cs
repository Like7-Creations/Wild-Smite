using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public AudioSource entersound;
    public AudioSource backsound;
    public GameObject buttonsonclick;

    public float delayTime = 2f;

    public void PlayLoadGame()
    {
        buttonsonclick.SetActive(false);
        entersound.Play();
        Invoke("DelayLoadGame", delayTime);
    }

                                    void DelayLoadGame()
                                    {
                                        SceneManager.LoadScene("Sami");
                                        Time.timeScale = 1f;
                                    }


    public void PlayNewGame()
    {
        buttonsonclick.SetActive(false);
        entersound.Play();
        Invoke("DelayNewGame", delayTime);
    }

                                    void DelayNewGame()
                                    {
                                        SceneManager.LoadScene("_MainScene");
                                        Time.timeScale = 1f;
                                    }

    public void PlayCharacter()
    {
        buttonsonclick.SetActive(false);
        entersound.Play();
        Invoke("DelayCharacter", delayTime);
    }

                                    void DelayCharacter()
                                    {
                                        SceneManager.LoadScene("Character Scene");
                                        Time.timeScale = 1f;
                                    }


    public void PlayCoopGame()
    {
        buttonsonclick.SetActive(false);
        entersound.Play();
        Invoke("DelayPlayCoopGame", delayTime);
    }

                                    void DelayPlayCoopGame()
                                    {
                                        SceneManager.LoadScene("Character Scene");
                                        Time.timeScale = 1f;
                                    }

    public void PlayCredits()
    {
        buttonsonclick.SetActive(false);
        entersound.Play();
        Invoke("DelayPlayCredits", delayTime);
    }

                                    void DelayPlayCredits()
                                    {
                                        SceneManager.LoadScene("Character Scene");
                                        Time.timeScale = 1f;
                                    }

    public void QuitGame()
    {
        backsound.Play();
        Application.Quit();
        Invoke("DelayQuitGame", delayTime);
    }

                                    void DelayQuitGame()
                                    {
                                        SceneManager.LoadScene("Character Scene");
                                        Time.timeScale = 1f;
                                    }

}

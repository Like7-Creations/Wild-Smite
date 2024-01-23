using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public float delayTime = 2f;

    /*//Farhan's Code
    [Header("Volume Settings")]
    [SerializeField] TMP_Text volume_ValTex = null;
    [SerializeField] Slider volSlider = null;

    [SerializeField] GameObject confirmationPrompt;
    //Farhan's Code*/


    [Header("PlayerSelection")]
    public PlayerConfigManager playerConfigManager;
    public GameObject SelectionCanvas;
    public GameObject SelectionPanel;
    public GameObject MenuPanel;
    public Button firstButton;

    public PanelTweenController tweenControl;
    
    private void Awake()
    {
        // playerConfigManager = PlayerConfigManager.Instance;
        ResetManager(SelectionPanel);
    }

    public void LoadCharacterSelection()
    {
        //PlayerConfigManager.Instance.SetMaxPlayers(playerCount);
        SelectionCanvas.SetActive(true);
    }

    public void ResetManager(GameObject panel)
    {
        PlayerConfigManager.Instance.ResetManager(panel);
        SelectionCanvas.SetActive(false);
    }

    public void SelectFirstButton()
    {
        firstButton.Select();
    }

    public void ToggleMenuState()
    {
        if (MenuPanel.active)
        {
            MenuPanel.SetActive(false);
            tweenControl.ResetButtonTweens();
        }
        else
        {
            MenuPanel.SetActive(true);
            tweenControl.TweenButtons();
        }
    }

    public void SetPlayerJoins(bool state)
    {
        PlayerConfigManager.Instance.SetJoinState(state);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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

    

    /*//Farhan's Code
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volume_ValTex.text = volume.ToString("0.0");
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        StartCoroutine(ChangesConfirmed());
    }

    public IEnumerator ChangesConfirmed()
    {
        confirmationPrompt.SetActive(true);

        yield return new WaitForSeconds(2);

        confirmationPrompt.SetActive(false);
    }

    //Farhan's Code*/
}

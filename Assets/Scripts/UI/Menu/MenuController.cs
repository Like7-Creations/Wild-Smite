using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    //Farhan's Code
    [Header("Volume Settings")]
    [SerializeField] Slider volSlider = null;
    [SerializeField] TMP_Text volume_ValTex = null;

    [SerializeField] float defaultVol_Val = 0.5f;

    [SerializeField] GameObject confirmationPrompt;


    [Header("Graphics Settings")]
    [SerializeField] Slider brightnessSlider = null;
    [SerializeField] TMP_Text brightness_ValTex = null;
    [SerializeField] float defaultBrightness = 1;

    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] Toggle fullScreenToggle;

    float brightnessLvl;
    int qualityLvl;
    bool isFullScreen;

    [Header("Resolution Settings")]
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    //Farhan's Code

    [Header("Levels to Load")]
    public string newGameLevel;
    string levelToLoad;

    [Header("UI Elements")]
    [SerializeField] GameObject noSaveGameObj = null;


    #region MainMenu Functions
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
    #endregion

    //Farhan's Code

    #region StandardFunctions
    void Start()
    {
        #region Identifying The Resolution Options & Displaying Current One
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        #endregion
    }
    #endregion

    #region Sound Functions
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
    #endregion

    #region Graphics Functions
    public void SetBrightness(float brightness)
    {
        brightnessLvl = brightness;
        brightness_ValTex.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool fullScreen)
    {
        isFullScreen = fullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        qualityLvl = qualityIndex;
    }

    public void ApplyGraphicsChanges()
    {
        PlayerPrefs.SetFloat("Master Brightness", brightnessLvl);
        //Change brightness here.

        PlayerPrefs.SetInt("Master Quality", qualityLvl);
        QualitySettings.SetQualityLevel(qualityLvl);

        PlayerPrefs.SetInt("FullScreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;

        StartCoroutine(ChangesConfirmed());
    }
    #endregion

    #region Resolution Functions
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
    }


    #endregion

    public void ResetChanges(string menuType)
    {
        if(menuType == "Sound")
        {
            AudioListener.volume = defaultVol_Val;
            volSlider.value= defaultVol_Val;
            volume_ValTex.text = defaultVol_Val.ToString("0.0");
            SaveSoundSettings();
        }
        else if (menuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightness_ValTex.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            ApplyGraphicsChanges();
        }
    }


    public IEnumerator ChangesConfirmed()
    {
        confirmationPrompt.SetActive(true);

        yield return new WaitForSeconds(2);

        confirmationPrompt.SetActive(false);
    }

    //Farhan's Code
}

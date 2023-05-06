using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fScreenTog;

    public Resolution[] resolutions;
    public TMP_Text resButton_Txt;

    List<string> resOptions;
    int selectedResIndex;
    int defaultResIndex;

    int selectedQualityIndex;
    public TMP_Text qualButton_Text;
    string[] qualityOptions;

    public AudioMixer mainMixer;

    public Slider masterSlider;
    public TMP_Text masterLabel;

    public Slider playerSlider;
    public TMP_Text playerLabel;

    public Slider enemySlider;
    public TMP_Text enemyLabel;

    public Slider musicSlider;
    public TMP_Text musicLabel;

    public Slider uiSlider;
    public TMP_Text uiLabel;

    public List<float> uiScaleValues;
    public TMP_Text uiScaleLabel;

    void Start()
    {
        #region Prepping Game Settings

        #endregion

        #region Prepping Graphics Settings

        fScreenTog.isOn = Screen.fullScreen;

        #region Identifying All Available Quality Options & Displaying Current One
        qualityOptions = new string[QualitySettings.names.Length];
        qualityOptions = QualitySettings.names;

        selectedQualityIndex = QualitySettings.GetQualityLevel();

        qualButton_Text.text = qualityOptions[selectedQualityIndex];
        #endregion

        #region Identifying All Available Resolutions & Displaying Current One
        resolutions = Screen.resolutions;

        List<string> resOptions = new List<string>();

        bool foundCurrentRes = false;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                foundCurrentRes = true;

                selectedResIndex = i;
                defaultResIndex = i;
                UpdateResLabel();
            }
        }

        /*if (!foundCurrentRes)
        {
            //Create new resolution option here, that matches the current screen's resolution
            //Add the new resolution option to the list of available options.
            //Set the selected count to be equal to "resolutions.Length - 1"

            //Update the text.

        }*/
        #endregion

        #endregion

        #region Prepping Audio Settings

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        masterLabel.text = Mathf.RoundToInt(masterSlider.value * 100).ToString();

        playerSlider.value = PlayerPrefs.GetFloat("PlayerVolume");
        playerLabel.text = Mathf.RoundToInt(playerSlider.value * 100).ToString();

        enemySlider.value = PlayerPrefs.GetFloat("EnemyVolume");
        enemyLabel.text = Mathf.RoundToInt(enemySlider.value * 100).ToString();

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        musicLabel.text = Mathf.RoundToInt(musicSlider.value * 100).ToString();

        uiSlider.value = PlayerPrefs.GetFloat("UIVolume");
        uiLabel.text = Mathf.RoundToInt(uiSlider.value * 100).ToString();

        #endregion
    }

    void Update()
    {

    }

    public float CalculatePercentage(float percentage)
    {
        if (percentage > 5)
        {
            percentage = 5;
        }

        float result = (percentage / 100) * 20;

        return result;
    }

    #region Graphics Functions

    #region Quality Functions
    /*public void SelectPrevQuality()
    {
        //Decrease the selected QualityIndex.
        selectedQualityIndex--;

        //Add a check to ensure that the selected QualityIndex does not go below zero.
        if (selectedQualityIndex < 0)
        {
            selectedQualityIndex = 0;
        }

        //Update the Quaity Label
        UpdateQualityLabel();
    }

    public void SelectNextQuality()
    {
        //Increase the selected QualityIndex.
        selectedQualityIndex++;

        //Add a check to ensure that the selected QualityIndex does not go above the max number of quality options - 1.
        if (selectedQualityIndex > qualityOptions.Length - 1)
        {
            selectedQualityIndex = qualityOptions.Length - 1;
        }

        //Update the Quaity Label
        UpdateQualityLabel();
    }
    */  //Deprecate Later
    public void SetQualityValue(int qualIndex)
    {
        selectedQualityIndex = qualIndex;
    }

    public void UpdateQualityLabel()
    {
        //Access the text element of the Quality Label, and then take the specified quality, convert it into a string and paste it.
        qualButton_Text.text = qualityOptions[selectedQualityIndex];
    }
    #endregion

    #region Resolution Functions
    public void SelectPrevResolution()
    {
        selectedResIndex--;

        if (selectedResIndex < 0)
        {
            selectedResIndex = 0;
        }

        UpdateResLabel();
    }

    public void SelectNextResolution()
    {
        selectedResIndex++;

        if (selectedResIndex > resolutions.Length - 1)
        {
            selectedResIndex = resolutions.Length - 1;
        }

        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resButton_Txt.text = resolutions[selectedResIndex].width.ToString() + " x " + resolutions[selectedResIndex].height.ToString();
    }
    #endregion

    #endregion

    #region Audio Functions

    float CalculateVolumeVal(float sliderVal)
    {
        float val = 0f;

        if (sliderVal == 0)
        {
            val = -80f;
        }
        else if (sliderVal == 1)
        {
            val = 0;
        }
        else
        {
            val = Mathf.Log(sliderVal) * 20f;
        }

        return val;
    }

    public void SetMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value * 100).ToString();

        mainMixer.SetFloat("MasterVol", CalculateVolumeVal(masterSlider.value));
    }

    public void SetPlayerVol()
    {
        playerLabel.text = Mathf.RoundToInt(playerSlider.value * 100).ToString();

        mainMixer.SetFloat("PlayerVol", CalculateVolumeVal(playerSlider.value));
    }

    public void SetEnemyVol()
    {
        enemyLabel.text = Mathf.RoundToInt(enemySlider.value * 100).ToString();

        mainMixer.SetFloat("EnemyVol", CalculateVolumeVal(enemySlider.value));
    }

    public void SetMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value * 100).ToString();

        mainMixer.SetFloat("MusicVol", CalculateVolumeVal(musicSlider.value));
    }

    public void SetUIVol()
    {
        uiLabel.text = Mathf.RoundToInt(uiSlider.value * 100).ToString();

        mainMixer.SetFloat("UIVol", CalculateVolumeVal(uiSlider.value));
    }
    #endregion

    #region General GameSetting Functions

    //Create a function that takes an int parameter, and uses it to scale the game UI in the scene.

    float uiScale = 0;

    public void ScaleUI(float scaleVal)
    {
        uiScale = uiScaleValues[(int)(scaleVal - 1)];
        Debug.Log("Set UI Scale to add " + uiScale);

        UpdateScaleSliderText(scaleVal);
    }

    public void UpdateScaleSliderText(float val)
    {
        uiScaleLabel.text = val.ToString();
    }

    #endregion

    #region Apply Settings Functions
    public void ApplyGraphics()
    {
        //Apply the new Quality Settings.
        QualitySettings.SetQualityLevel(selectedQualityIndex);

        //Apply the new Resolution Settings.
        Screen.SetResolution(resolutions[selectedResIndex].width, resolutions[selectedResIndex].height, fScreenTog.isOn);
    }

    public void ApplyAudio()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);

        PlayerPrefs.SetFloat("PlayerVolume", playerSlider.value);

        PlayerPrefs.SetFloat("EnemyVolume", enemySlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);

        PlayerPrefs.SetFloat("UIVolume", uiSlider.value);
    }

    public void ApplyGame()
    {
        PlayerPrefs.SetFloat("GameUIScale", uiScale);
    }

    #endregion

    #region Rest Settings Functions
    public void ResetGraphics()
    {
        //Reset Quality
        selectedQualityIndex = 1;

        QualitySettings.SetQualityLevel(1);
        UpdateQualityLabel();

        //Reset Screen Resolution
        selectedResIndex = defaultResIndex;

        fScreenTog.isOn = false;

        Screen.SetResolution(Screen.width, Screen.height, false);
        UpdateResLabel();
    }

    public void ResetAudio()
    {
        //Reset Master Audio
        masterSlider.value = 0.5f;
        masterLabel.text = Mathf.RoundToInt(masterSlider.value * 100).ToString();
        mainMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);

        //Reset Player Audo
        playerSlider.value = 0.5f;
        playerLabel.text = Mathf.RoundToInt(playerSlider.value * 100).ToString();
        mainMixer.SetFloat("PlayerVol", playerSlider.value);

        PlayerPrefs.SetFloat("PlayerVolume", playerSlider.value);

        //Reset Enemy Audio
        enemySlider.value = 0.5f;
        enemyLabel.text = Mathf.RoundToInt(enemySlider.value * 100).ToString();
        mainMixer.SetFloat("EnemyVol", enemySlider.value);

        PlayerPrefs.SetFloat("EnemyVolume", enemySlider.value);

        //Reset Music Audio
        musicSlider.value = 0.5f;
        musicLabel.text = Mathf.RoundToInt(musicSlider.value * 100).ToString();
        mainMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);

        //Reset UI Audio
        uiSlider.value = 0.5f;
        uiLabel.text = Mathf.RoundToInt(uiSlider.value * 100).ToString();
        mainMixer.SetFloat("UIVol", uiSlider.value);

        PlayerPrefs.SetFloat("UIVolume", uiSlider.value);
    }
    #endregion
}
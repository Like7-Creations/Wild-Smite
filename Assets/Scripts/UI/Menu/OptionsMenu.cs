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

    void Start()
    {
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

        float vol = 0;
        mainMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        mainMixer.GetFloat("PlayerVol", out vol);
        playerSlider.value = vol;
        playerLabel.text = Mathf.RoundToInt(playerSlider.value + 80).ToString();

        mainMixer.GetFloat("EnemyVol", out vol);
        enemySlider.value = vol;
        enemyLabel.text = Mathf.RoundToInt(enemySlider.value + 80).ToString();

        mainMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        mainMixer.GetFloat("UIVol", out vol);
        uiSlider.value = vol;
        uiLabel.text = Mathf.RoundToInt(uiSlider.value + 80).ToString();

        #endregion
    }

    void Update()
    {

    }

    #region Graphics Functions

    #region Quality Functions
    public void SelectPrevQuality()
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

    public void SetMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        mainMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void SetPlayerVol()
    {
        playerLabel.text = Mathf.RoundToInt(playerSlider.value + 80).ToString();

        mainMixer.SetFloat("PlayerVol", playerSlider.value);

        PlayerPrefs.SetFloat("PlayerVolume", playerSlider.value);
    }

    public void SetEnemyVol()
    {
        enemyLabel.text = Mathf.RoundToInt(enemySlider.value + 80).ToString();

        mainMixer.SetFloat("EnemyVol", enemySlider.value);

        PlayerPrefs.SetFloat("EnemyVolume", enemySlider.value);
    }

    public void SetMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        mainMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetUIVol()
    {
        uiLabel.text = Mathf.RoundToInt(uiSlider.value + 80).ToString();

        mainMixer.SetFloat("UIVol", uiSlider.value);

        PlayerPrefs.SetFloat("UIVolume", uiSlider.value);
    }
    #endregion

    #region General GameSetting Functions

    //Create a function that enables key recording mode.

    //Create function that unbinds a key from an action.

    //Create function that binds a key to an action.

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
        masterSlider.value = 0;
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        mainMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);

        //Reset Player Audo
        playerSlider.value = 0;
        playerLabel.text = Mathf.RoundToInt(playerSlider.value + 80).ToString();
        mainMixer.SetFloat("PlayerVol", playerSlider.value);

        PlayerPrefs.SetFloat("PlayerVolume", playerSlider.value);

        //Reset Enemy Audio
        enemySlider.value = 0;
        enemyLabel.text = Mathf.RoundToInt(enemySlider.value + 80).ToString();
        mainMixer.SetFloat("EnemyVol", enemySlider.value);

        PlayerPrefs.SetFloat("EnemyVolume", enemySlider.value);

        //Reset Music Audio
        musicSlider.value = 0;
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        mainMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);

        //Reset UI Audio
        uiSlider.value = 0;
        uiLabel.text = Mathf.RoundToInt(uiSlider.value + 80).ToString();
        mainMixer.SetFloat("UIVol", uiSlider.value);

        PlayerPrefs.SetFloat("UIVolume", uiSlider.value);
    }
    #endregion
}
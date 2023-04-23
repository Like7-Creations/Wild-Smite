using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionsMenu : MonoBehaviour
{
    public Toggle fScreenTog;

    public Resolution[] resolutions;
    public TMP_Text resButton_Txt;

    List<string> resOptions;
    int selectedResIndex;

    int selectedQualityIndex;
    public TMP_Text qualButton_Text;
    string[] qualityOptions;

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



    #endregion

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
}
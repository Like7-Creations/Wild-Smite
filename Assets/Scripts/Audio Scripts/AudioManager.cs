using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        float vol = 0f;

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            vol = PlayerPrefs.GetFloat("MasterVolume");
            vol = CalculateVal(vol);
            Debug.Log("Retrieved " + vol + "From player Prefs Master");

            mixer.SetFloat("MasterVol", vol);
        }

        if (PlayerPrefs.HasKey("PlayerVolume"))
        {
            vol = PlayerPrefs.GetFloat("PlayerVolume");
            vol = CalculateVal(vol);
            Debug.Log("Retrieved " + vol + "From player Prefs Player");

            mixer.SetFloat("PlayerVol", vol);
        }

        if (PlayerPrefs.HasKey("EnemyVolume"))
        {
            vol = PlayerPrefs.GetFloat("EnemyVolume");
            vol = CalculateVal(vol);
            Debug.Log("Retrieved " + vol + "From player Prefs Enemy");

            mixer.SetFloat("EnemyVol", vol);
        }

        if (PlayerPrefs.HasKey("UIVolume"))
        {
            vol = PlayerPrefs.GetFloat("UIVolume");
            vol= CalculateVal(vol);
            Debug.Log("Retrieved " + vol + "From player Prefs UI");

            mixer.SetFloat("UIVol", vol);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            vol = PlayerPrefs.GetFloat("MusicVolume");
            vol= CalculateVal(vol);
            Debug.Log("Retrieved " + vol + "From player Prefs Music");

            mixer.SetFloat("MusicVol", vol);
        }

    }

    float CalculateVal(float sliderVal)
    {
        float val = 0;

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
}
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
        if (PlayerPrefs.HasKey("MasterVolume"))
            mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVolume"));

        if (PlayerPrefs.HasKey("MusicVolume"))
            mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVolume"));

        if (PlayerPrefs.HasKey("SFXVolume"))
            mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVolume"));
    }
}
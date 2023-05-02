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

        if (PlayerPrefs.HasKey("PlayerVolume"))
            mixer.SetFloat("PlayerVol", PlayerPrefs.GetFloat("PlayerVolume"));

        if (PlayerPrefs.HasKey("EnemyVolume"))
            mixer.SetFloat("EnemyVol", PlayerPrefs.GetFloat("EnemyVolume"));

        if (PlayerPrefs.HasKey("UIVolume"))
            mixer.SetFloat("UIVol", PlayerPrefs.GetFloat("UIVolume"));

        if (PlayerPrefs.HasKey("MusicVolume"))
            mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVolume"));

    }
}
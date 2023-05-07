using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
            mixer.SetFloat("MasterVol", Mathf.Log(PlayerPrefs.GetFloat("MasterVolume")) * 20f);

        if (PlayerPrefs.HasKey("PlayerVolume"))
            mixer.SetFloat("PlayerVol", Mathf.Log(PlayerPrefs.GetFloat("PlayerVolume")) * 20f);

        if (PlayerPrefs.HasKey("EnemyVolume"))
            mixer.SetFloat("EnemyVol", Mathf.Log(PlayerPrefs.GetFloat("EnemyVolume")) * 20f);

        if (PlayerPrefs.HasKey("UIVolume"))
            mixer.SetFloat("UIVol", Mathf.Log(PlayerPrefs.GetFloat("UIVolume")) * 20f);

        if (PlayerPrefs.HasKey("MusicVolume"))
            mixer.SetFloat("MusicVol", Mathf.Log(PlayerPrefs.GetFloat("MusicVolume")) * 20f);

    }
}
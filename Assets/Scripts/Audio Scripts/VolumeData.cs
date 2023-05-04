using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Volume Data")]
public class VolumeData : ScriptableObject
{
    public string namePrefix = "Volume_";

    public AudioMixer mixer;
    public VolumeHolder[] volumes;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

[System.Serializable]
public class VolumeHolder
{

}
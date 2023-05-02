using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SFXClip
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float voumeVal = 1f;
}

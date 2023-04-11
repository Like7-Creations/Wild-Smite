using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy_SFXHandler : BaseEnemy_SFXHandler
{
    [SerializeField] AudioClip enemy_MeleeSpinVFX;
    [SerializeField] AudioClip enemy_MeleeSwingVFX;
    [SerializeField] AudioClip enemy_MeleeJabVFX;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_MeleeSpinSFX()
    {
        audioSource.clip = enemy_MeleeSpinVFX;
        audioSource.Play();
    }

    public void Play_MeleeSwingSFX()
    {
        audioSource.clip = enemy_MeleeSwingVFX;
        audioSource.Play();
    }

    public void Play_MeleeJabSFX()
    {
        audioSource.clip = enemy_MeleeJabVFX;
        audioSource.Play();
    }
}
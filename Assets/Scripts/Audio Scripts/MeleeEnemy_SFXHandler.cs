using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy_SFXHandler : BaseEnemy_SFXHandler
{
    [SerializeField] SFXClip enemy_MeleeSpinVFX;
    [SerializeField] SFXClip enemy_MeleeSwingVFX;
    [SerializeField] SFXClip enemy_MeleeJabVFX;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_MeleeSpinSFX()
    {
        audioSource.PlayOneShot(enemy_MeleeSpinVFX.clip, enemy_MeleeSpinVFX.voumeVal);
    }

    public void Play_MeleeSwingSFX()
    {
        audioSource.PlayOneShot(enemy_MeleeSwingVFX.clip, enemy_MeleeSwingVFX.voumeVal);
    }

    public void Play_MeleeJabSFX()
    {
        audioSource.PlayOneShot(enemy_MeleeJabVFX.clip, enemy_MeleeJabVFX.voumeVal);
    }
}
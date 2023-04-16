using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy_SFXHandler : MonoBehaviour
{
    protected AudioSource audioSource;

    [SerializeField] AudioClip enemy_AttackWarningVFX;

    [SerializeField] AudioClip enemy_DestroyedVFX;
    [SerializeField] AudioClip enemy_DetectedPlayerVFX;

    [SerializeField] AudioClip enemy_MovementVFX;

    [SerializeField] AudioClip enemy_DamagedVFX;

    public void Play_AttackWarningSFX()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = enemy_AttackWarningVFX;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void Play_DestroyedSFX()
    {
        audioSource.clip = enemy_DestroyedVFX;
        audioSource.Play();
    }

    public void Play_DetectedPlayerSFX()
    {
        audioSource.clip = enemy_DetectedPlayerVFX;
        audioSource.Play();
    }

    public void Play_MovementSFX()
    {
        audioSource.clip = enemy_MovementVFX;
        audioSource.Play();
    }

    public void Play_DamagedSFX()
    {
        audioSource.clip = enemy_DamagedVFX;
        audioSource.Play();
    }


}

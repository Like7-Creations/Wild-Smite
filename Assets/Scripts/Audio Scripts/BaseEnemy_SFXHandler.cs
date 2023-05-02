using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy_SFXHandler : MonoBehaviour
{
    protected AudioSource audioSource;

    [SerializeField] SFXClip enemy_AttackWarningVFX;

    [SerializeField] SFXClip[] enemy_DestroyedVFX;
    [SerializeField] SFXClip enemy_DetectedPlayerVFX;

    [SerializeField] SFXClip enemy_MovementVFX;

    [SerializeField] SFXClip[] enemy_DamagedVFX;

    public void Play_AttackWarningSFX()
    {
        audioSource.PlayOneShot(enemy_AttackWarningVFX.clip, enemy_AttackWarningVFX.voumeVal);

    }

    public void Play_DestroyedSFX()
    {
        SFXClip clip = enemy_DestroyedVFX[Random.Range(0, enemy_DestroyedVFX.Length)];
        audioSource.PlayOneShot(clip.clip, clip.voumeVal);
    }

    public void Play_DetectedPlayerSFX()
    {
        audioSource.PlayOneShot(enemy_DetectedPlayerVFX.clip, enemy_DetectedPlayerVFX.voumeVal);
    }

    public void Play_MovementSFX()
    {
        audioSource.PlayOneShot(enemy_MovementVFX.clip, enemy_MovementVFX.voumeVal);
    }

    public void Play_DamagedSFX()
    {
        SFXClip clip = enemy_DamagedVFX[Random.Range(0, enemy_DamagedVFX.Length)];
        audioSource.PlayOneShot(clip.clip, clip.voumeVal);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy_SFXHandler : BaseEnemy_SFXHandler
{
    [SerializeField] SFXClip enemy_RangeSingleShotVFX;
    [SerializeField] SFXClip enemy_RangeTriShotVFX;
    [SerializeField] SFXClip enemy_RangeHomingVFX;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_RangeSingleShotSFX()
    {
        audioSource.PlayOneShot(enemy_RangeSingleShotVFX.clip, enemy_RangeSingleShotVFX.voumeVal);
    }

    public void Play_RangeTriShotSFX()
    {
        audioSource.PlayOneShot(enemy_RangeTriShotVFX.clip, enemy_RangeTriShotVFX.voumeVal);
    }

    public void Play_RangeHomingSFX()
    {
        audioSource.PlayOneShot(enemy_RangeHomingVFX.clip, enemy_RangeHomingVFX.voumeVal);
    }
}
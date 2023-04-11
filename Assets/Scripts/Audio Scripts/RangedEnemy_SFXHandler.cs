using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy_SFXHandler : BaseEnemy_SFXHandler
{
    [SerializeField] AudioClip enemy_RangeSingleShotVFX;
    [SerializeField] AudioClip enemy_RangeTriShotVFX;
    [SerializeField] AudioClip enemy_RangeHomingVFX;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_RangeSingleShotSFX()
    {
        audioSource.clip = enemy_RangeSingleShotVFX;
        audioSource.Play();
    }

    public void Play_RangeTriShotSFX()
    {
        audioSource.clip = enemy_RangeTriShotVFX;
        audioSource.Play();
    }

    public void Play_RangeHomingSFX()
    {
        audioSource.clip = enemy_RangeHomingVFX;
        audioSource.Play();
    }
}

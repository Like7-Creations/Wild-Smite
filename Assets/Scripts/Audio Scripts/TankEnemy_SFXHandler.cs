using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy_SFXHandler : BaseEnemy_SFXHandler
{
    [SerializeField] AudioClip tank_SmashAttackVFX;
    [SerializeField] AudioClip tank_ShootAttackVFX;
    [SerializeField] AudioClip tank_SwipeAttackVFX;

    [SerializeField] AudioClip tank_MovementVFX;
    
    [SerializeField] AudioClip tank_SummonVFX;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_TankSwipe()
    {
        audioSource.clip = tank_SwipeAttackVFX;
        audioSource.Play();
    }

    public void Play_TankSmashSFX()
    {
        audioSource.clip = tank_SmashAttackVFX;
        audioSource.Play();
    }

    public void Play_TankShootSFX()
    {
        audioSource.clip = tank_ShootAttackVFX;
        audioSource.Play();
    }

    public void Play_TankMoveSFX()
    {
        audioSource.clip = tank_MovementVFX;
        audioSource.Play();
    }

    public void Play_TankSummon()
    {
        audioSource.clip = tank_SummonVFX;
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy_SFXHandler : BaseEnemy_SFXHandler
{
    [SerializeField] SFXClip tank_SmashAttackVFX;
    [SerializeField] SFXClip tank_ShootAttackVFX;
    [SerializeField] SFXClip tank_SwipeAttackVFX;

    [SerializeField] SFXClip tank_MovementVFX;
    
    [SerializeField] SFXClip tank_SummonVFX;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_TankSwipe()
    {
        audioSource.PlayOneShot(tank_SwipeAttackVFX.clip, tank_SwipeAttackVFX.voumeVal);
    }

    public void Play_TankSmashSFX()
    {
        audioSource.PlayOneShot(tank_SmashAttackVFX.clip, tank_SmashAttackVFX.voumeVal);
    }

    public void Play_TankShootSFX()
    {
        audioSource.PlayOneShot(tank_ShootAttackVFX.clip, tank_ShootAttackVFX.voumeVal);
    }

    public void Play_TankMoveSFX()
    {
        audioSource.PlayOneShot(tank_MovementVFX.clip, tank_MovementVFX.voumeVal);
    }

    public void Play_TankSummon()
    {
        audioSource.PlayOneShot(tank_SummonVFX.clip, tank_SummonVFX.voumeVal);
    }
}
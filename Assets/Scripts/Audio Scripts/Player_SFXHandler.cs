using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SFXHandler : MonoBehaviour
{
    PlayerActions pActions;

    AudioSource audioSource;

    [SerializeField] AudioClip playerFootsteps_SFX;

    [SerializeField] AudioClip playerDash_SFX;

    [SerializeField] AudioClip playerSprint_SFX;

    [SerializeField] AudioClip playerAttack_SFX;

    [SerializeField] AudioClip playerAOE_SFX;
    [SerializeField] AudioClip playerAOECharging_SFX;

    [SerializeField] AudioClip playerDmg_SFX;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play_WalkingSFX()
    {
        audioSource.clip = playerFootsteps_SFX;
        audioSource.Play();
    }

    public void Play_DashingSFX()
    {
        audioSource.clip = playerDash_SFX;
        audioSource.Play();
    }

    public void Play_SprintingSFX()
    {
        audioSource.clip = playerSprint_SFX;
        audioSource.Play();
    }

    public void Play_AttackSFX()
    {
        audioSource.clip = playerAttack_SFX;
        audioSource.Play();
    }

    public void Play_AOESFX()
    {
        audioSource.clip = playerAOE_SFX;
        audioSource.Play();
    }

    public void Play_AOEChargeSFX()
    {
        audioSource.clip = playerAOECharging_SFX;
        audioSource.Play();
    }

    public void Play_DamageSFX()
    {
        audioSource.clip = playerDmg_SFX;
        audioSource.Play();
    }


}

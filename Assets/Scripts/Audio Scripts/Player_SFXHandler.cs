using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SFXHandler : MonoBehaviour
{
    PlayerActions pActions;

    [Header("Audio Sources")]
    public AudioSource baseAudio;
    public AudioSource loopAudio;

    [Space(10)]
    [Header("Audio Clips")]
    [SerializeField] AudioClip playerFootsteps_SFX;

    [SerializeField] AudioClip playerDash_SFX;

    [SerializeField] AudioClip playerSprint_SFX;

    [SerializeField] AudioClip playerAttack_SFX;

    [SerializeField] AudioClip playerAOE_SFX;
    [SerializeField] AudioClip playerAOECharging_SFX;

    [SerializeField] AudioClip playerDmg_SFX;

    [Space(10)]
    [Header("Audio Settings")]
    [SerializeField, Range(0, 1)] float volume = 0.8f;

    private void Start()
    {
        //baseAudio = GetComponent<AudioSource>();
        pActions = GetComponent<PlayerActions>();
    }

    public void Play_WalkingSFX()
    {
        if (loopAudio.clip != playerFootsteps_SFX)
        {
            Debug.Log("Triggering WalkSFX");
            loopAudio.clip = playerFootsteps_SFX;
            loopAudio.Play();
        }
        else if (loopAudio.isPlaying && loopAudio.clip == playerFootsteps_SFX)
        {
            loopAudio.clip = null;
            loopAudio.Stop();
        }
    }

    public void Play_DashingSFX()
    {
        //baseAudio.clip = playerDash_SFX;
        baseAudio.PlayOneShot(playerDash_SFX);
    }

    public void Play_SprintingSFX()
    {
        if (pActions.isSprinting)
        {
            if (loopAudio.clip != playerSprint_SFX)
            {
                Debug.Log("Triggering SprintSFX");
                loopAudio.clip = playerSprint_SFX;
                loopAudio.Play();
            }
        }
        else
        {
            if (loopAudio.isPlaying && loopAudio.clip == playerSprint_SFX)
            {
                Debug.Log("Stopping SprintSFX");

                loopAudio.clip = null;
                loopAudio.Stop();
            }
        }
    }

    public void Play_AttackSFX()
    {
        //baseAudio.clip = playerAttack_SFX;
        baseAudio.PlayOneShot(playerAttack_SFX);
    }

    public void Play_AOESFX()
    {
        //baseAudio.clip = playerAOE_SFX;
        baseAudio.PlayOneShot(playerAOE_SFX);
    }

    public void Play_AOEChargeSFX()
    {
        if (pActions.charging)
        {
            if (!loopAudio.isPlaying)
            {
                loopAudio.clip = playerAOECharging_SFX;
                loopAudio.Play();
            }
        }
        else
        {
            if (loopAudio.isPlaying && loopAudio.clip == playerAOECharging_SFX)
            {
                loopAudio.clip = playerAOECharging_SFX;
                loopAudio.Stop();
            }
        }
    }

    public void Play_DamageSFX()
    {
        //baseAudio.clip = playerDmg_SFX;
        baseAudio.PlayOneShot(playerDmg_SFX);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SFXHandler : MonoBehaviour
{
    PlayerActions pActions;

    [Header("Audio Sources")]
    public AudioSource baseAudio;
    public AudioSource moveLoopAudio;
    public AudioSource aoeChargeAudio;

    [Space(10)]

    [Header("Movement SFX")]
    [SerializeField] SFXClip playerWalk_SFX;

    [SerializeField] SFXClip playerDash_SFX;

    [SerializeField] SFXClip playerSprint_SFX;

    [Space(10)]

    [Header("Attack SFX")]
    [SerializeField] SFXClip[] playerMATKs_SFX;
    [SerializeField] SFXClip[] playerRATKs_SFX;

    [SerializeField] SFXClip playerAOE_SFX;
    [SerializeField] SFXClip playerAOECharging_SFX;

    [SerializeField] SFXClip[] playerDamage_SFX;

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
        if (moveLoopAudio.clip != playerWalk_SFX.clip)
        {
            Debug.Log("Triggering WalkSFX");
            moveLoopAudio.clip = playerWalk_SFX.clip;
            moveLoopAudio.volume = playerWalk_SFX.voumeVal;
            moveLoopAudio.Play();
        }
        else if (moveLoopAudio.isPlaying && moveLoopAudio.clip == playerWalk_SFX.clip)
        {
            moveLoopAudio.clip = null;
            moveLoopAudio.Stop();
        }
    }

    public void Play_DashingSFX()
    {
        //baseAudio.clip = playerDash_SFX;
        baseAudio.PlayOneShot(playerDash_SFX.clip, playerDash_SFX.voumeVal);
    }

    public void Play_SprintingSFX()
    {
        if (pActions.isSprinting)
        {
            if (moveLoopAudio.clip != playerSprint_SFX.clip)
            {
                Debug.Log("Triggering SprintSFX");
                moveLoopAudio.clip = playerSprint_SFX.clip;
                moveLoopAudio.volume = playerSprint_SFX.voumeVal;
                moveLoopAudio.Play();
            }
        }
        else
        {
            if (moveLoopAudio.isPlaying && moveLoopAudio.clip == playerSprint_SFX.clip)
            {
                Debug.Log("Stopping SprintSFX");

                moveLoopAudio.clip = null;
                moveLoopAudio.Stop();
            }
        }
    }

    public void Play_MATKSFX()
    {
        //baseAudio.clip = playerAttack_SFX;

        SFXClip attackClip = playerMATKs_SFX[Random.Range(0, playerMATKs_SFX.Length)];

        baseAudio.PlayOneShot(attackClip.clip, attackClip.voumeVal);
    }
    
    public void Play_RATKSFX()
    {
        //baseAudio.clip = playerAttack_SFX;

        SFXClip attackClip = playerRATKs_SFX[Random.Range(0, playerRATKs_SFX.Length)];

        baseAudio.PlayOneShot(attackClip.clip, attackClip.voumeVal);
    }

    public void Play_AOESFX()
    {
        //baseAudio.clip = playerAOE_SFX;
        baseAudio.PlayOneShot(playerAOE_SFX.clip, playerAOE_SFX.voumeVal);
    }

    public void Play_AOEChargeSFX()
    {
        if (pActions.charging)
        {
            if (!aoeChargeAudio.isPlaying)
            {
                aoeChargeAudio.clip = playerAOECharging_SFX.clip;
                aoeChargeAudio.volume = playerAOECharging_SFX.voumeVal;
                aoeChargeAudio.Play();
            }
        }
        else
        {
            if (aoeChargeAudio.isPlaying && aoeChargeAudio.clip == playerAOECharging_SFX.clip)
            {
                aoeChargeAudio.clip = playerAOECharging_SFX.clip;
                aoeChargeAudio.Stop();
            }
        }
    }

    public void Play_DamageSFX()
    {
        //baseAudio.clip = playerDmg_SFX;

        SFXClip dmgClip = playerDamage_SFX[Random.Range(0, playerDamage_SFX.Length)];


        baseAudio.PlayOneShot(dmgClip.clip, dmgClip.voumeVal);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VFXHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem playerFootsteps_VFX;

    [SerializeField] ParticleSystem playerDash1_VFX;
    //[SerializeField] ParticleSystem playerDash2_VFX;    //Will be deprecated.

    [SerializeField] ParticleSystem playerSprint1_VFX;
    //[SerializeField] ParticleSystem playerSprint2_VFX;

    [SerializeField] ParticleSystem playerAttack_VFX;

    [SerializeField] ParticleSystem playerAOE_VFX;
    [SerializeField] ParticleSystem playerAOECharging_VFX;

    [SerializeField] ParticleSystem playerDmg_VFX;

    public void TriggerWalkingVFX()
    {
        if(playerFootsteps_VFX.isStopped)
        {
            playerFootsteps_VFX.Play();
        }
        else if (playerFootsteps_VFX.isPlaying)
        {
            playerFootsteps_VFX.Stop();
        }
    }

    public void TriggerDashVFX()
    {
        if (playerDash1_VFX.isStopped)
        {
            playerDash1_VFX.Play();
        }
        else if (playerDash1_VFX.isPlaying)
        {
            playerDash1_VFX.Stop();
        }
    }

    public void TriggerSprintVFX()
    {
        //ParticleSystem currentDash_VFX;

        if (playerSprint1_VFX.isStopped)
        {
            playerSprint1_VFX.Play();
        }
        else if (playerSprint1_VFX.isPlaying)
        {
            playerSprint1_VFX.Stop();
        }
    }

    public void TriggerAttackVFX()
    {
        if (playerAttack_VFX.isStopped)
        {
            playerAttack_VFX.Play();
        }
        else if (playerAttack_VFX.isPlaying)
        {
            playerAttack_VFX.Stop();
        }
    }

    public void TriggerAOEVFX()
    {
        if (playerAOE_VFX.isStopped)
        {
            playerAOE_VFX.Play();
        }
        else if (playerAOE_VFX.isPlaying)
        {
            playerAOE_VFX.Stop();
        }
    }

    public void TriggerAOEChargingVFX()
    {
        if (playerAOECharging_VFX.isStopped)
        {
            playerAOECharging_VFX.Play();
        }
        else if (playerAOECharging_VFX.isPlaying)
        {
            playerAOECharging_VFX.Stop();
        }
    }

    public void TriggerDamageVFX()
    {
        if (playerDmg_VFX.isStopped)
        {
            playerDmg_VFX.Play();
        }
        else if (playerDmg_VFX.isPlaying)
        {
            playerDmg_VFX.Stop();
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

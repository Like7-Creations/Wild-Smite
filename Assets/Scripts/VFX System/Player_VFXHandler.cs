using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VFXHandler : MonoBehaviour
{
    PlayerActions pActions;

    [SerializeField] ParticleSystem playerFootsteps_VFX;

    [SerializeField] ParticleSystem playerDash1_VFX;

    [SerializeField] ParticleSystem playerSprint1_VFX;

    [SerializeField] ParticleSystem playerAttack_VFX;

    [SerializeField] ParticleSystem playerAOE_VFX;
    [SerializeField] ParticleSystem playerAOECharging_VFX;

    [SerializeField] ParticleSystem playerDmg_VFX;

    //Needs to be triggered
    public void TriggerWalkingVFX()
    {
        if (!playerFootsteps_VFX.isPlaying)
        {
            playerFootsteps_VFX.Play();
        }
        else if (playerFootsteps_VFX.isPlaying)
        {
            playerFootsteps_VFX.Stop();
        }
    }

    //Needs to be triggered
    public void TriggerDashVFX()
    {
        if (!playerDash1_VFX.isPlaying)
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
        if (pActions.isSprinting)
        {
            if (!playerSprint1_VFX.isPlaying)
            {
                playerSprint1_VFX.Play();
            }
        }
        else
        {
            if (playerSprint1_VFX.isPlaying)
            {
                playerSprint1_VFX.Stop();
            }
        }
    }

    //Needs to be triggered
    public void TriggerAttackVFX()
    {
        if (!playerAttack_VFX.isPlaying)
        {
            playerAttack_VFX.Play();
        }
        else if (playerAttack_VFX.isPlaying)
        {
            playerAttack_VFX.Stop();
        }
    }

    //Needs to be triggered
    public void TriggerAOEVFX()
    {
        if (!pActions.charging && pActions.currentCharge >= 0)
        {
            if (!playerAOE_VFX.isPlaying)
            {
                playerAOE_VFX.Play();

                playerAOE_VFX.gameObject.transform.localScale = new Vector3(pActions.currentCharge, pActions.currentCharge, pActions.currentCharge);
            }
        }
        else if (!pActions.charging || pActions.currentCharge == 0)
        {
            if (playerAOE_VFX.isPlaying)
            {
                playerAOE_VFX.Stop();

                playerAOE_VFX.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }
    }

    //Needs to be triggered and then stopped.
    public void TriggerAOEChargingVFX()
    {
        if (pActions.charging)
        {
            if (!playerAOECharging_VFX.isPlaying)
            {
                playerAOECharging_VFX.Play();
            }
        }
        else
        {
            if (playerAOECharging_VFX.isPlaying)
            {
                playerAOECharging_VFX.Stop();
            }
        }
    }

    //Needs to be triggered
    public void TriggerDamageVFX()
    {
        if (!playerDmg_VFX.isPlaying)
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
        pActions = GetComponent<PlayerActions>();

        playerFootsteps_VFX.Stop();
        playerDash1_VFX.Stop();
        playerSprint1_VFX.Stop();

        playerAttack_VFX.Stop();

        playerAOE_VFX.Stop();
        playerAOECharging_VFX.Stop();

        playerDmg_VFX.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

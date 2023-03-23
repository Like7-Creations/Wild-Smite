using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_VFXHandler : Enemy_VFXHandler
{
    [SerializeField] ParticleSystem summon_VFX;

    [SerializeField] ParticleSystem swipe_VFX;

    [SerializeField] public ParticleSystem smash_VFX;

    [SerializeField] ParticleSystem Gatling_VFX;

    [SerializeField] ParticleSystem flurry_VFX;

    [SerializeField] ParticleSystem projectileshoot_vfx;

    [SerializeField] ParticleSystem shockWave_VFX;

    public void SummonVFX()
    {
        if (!summon_VFX.isPlaying)
        {
            summon_VFX.Play();
        }
        else if (summon_VFX.isPlaying)
        {
            summon_VFX.Stop();
        }
    }

    public void SwipeVFX()
    {
        if (!swipe_VFX.isPlaying)
        {
            swipe_VFX.Play();
        }
        else if (swipe_VFX.isPlaying)
        {
            swipe_VFX.Stop();
        }
    }

    public void GatlingVFX()
    {
        if (!Gatling_VFX.isPlaying)
        {
            Gatling_VFX.Play();
        }
        else if (Gatling_VFX.isPlaying)
        {
            Gatling_VFX.Stop();
        }
    }

    public void SmashVFX()
    {
        if (!smash_VFX.isPlaying)
        {
            smash_VFX.Play();
        }
        else if (smash_VFX.isPlaying)
        {
            smash_VFX.Stop();
        }
    }

    public void FlurryVFX()
    {
        if (!flurry_VFX.isPlaying)
        {
            flurry_VFX.Play();
        }
        else if (flurry_VFX.isPlaying)
        {
            flurry_VFX.Stop();
        }
    }

    public void ProjectileBombVFX()
    {
        if (!projectileshoot_vfx.isPlaying)
        {
            projectileshoot_vfx.Play();
        }
        else if (projectileshoot_vfx.isPlaying)
        {
            projectileshoot_vfx.Stop();
        }
    }

    public void ShockwaveVFX()
    {
        if (!shockWave_VFX.isPlaying)
        {
            shockWave_VFX.Play();
        }
        else if (shockWave_VFX.isPlaying)
        {
            shockWave_VFX.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_VFXHandler : Enemy_VFXHandler
{
    [SerializeField] ParticleSystem summon_VFX;

    [SerializeField] ParticleSystem swipe_VFX;

    [SerializeField] ParticleSystem shoot_VFX;

    [SerializeField] public ParticleSystem smash_VFX;

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

    public void ShootVFX()
    {
        if (!shoot_VFX.isPlaying)
        {
            shoot_VFX.Play();
        }
        else if (shoot_VFX.isPlaying)
        {
            shoot_VFX.Stop();
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
}

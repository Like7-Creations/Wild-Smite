using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_VFXHandler : Enemy_VFXHandler
{
    [SerializeField] ParticleSystem jab_VFX;

    [SerializeField] ParticleSystem swing_VFX;

    [SerializeField] ParticleSystem spin_VFX;

    public void jabVFX()
    {
        if(!jab_VFX.isPlaying)
        {
            jab_VFX.Play();
        }
        else if (jab_VFX.isPlaying)
        {
            jab_VFX.Stop();
        }
    }

    public void swingVFX()
    {
        if (!swing_VFX.isPlaying)
        {
            swing_VFX.Play();
        }
        else if (jab_VFX.isPlaying)
        {
            swing_VFX.Stop();
        }
    }

    public void spinVFX()
    {
        if (!spin_VFX.isPlaying)
        {
            spin_VFX.Play();
        }
        else if (spin_VFX.isPlaying)
        {
            spin_VFX.Stop();
        }
    }
}

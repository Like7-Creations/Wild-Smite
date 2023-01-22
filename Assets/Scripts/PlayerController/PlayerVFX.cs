using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{

    [Header("Dashing")]
    public TrailRenderer DashVFX;

    [Header("Melee")]
    public TrailRenderer MeleeVFX;

    public void Dash()
    {
        DashVFX.emitting = !DashVFX.emitting;
    }

    public void Melee()
    {
        MeleeVFX.emitting = !MeleeVFX.emitting;
    }
}

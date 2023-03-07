using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : Attack
{
    public override void AttackType()
    {
        //Debug.Log("Swing attack");
        ultimateAI.attackRange = 2;
        fov.viewAngle = 100;
        ultimateAI.anim.SetTrigger("Swing");
        vfx.GetComponent<Melee_VFXHandler>().swingVFX();
        ultimateAI.playerTakeDamage();
        vfx.GetComponent<Melee_VFXHandler>().swingVFX();
        // ultimateAI.MeleeAttack();
        // set animation trigger
    }
}

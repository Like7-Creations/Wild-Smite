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
        ultimateAI.playerTakeDamage();
        // ultimateAI.MeleeAttack();
        // set animation trigger
    }
}

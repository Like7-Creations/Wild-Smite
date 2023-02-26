using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jab : Attack
{
    public override void AttackType()
    {
        Debug.Log("Jab attack");
        ultimateAI.attackRange = 3;
        fov.viewAngle = 65;
        // set animation
        ultimateAI.playerTakeDamage();
    }
}

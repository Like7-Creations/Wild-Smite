using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jab : Attack
{
    public override void AttackType()
    {
        //Debug.Log("Jab attack");
        ultimateAI.attackRange = 3;
        fov.viewAngle = 65;
        ultimateAI.anim.SetTrigger("Jab");
        vfx.GetComponent<Melee_VFXHandler>().jabVFX();
        for (int e = 0; e < ultimateAI.players.Count; e++)
        {
            float dist = Vector3.Distance(ultimateAI.players[e].transform.position, transform.position);
            if (dist < ultimateAI.attackRange)
            {
                ultimateAI.playerTakeDamage();
            }
        }
        vfx.GetComponent<Melee_VFXHandler>().jabVFX(); 
    }
}

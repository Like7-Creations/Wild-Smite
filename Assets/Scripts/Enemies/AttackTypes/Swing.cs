using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : Attack
{
    public override IEnumerator AttackType()
    {
        yield return new WaitForSeconds(0);
        //ultimateAI.attackRange = 2;
        //fov.viewAngle = 100;
        // ultimateAI.anim.SetTrigger("Swing");
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Melee_VFXHandler>().swingVFX();
        }
        if (sfx.isEnabled) 
        {
            var obj = GetComponent<Melee_SFXHandler>();
            var clip = obj.swingSFX[Random.Range(0, obj.swingSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        for (int e = 0; e < state.players.Length; e++)
        {
            float dist = Vector3.Distance(state.players[e].transform.position, transform.position);
            if (dist < stats.attackRange)
            {
                state.chosenPlayer.TakeDamage(stats.MATK);
            }
        }
        if(vfx.isEnabled)
        {
            vfx.GetComponent<Melee_VFXHandler>().swingVFX();
        }
        // ultimateAI.MeleeAttack();
        // set animation trigger
    }
}

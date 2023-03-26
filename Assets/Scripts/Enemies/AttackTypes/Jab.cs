using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jab : Attack
{
    /*public override IEnumerator AttackType()
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
    }*/
    public override IEnumerator AttackType()
    {
        yield return new WaitForSeconds(0);
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Melee_SFXHandler>();
            var clip = obj.jabSFX[Random.Range(0, obj.jabSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        //ultimateAI.attackRange = 3;
        //fov.viewAngle = 65;
        //ultimateAI.anim.SetTrigger("Jab");
        if(vfx.isEnabled)
        {
            vfx.GetComponent<Melee_VFXHandler>().jabVFX();
        }
        for (int e = 0; e < state.players.Length; e++)
        {
            float dist = Vector3.Distance(state.players[e].transform.position, transform.position);
            if (dist < stats.attackRange)
            {
                state.chosenPlayer.TakeDamage(stats.MATK);
            }
        }
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Melee_VFXHandler>().jabVFX();
        }
    }
}

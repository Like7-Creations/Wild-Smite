using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : Attack
{
    public override IEnumerator AttackType()
    {
        //Debug.Log("Swing attack");
        if (vfx.isEnabled)
        {
            vfx.attackIndicationVFX.Play();
        }
        yield return new WaitForSeconds(timeToAttackAfterIndicator);
        ultimateAI.attackRange = 2;
        fov.viewAngle = 100;
        ultimateAI.anim.SetTrigger("Swing");
        vfx.GetComponent<Melee_VFXHandler>().swingVFX();
        if (sfx.isEnabled) 
        {
            var obj = GetComponent<Melee_SFXHandler>();
            var clip = obj.swingSFX[Random.Range(0, obj.swingSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        for (int e = 0; e < ultimateAI.players.Count; e++)
        {
            float dist = Vector3.Distance(ultimateAI.players[e].transform.position, transform.position);
            if (dist < ultimateAI.attackRange)
            {
                ultimateAI.playerTakeDamage();
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

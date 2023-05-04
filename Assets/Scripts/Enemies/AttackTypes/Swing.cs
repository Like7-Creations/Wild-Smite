using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Swing : Attack
{
    public override void Start()
    {
        base.Start();
    }

    public override void attackLogic()
    {
        for (int e = 0; e < state.players.Length; e++)
        {
            float dist = Vector3.Distance(state.players[e].transform.position, transform.position);
            if (dist < stats.attackRange)
            {
                state.chosenPlayer.TakeDamage(stats.MATK);
            }
        }
    }

    public override void attackVFX()
    {
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Melee_VFXHandler>().swingVFX();
        }
    }

    public override void attackSFX()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Melee_SFXHandler>();
            var clip = obj.swingSFX[Random.Range(0, obj.swingSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public override void AttackIndication()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Melee_SFXHandler>();
            var clip = obj.enemyAttackIndicatorSFX[Random.Range(0, obj.enemyAttackIndicatorSFX.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (vfx.isEnabled)
        {
            vfx.GetComponent<Melee_VFXHandler>().attackIndicationVFX.Play();
        }
    }

    /*public override IEnumerator AttackType()
    {
        // Attack indication for ranged and melee are implemented in the state machine, therefore they dont have to be here
        print("called swing");
        yield return new WaitForSeconds(0);
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
    }*/
}

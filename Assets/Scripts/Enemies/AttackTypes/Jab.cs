using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jab : Attack
{
    public override void Start()
    {
        base.Start();
    }

    public override IEnumerator AttackType()
    {
        // Attack indication for ranged and melee are implemented in the state machine, therefore they dont have to be here
        yield return new WaitForSeconds(0);
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Melee_SFXHandler>();
            var clip = obj.jabSFX[Random.Range(0, obj.jabSFX.Length)];
            audioSource.PlayOneShot(clip);
        }
        anim.SetTrigger("Jab");
        if (vfx.isEnabled)
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

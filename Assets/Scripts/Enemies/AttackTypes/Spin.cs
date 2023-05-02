using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Spin : Attack
{
    public float noOfAttacks;
    public float TimeBetweenAttacks;

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
            if (sfx.isEnabled)
            {
                var obj = GetComponent<Melee_SFXHandler>();
                var clip = obj.jabSFX[Random.Range(0, obj.jabSFX.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
    }

    public override void attackVFX()
    {
        if (vfx.isEnabled)
        {
            vfx.GetComponent<Melee_VFXHandler>().spinVFX();
        }
    }

    public override void attackSFX()
    {
        if (sfx.isEnabled)
        {
            var obj = GetComponent<Melee_SFXHandler>();
            var clip = obj.spinSFX[Random.Range(0, obj.jabSFX.Length)];
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


    /* public override IEnumerator AttackType()
     {
         // Attack indication for ranged and melee are implemented in the state machine, therefore they dont have to be here

         if (vfx.isEnabled)
         {
             vfx.GetComponent<Melee_VFXHandler>().spinVFX();
         }
         //anim.SetTrigger("Jab");
         for (int i = 0; i < noOfAttacks; i++)
         {
             for (int e = 0; e < state.players.Length; e++)
             {
                 float dist = Vector3.Distance(state.players[e].transform.position, transform.position);
                 if (dist < stats.attackRange)
                 {
                     state.chosenPlayer.TakeDamage(stats.MATK);
                 }
                 if (sfx.isEnabled)
                 {
                     var obj = GetComponent<Melee_SFXHandler>();
                     var clip = obj.jabSFX[Random.Range(0, obj.jabSFX.Length)];
                     audioSource.PlayOneShot(clip);
                 }
             }
             //anim.SetTrigger("prepSpin");
             yield return new WaitForSeconds(TimeBetweenAttacks); // We either use this or we just use the normal void and call the melee as event at attack time
         }
         if (vfx.isEnabled)
         {
             vfx.GetComponent<Melee_VFXHandler>().spinVFX();
         }
     }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class AttackState : State
{
    public  State retreat;
    float dist;
    [SerializeField] public float attackRange;

    bool indicatorPlayed;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }
    public override State RunCurrentState()
    {
        print("attack state");
        dist = Vector3.Distance(chosenPlayer.transform.position, transform.position);

        Vector3 pos = chosenPlayer.transform.position;
        if(GetComponent<EnemyStats>().Type == EnemyStats.enemyType.Melee)
        {
            agent.SetDestination(pos);
            //agent.destination = pos;
            print("approaching player");
            if (dist <= attackRange)
            {
                Attack();
                return retreat;
            }
        }
        else
        {
            Attack();
            return retreat;
        }
        

        /*if (dist >= attackRange)
        {
            Vector3 pos = chosenPlayer.transform.position = transform.position;
            agent.SetDestination(pos);
            agent.destination = pos;
        }*/
        //else
        //{
        /*if (dist<=attackRange)
        {
            meleeAttack();
            return retreat;
        }*/
        //}
        return this;

    }

    public void Attack()
    {
        GetComponent<MultiAttacker>().AttackPlayer();
        indicatorPlayed = false;
        if (!indicatorPlayed)
        {
            if (vfx.isEnabled)
            {
                vfx.attackIndicationVFX.Play();
                indicatorPlayed = true;
            }
            if (sfx.isEnabled)
            {
                var obj = GetComponent<Enemy_SFXHandler>();
                if (obj.GetComponent<Melee_SFXHandler>() != null)
                {
                    var clipObj = obj.GetComponent<Melee_SFXHandler>();
                    var clip = clipObj.enemyAttackIndicatorSFX[Random.Range(0, clipObj.enemyAttackIndicatorSFX.Length)];
                    audioSource.PlayOneShot(clip);
                }

                if (obj.GetComponent<Range_SFXHandler>() != null)
                {
                    var clipObj = obj.GetComponent<Range_SFXHandler>();
                    var clip = clipObj.enemyAttackIndicatorSFX[Random.Range(0, clipObj.enemyAttackIndicatorSFX.Length)];
                    audioSource.PlayOneShot(clip);
                }
            }
        }
    }
}

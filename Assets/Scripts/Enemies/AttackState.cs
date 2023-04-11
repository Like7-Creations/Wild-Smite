using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Timeline;
using UnityEngine;

public class AttackState : State
{
    public  State retreat;
    float dist;
    [SerializeField] public float attackRange;

    bool indicatorPlayed;

    bool animSet;

    [SerializeField] int randomAttack;

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
        ///agent.acceleration = 20;

        Vector3 playerpos = chosenPlayer.transform.position;
        //playerpos.y = 1;
        transform.LookAt(playerpos, Vector3.up);

        //indicatorPlayed = false;
        if (!indicatorPlayed)
        {
            if (vfx.isEnabled)
            {
                vfx.attackIndicationVFX.Play();
                indicatorPlayed = true;
            }
            if (sfx.isEnabled)
            {
                var obj = GetComponent<BaseEnemy_SFXHandler>();
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

            anim.SetLayerWeight(anim.GetLayerIndex("AttackLayer"), 1);
            randomAttack = Random.Range(0, 3);

            if (GetComponent<EnemyStats>().Type == EnemyStats.enemyType.Melee)
            {
                switch (randomAttack)
                {
                    case 0:
                        anim.SetTrigger("prepSwing");
                        break;
                    case 1:
                        anim.SetTrigger("prepJab");
                        break;
                    case 2:
                        anim.SetTrigger("prepSpin");
                        break;
                }
            }

            indicatorPlayed = true;
        }

        Vector3 pos = chosenPlayer.transform.position + ((chosenPlayer.transform.position - transform.position).normalized * .8f);
        if(GetComponent<EnemyStats>().Type == EnemyStats.enemyType.Melee)
        {
            agent.SetDestination(pos);
            

            //agent.destination = pos;
            print("approaching player");
            if (dist <= attackRange)
            {
                //Attack();
                StartCoroutine(GetComponent<MultiAttacker>().attacksList[randomAttack].AttackType());
                indicatorPlayed = false;
                agent.acceleration = 10;
                return retreat;
            }
        }
        else
        {
            anim.SetTrigger("AttackPrep");
            Attack();
            agent.acceleration = 10;
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
        print("attacked at attack state");
        indicatorPlayed = false;
    }
}

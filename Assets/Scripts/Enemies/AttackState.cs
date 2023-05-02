using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Timeline;
using UnityEngine;

public class AttackState : State
{
    public  State retreat;
    float dist;
    [SerializeField] public float attackRange;
    public State chase;

    //bool indicatorPlayed;

    //bool animSet;

    [HideInInspector] public bool attacked;

    [SerializeField] int randomAttack;

    float test;

    MultiAttacker multiAttack;

    public override void Start()
    {
        base.Start();
        multiAttack = GetComponent<MultiAttacker>();
        test = agent.speed;
    }
    public override void Update()
    {
        base.Update();
        if(agent.speed != test)
        {
            Debug.LogFormat("speed Changed");
        }
    }
    public override State RunCurrentState()
    {
        dist = Vector3.Distance(chosenPlayer.transform.position, transform.position);
        agent.acceleration = 20;

        Vector3 playerpos = chosenPlayer.transform.position;
        //playerpos.y = 1;
        transform.LookAt(playerpos, Vector3.up);

        anim.SetLayerWeight(anim.GetLayerIndex("AttackLayer"), 1);

        //indicatorPlayed = false;
        /*if (!indicatorPlayed)
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
                //srandomAttack = Random.Range(0, 2);
            }

            anim.SetLayerWeight(anim.GetLayerIndex("AttackLayer"), 1);
            //randomAttack = Random.Range(0, 2);

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
        }*/




        if(GetComponent<EnemyStats>().Type == EnemyStats.enemyType.Melee)
        {
            Vector3 pos = chosenPlayer.transform.position + ((chosenPlayer.transform.position - transform.position).normalized * .8f);
            agent.SetDestination(pos);

            //agent.destination = pos;
            //print("approaching player");
            if (dist <= attackRange)
            {
                //Attack();
                //if(!attacked) Attack();
                //print($"attacked player at {dist}");
                //StartCoroutine(GetComponent<MultiAttacker>().attacksList[randomAttack].AttackType());
                //GetComponent<MultiAttacker>().attacksList[randomAttack].AttackType();

                // indicatorPlayed = false;
                agent.acceleration = 10;
                //return retreat;
            }
        }
        else
        {
            agent.acceleration = 10;
            //return retreat;
        }

        if (dist >= GetComponent<Chase>().orbitRange + 2)
        {
            // indicatorPlayed = false;
            agent.acceleration = 10;
            return chase;
        }

        /*if (GetComponent<EnemyStats>().Type == EnemyStats.enemyType.Melee)
        {
            agent.SetDestination(pos);

            //agent.destination = pos;
            //print("approaching player");
            if (dist <= attackRange)
            {
                //Attack();
                Attack();
                //print($"attacked player at {dist}");
                //StartCoroutine(GetComponent<MultiAttacker>().attacksList[randomAttack].AttackType());
                //GetComponent<MultiAttacker>().attacksList[randomAttack].AttackType();

                indicatorPlayed = false;
                agent.acceleration = 10;
                return retreat;
            }
            else if(dist >= GetComponent<Chase>().orbitRange + 2)
            {
                indicatorPlayed = false;
                agent.acceleration = 10;
                return chase;
            }
        }
        else
        {
            anim.SetTrigger("AttackPrep");
            Attack();
            //agent.acceleration = 10;
            return retreat;
        }*/

        return this;

    }

    public void Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("AttackLayer"), 1);
        multiAttack.AttackPlayer(0, multiAttack.attacksList.Length);
        print("attacked at attack state");
        attacked = true;
        //indicatorPlayed = false;
    }
}

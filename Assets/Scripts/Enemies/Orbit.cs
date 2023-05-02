using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Orbit : State
{
    [SerializeField] public float timer;
    public State attackState;
    public State Chase;
    public State retreat;

    [SerializeField] bool backOff;
    [SerializeField] GameObject retreatOBj;

    float dist;
    public float orbitSpeed;
    public float originalSpeed;

    float orbitRange;

    Vector3 dire;
    int rotateDir;

    MultiAttacker multiAttack;

    public override void Start()
    {
        base.Start();
        multiAttack = GetComponent<MultiAttacker>();
        orbitRange = GetComponent<Chase>().orbitRange;
        originalSpeed = agent.speed;
        rotateDir = Random.Range(1, 3);
       
        if(rotateDir == 1)
        {
            dire = Vector3.down;
        }
        else
        {
            dire = Vector3.up;
        }
        //orbitRange = Random.Range(5, 10);
    }

    public override void Update()
    {
        base.Update();
    }


    public override State RunCurrentState()
    {
        print("orbit state");
        //print(orbitRange);
        dist = Vector3.Distance(chosenPlayer.transform.position, transform.position);
        if(!backOff)
        {
            var offsetPlayer = chosenPlayer.transform.position - transform.position;
            var dir = Vector3.Cross(offsetPlayer, dire);
            agent.SetDestination(transform.position + dir);

            //agent.speed = orbitSpeed;
            Vector3 playerpos = chosenPlayer.transform.position;
            //playerpos.y = 1;
            transform.LookAt(playerpos, Vector3.up);
        }
        

        if (dist < orbitRange)
        {
            agent.speed = 2;
            agent.SetDestination(retreatOBj.transform.position);
            print("movingg away from player");
            backOff = true;
        }
        else if(dist > orbitRange + 1)
        {
            return Chase;
        }
        else
        {
            agent.speed = orbitSpeed;
            backOff= false;
        }

        timer += Time.deltaTime;
        if (timer >= GetComponent<EnemyStats>().generalCDN)
        {
            timer = 0;
            agent.speed = originalSpeed;
            //GetComponent<AttackState>().attacked = false;
            multiAttack.AttackPlayer(0, multiAttack.attacksList.Length);
            return attackState;
        }

        return this;
    }

}

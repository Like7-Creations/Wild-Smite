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
    float originalSpeed;

    float orbitRange;

    Vector3 dire;
    int rotateDir;

    public override void Start()
    {
        base.Start();
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
            return attackState;
        }



        /*if (dist <= orbitRange)
        {
            //transform.RotateAround(chosenPlayer.transform.position, Vector3.up, speed * Time.deltaTime);

            if (dist <= orbitRange - 1)
            {
                agent.speed = 3;
                agent.Move(chosenPlayer.transform.forward * Time.deltaTime);
            }
            else
            {
                agent.speed = originalSpeed;
            }


            timer += Time.deltaTime;
            if (timer >= GetComponent<EnemyStats>().generalCDN)
            {
                timer = 0;
                agent.speed = originalSpeed;
                return attackState;
            }
        }*/


        /*if (GetComponent<EnemyStats>().Type == EnemyStats.enemyType.Range)
        {
            if(dist <= GetComponent<Chase>().orbitRange)
            {
                return retreat;
            }
        }*/
        /*else
        {
            Vector3 pos = transform.position - chosenPlayer.transform.position;
            agent.SetDestination(pos);
            agent.destination = pos;
        }*/
        return this;
    }

}

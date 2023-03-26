using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Orbit : State
{
    [SerializeField] float timer;
    public State attackState;
    public State Chase;
    public State retreat;
    //[SerializeField] float orbitRange;
    float dist;
    public float orbitSpeed;
    float originalSpeed;

    Vector3 dire;
    int rotateDir;

    public override void Start()
    {
        base.Start();
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
        if (dist <= GetComponent<Chase>().orbitRange+1)
        {
            //transform.RotateAround(chosenPlayer.transform.position, Vector3.up, speed * Time.deltaTime);

            agent.speed = orbitSpeed;
            Vector3 playerpos = chosenPlayer.transform.position;
           // playerpos.y = 2;
            transform.LookAt(playerpos);

            var offsetPlayer = chosenPlayer.transform.position - transform.position;
            var dir = Vector3.Cross(offsetPlayer, dire);
            agent.SetDestination(transform.position + dir);




            Vector3 pos = transform.forward.normalized * 5;
            //agent.SetDestination(pos);
            //agent.destination = pos;
            timer += Time.deltaTime;
            if (timer >= GetComponent<EnemyStats>().generalCDN)
            {
                timer = 0;
                agent.speed = originalSpeed;
                return attackState;
            }
        }
        if(dist >= GetComponent<Chase>().orbitRange + 1)
        {
            agent.speed = originalSpeed;
            return Chase;
        }

        if(GetComponent<EnemyStats>().Type == EnemyStats.enemyType.Range)
        {
            if(dist <= GetComponent<Chase>().orbitRange)
            {
                return retreat;
            }
        }
        /*else
        {
            Vector3 pos = transform.position - chosenPlayer.transform.position;
            agent.SetDestination(pos);
            agent.destination = pos;
        }*/
        return null;
    }

}

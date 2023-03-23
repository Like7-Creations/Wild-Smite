using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : State
{
    float dist;

    [SerializeField] float retreatDist;
    public State orbit;
    Vector3 pos;
    bool obtainedPos;

    float timer;

    public override State RunCurrentState()
    {
        timer += Time.deltaTime;
        Debug.Log("Retreat");
        dist = Vector3.Distance(chosenPlayer.transform.position, transform.position);
        if (!obtainedPos)
        {
            pos = transform.position + (transform.forward * -1 * retreatDist);
            obtainedPos= true;
            agent.SetDestination(pos);
        }
        //print(Vector3.Distance(agent.destination, transform.position));
        if(obtainedPos && Vector3.Distance(agent.destination, transform.position) <= 0.2f)
        {
            obtainedPos = false;
            return orbit;
        }

        /*if(GetComponent<EnemyStats>().Type != EnemyStats.enemyType.Melee)
        {
            return orbit;
        }*/

        // Sometimes the ai is being stuck in retreat and not moving at all for some reason so i added this timer to make sure to goes back to orbit state..
        if(timer >= 3)
        {
            timer = 0;
            obtainedPos = false;
            return orbit;
        }

        /*else if(agent.destination <= pos.magnitude)
        {
        }*/
        return this;
    }
}

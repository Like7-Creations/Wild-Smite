using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public State orbitState;
    [SerializeField] float dist;


    [SerializeField] public int orbitRange;
    [SerializeField] int orbitRangeMin;
    [SerializeField] int orbitRangeMax;

    bool pointSet;



    public override void Start()
    {
        base.Start();
        orbitRange = Random.Range(orbitRangeMin, orbitRangeMax);
    }

    public override void Update()
    {
        base.Update();
    }

    public override State RunCurrentState()
    {
        // print("chase state");
        // anim.SetFloat("Y", 1);
        dist = Vector3.Distance(chosenPlayer.transform.position, transform.position);
         if(dist >= orbitRange && !pointSet)
         {
            anim.SetBool("Moving", true);
            print("chasingg player");
            Vector3 pos = chosenPlayer.transform.position - transform.position;
            agent.SetDestination(chosenPlayer.transform.position);
            pointSet = true;
            //agent.destination = pos;
         }
         else if(dist <= orbitRange)
         {
            return orbitState;
         }
        return this;
    }
}

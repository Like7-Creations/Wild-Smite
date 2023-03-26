using UnityEngine;
using UnityEngine.AI;

public class Wander : State
{
    public State chaseState;
    float dist;
    [SerializeField] public float chaseRange;

    public float wanderRange;
    Vector3 wanderPoint;

    bool wanderPointSet;
    [SerializeField] float wanderDelay;
    float wanderTimer;


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
        if(!wanderPointSet)
        {
            wanderBeh();
            wanderPointSet = true;
        }
        if(wanderPointSet)
        {
            wanderTimer += Time.deltaTime;
            if (wanderTimer >= wanderDelay)
            {
                wanderPointSet = false;
                wanderTimer = 0;
            }
        }

        if (chosenPlayer != null)       
            dist = Vector3.Distance(chosenPlayer.transform.position, transform.position);
        
        // have to do null check cuz its  causingg errors at first frame...

        print("wander state");
        if(dist <= chaseRange && chosenPlayer != null)
        {
            return chaseState;
        }
        return null;
    }

    void wanderBeh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1);
        wanderPoint = hit.position;
        agent.SetDestination(wanderPoint);
        agent.destination= wanderPoint;
    }
}

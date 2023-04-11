using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneWander : MonoBehaviour
{

    NavMeshAgent agent;
    Vector3 wanderPoint;
    public float wanderRange;

    void WanderDrone()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1);
        wanderPoint = hit.position;
        //agent = GetComponent<NavMeshAgent>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wanderPoint);
        //agent.destination= wanderPoint;
    }
}

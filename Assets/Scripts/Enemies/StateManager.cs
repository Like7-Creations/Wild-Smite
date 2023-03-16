using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    public float dist;
    public Transform player;

    private void Start()
    {
        this.player = GameObject.FindWithTag("Player").transform;
        //currentState = GetComponent<Idle>();
    }
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        RunStateMachine();

    }
    private void RunStateMachine()
    {
        State nextState = currentState.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
        //dist = Vector3.Distance(transform.position, player.transform.position);

        Debug.Log("Hello?");

    }
    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }

}

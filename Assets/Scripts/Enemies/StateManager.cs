using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    EnemyStats stats;
    public float dist;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        //currentState = GetComponent<Idle>();
    }
    void Update()
    {
        if (!stats.isDead)
        {
            RunStateMachine();
        }

    }
    private void RunStateMachine()
    {
        State nextState = currentState.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }

    }
    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }

}

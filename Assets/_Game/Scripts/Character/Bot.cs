using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    IState<Bot> currentState;
    private Vector3 destionation;
    public NavMeshAgent agent;

    public override void OnInit()
    {
        base.OnInit();
        currentState = new IdleState();
        ChangeState(currentState);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
            CanMove(TF.position + Vector3.forward);
        }
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(position);
        //Debug.Log(destionation);
    }

    public bool IsDestination()
    {
        return Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;
    }


    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void MoveStop()
    {
        agent.enabled = false;
    }
}

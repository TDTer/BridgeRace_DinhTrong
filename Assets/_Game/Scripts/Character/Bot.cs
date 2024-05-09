using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private Transform finishPos;
    public NavMeshAgent agent;
    void Start()
    {
        ChangeColor(ColorType.Blue);
    }
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(finishPos.position);
        ChangeAnim(AnimationState.run);
    }
}

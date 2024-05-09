using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    int brickTarget;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Character.AnimationState.run);
        brickTarget = Random.Range(3, 6);
        FindTarget(t);
    }

    private void FindTarget(Bot t)
    {
        if (t.stage != null)
        {
            Brick brick = t.stage.FindBrick(t.ColorType);
            if (brick == null)
            {
                t.ChangeState(new AttackState());
            }
            else
            {
                t.SetDestination(brick.TF.position);
            }
        }
        else
        {
            t.SetDestination(t.TF.position);
        }
    }

    public void OnExecute(Bot t)
    {
        if (t.IsDestination())
        {
            if (t.BrickCount >= brickTarget)
            {
                t.ChangeState(new AttackState());
            }
            else
            {
                FindTarget(t);
            }
        }
    }

    public void OnExit(Bot t)
    {

    }
}

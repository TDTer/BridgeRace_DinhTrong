using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Character.AnimationState.idle);
    }

    public void OnExecute(Bot t)
    {

    }

    public void OnExit(Bot t)
    {

    }
}

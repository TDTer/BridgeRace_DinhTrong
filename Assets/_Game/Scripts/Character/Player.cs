using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5.0f;


    public override void OnInit()
    {
        base.OnInit();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 nextPosition = TF.position + JoyStick.direction * Time.deltaTime * speed;
            if (CanMove(nextPosition + Vector3.forward))
            {
                TF.position = CheckGround(nextPosition);
                ChangeAnim(AnimationState.run);
            }
            if (JoyStick.direction != Vector3.zero)
            {
                TF.forward = JoyStick.direction;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeAnim(AnimationState.idle);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    public enum AnimationState
    {
        idle, run, win
    }
    private AnimationState currentAnim;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddBrick()
    {

    }

    private void RemoveBrick()
    {

    }

    private void ClearBrick()
    {

    }

    public void ChangeAnim(AnimationState animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim.ToString());
            currentAnim = animName;
            anim.SetTrigger(currentAnim.ToString());
        }
    }

    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 0.1f;
        }

        return TF.position;
    }
}

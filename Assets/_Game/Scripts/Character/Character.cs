using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : ColorObject
{
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer, stairLayer;
    [SerializeField] private GameObject charaterBrick;
    [SerializeField] private Transform brickContainer;
    private List<Brick> brickList = new List<Brick>();
    private float brickHeight = 0.2f;
    public Stage stage;
    public enum AnimationState
    {
        idle, run, dance
    }
    private AnimationState currentAnim;

    public int BrickCount => brickList.Count;

    public override void OnInit()
    {
        ClearBrick();
    }

    private void AddBrick()
    {
        Brick newBrick = Instantiate(charaterBrick.GetComponent<Brick>(), brickContainer);
        newBrick.ChangeColor(colorType);
        newBrick.TF.localPosition = Vector3.up * brickHeight * brickList.Count;
        brickList.Add(newBrick);
    }

    private void RemoveBrick()
    {
        if (brickList.Count > 0)
        {

            Brick playerBrick = brickList[brickList.Count - 1];
            brickList.RemoveAt(brickList.Count - 1);
            Destroy(playerBrick.gameObject);
        }
    }

    private void ClearBrick()
    {
        for (int i = 0; i < brickList.Count; i++)
        {
            Destroy(brickList[i].gameObject);
        }

        brickList.Clear();
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
        Debug.DrawRay(nextPoint + Vector3.up, Vector3.down * 2f, Color.green, 1f);
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 0.1f;
        }

        return TF.position;
    }

    public bool CanMove(Vector3 nextPos)
    {

        RaycastHit hit;
        // Debug.DrawRay(nextPos + Vector3.up, Vector3.down * 2f, Color.green, 1f);
        if (Physics.Raycast(nextPos + Vector3.up, Vector3.down, out hit, 2f, stairLayer))
        {
            if (nextPos.z - TF.position.z < 1)
            {
                return true;
            }
            else
            {
                if (hit.collider.GetComponent<ColorObject>().ColorType == ColorType)
                {
                    return true;
                }
                else
                {
                    if (brickList.Count > 0)
                    {
                        RemoveBrick();
                        hit.collider.GetComponent<ColorObject>().ChangeColor(ColorType);

                        // Respawn a new Birck in current Stage
                        stage.SpawnNewBrick(colorType);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstants.BRICK_TAG) && other.GetComponent<ColorObject>().ColorType == colorType)
        {
            // ReAdd Brick to Stage
            other.GetComponent<Brick>().ReAddBrick();
            AddBrick();
        }
    }
}

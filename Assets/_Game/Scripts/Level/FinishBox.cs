using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            UIManager.Ins.CloseUI<GamePlay>();
            LevelManager.Ins.OnFinishGame();
            if (character is Player)
            {
                UIManager.Ins.OpenUI<Win>();
            }
            else
            {
                UIManager.Ins.OpenUI<Lose>();
            }

            GameManager.Ins.ChangeState(GameState.Pause);

            character.ChangeAnim(Character.AnimationState.dance);

            character.TF.eulerAngles = Vector3.up * 180;
            character.OnInit();
        }
    }
}

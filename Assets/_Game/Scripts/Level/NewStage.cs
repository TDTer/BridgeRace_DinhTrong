using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStage : MonoBehaviour
{
    public Stage stage;
    private List<ColorType> colorTypes = new List<ColorType>();

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null && !colorTypes.Contains(character.ColorType))
        {
            colorTypes.Add(character.ColorType);
            character.stage = stage;
            stage.InitColor(character.ColorType);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ColorData")]
public class ColorData : ScriptableObject
{
    [SerializeField] Material[] colorMats;

    public Material GetColorMat(ColorType colorType)
    {
        return colorMats[(int)colorType];
    }
}

public enum ColorType
{
    None = 0,
    Red = 1,
    Blue = 2,
    Green = 3,
    Pink = 4,
    Yellow = 5,
    Black = 6,
}

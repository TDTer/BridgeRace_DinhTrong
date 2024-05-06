using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : GameUnit
{


    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer renderer;
    protected ColorType colorType;

    public ColorType ColorType { get => colorType; set => colorType = value; }

    private void Awake()
    {
        //renderer = GetComponent<SkinnedMeshRenderer>();
    }
    public void ChangeColor(ColorType colorType)
    {
        this.ColorType = colorType;
        renderer.material = colorData.GetColorMat(colorType);
    }
}

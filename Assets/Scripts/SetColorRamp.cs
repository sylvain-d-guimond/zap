using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorRamp : MonoBehaviour
{
    public Color Color1;
    public Color Color2;

    public Renderer Renderer;

    public void Call(float t)
    {
        Debug.Log($"Color at value {t}");
        Renderer.material.SetColor("_Color",Color.Lerp(Color1, Color2, 1-t));
    }
}

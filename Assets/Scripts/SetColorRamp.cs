using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorRamp : MonoBehaviour
{
    public Color Color1;
    public Color Color2;

    public List<Renderer> Renderers;

    public void Call(float t)
    {
        if (DebugMode.instance.DebugLevel <= DebugLevels.Debug) Debug.Log($"Color at value {t}");
        foreach (var r in Renderers)
        {
            r.material.SetColor("_Color", Color.Lerp(Color1, Color2, 1 - t));
        }
    }
}

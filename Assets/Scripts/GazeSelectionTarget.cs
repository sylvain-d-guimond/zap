using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class GazeSelectionTarget : MonoBehaviour
{
    public VisualEffect FX;
    public EyeTrackingTarget EyeTracking;
    public GazeSelectionIndicator Indicator;
    public UnityEvent OnTrigger;

    public int SpawnRate = 15;

    public void Highlight()
    {
        FX.SetInt("SpawnRate", SpawnRate);
    }

    public void StopHighlight()
    {
        FX.SetInt("SpawnRate", 0);
    }
}

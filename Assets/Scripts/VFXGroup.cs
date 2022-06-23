using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class VFXGroup : MonoBehaviour
{
    public VisualEffect[] Effects;

    public UnityEvent OnStart;
    public UnityEvent OnStop;

    public void StartFX()
    {
        foreach (var effect in Effects) effect.Play();
        OnStart.Invoke();
    }

    public void StopFX()
    {
        foreach (var effect in Effects)
        {
            effect.Stop();
            effect.Reinit();
        }
        OnStop.Invoke();
    }
}

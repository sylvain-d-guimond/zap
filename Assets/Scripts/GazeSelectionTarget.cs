using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

[RequireComponent(typeof(EyeTrackingTarget))]
public class GazeSelectionTarget : MonoBehaviour
{
    public EyeTrackingTarget EyeTracking;
    public Animator Animator;
    public string AnimationParameter;
    public UnityEvent OnTrigger;
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;

    private void Awake()
    {
        EyeTracking = GetComponent<EyeTrackingTarget>();
        Animator = GetComponent<Animator>();
    }

    public void Call()
    {
        OnTrigger.Invoke();
    }

    public void Select()
    {
        Debug.Log($"Select target {gameObject.name}");
        OnSelect.Invoke();
    }

    public void Deselect()
    {
        OnDeselect.Invoke();
    }

    public void Open()
    {
        if (Animator != null)
        {
            Animator.SetBool(AnimationParameter, true);
        }
    }

    public void Close ()
    {
        if (Animator != null)
        {
            Animator.SetBool(AnimationParameter, false);
        }
    }
}

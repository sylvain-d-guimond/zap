using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magic : MonoBehaviour
{
    public MagicStage Stage = MagicStage.Prepare;
    public UnityEvent OnActivate;
    public UnityEvent OnAppeared;

    private void OnEnable()
    {
        MagicManager.Instance.Add(this);
    }

    private void OnDisable()
    {
        MagicManager.Instance.Remove(this);
    }

    public void Activate()
    {
        OnActivate.Invoke();
    }

    public void Appear()
    {
        OnAppeared.Invoke();
        this.Stage = MagicStage.Prepare;
    }

    public void SetReady()
    {
        this.Stage = MagicStage.Ready;
    }

    public void Thrown()
    {
        this.Stage = MagicStage.Thrown;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

public enum MagicStage
{
    Appear,
    Prepare,
    Ready,
    Thrown
}
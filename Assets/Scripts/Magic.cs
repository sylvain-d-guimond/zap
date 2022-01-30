using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magic : MonoBehaviour
{
    public MagicStage Stage = MagicStage.Preparing;
    public UnityEvent OnActivate;

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

    public void SetReady()
    {
        this.Stage = MagicStage.Ready;
    }
}

public enum MagicStage
{
    Preparing,
    Ready
}
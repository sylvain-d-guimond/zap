using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    private bool _deactivating;

    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public UnityEvent OnDeactivateFinished;

    public UnityEvent OnShow;
    public UnityEvent OnShowFinished;

    public UnityEvent OnHide;
    public UnityEvent OnHideFinished;

    public void Activate()
    {
        OnActivate.Invoke();
    }

    public void Deactivate()
    {
        OnDeactivate.Invoke();
    }

    public void DeactivateFinished()
    {
        OnDeactivateFinished.Invoke();
    }

    public void Show()
    {
        OnShow.Invoke();
    }

    public void Hide()
    {
        OnHide.Invoke();
    }

    public void ShowFinished()
    {
        OnShowFinished.Invoke();
    }

    public void HideFinished()
    {
        OnHideFinished.Invoke();

        if (_deactivating)
        {
            OnDeactivateFinished.Invoke();

            _deactivating = false;
        }
    } 

    public void HideAndDeactivate()
    {
        _deactivating = true;

        Hide();
    }
}